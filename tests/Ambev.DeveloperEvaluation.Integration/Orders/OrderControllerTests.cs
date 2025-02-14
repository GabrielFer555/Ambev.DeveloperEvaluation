using Ambev.DeveloperEvaluation.Integration.Orders.Utility;
using Ambev.DeveloperEvaluation.Integration.Products.TestData;
using Ambev.DeveloperEvaluation.Integration.Users.TestData;
using Ambev.DeveloperEvaluation.Integration.Users.Utility;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Ambev.DeveloperEvaluation.Integration.Orders
{
    public class OrderControllerTests
    {
        public HttpClient _httpClient { get; private set; }
        public WebApplicationEvaluationFactory _evaluationFactory { get; private set; }

        public OrderControllerTests()
        {
            _evaluationFactory = new WebApplicationEvaluationFactory();
            _httpClient = _evaluationFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthHandler.GenerateJwtToken());
        }

        [Fact]
        public async Task CreateOrder_ValidOrderWithoutDiscount_ReturnsOrderWithNoDiscount()
        {
               //arrange
               var user = await CreateUser();
               var product = await CreateProductsForOrders();
                var fakeData = new Faker<CreateOrderRequest>()
                 .RuleFor(x => x.CustomerId, f => user.Id)
                 .RuleFor(x => x.Branch, f => f.Commerce.Department())
                 .RuleFor(x => x.Items, f => new Faker<OrderItemCommandDto>()
                    .RuleFor(x => x.ProductId, f => product.Id)
                    .RuleFor(x => x.Quantity, f => f.Random.Int(1, 3))
                    .RuleFor(x => x.Price, f => product.Price).Generate(1)).Generate();


            //act
            var createOrderRequest = await _httpClient.PostAsJsonAsync("/api/Order", fakeData);
            createOrderRequest.EnsureSuccessStatusCode();
            var result = await createOrderRequest.Content.ReadFromJsonAsync<OrderResponseDto>();

            //assert

            result.Should().NotBeNull();
            result.Id.Should().BePositive();
            result.Branch.Should().Be(fakeData.Branch);
            result.CustomerId.Should().Be(fakeData.CustomerId);
            result.CreatedAt.Should().BeAfter(DateTime.Today);
            foreach (var item in result.Items)
            {
                item.Should().NotBeNull();
                item.Discount.Should().Be(0);
                item.TotalDiscount.Should().Be(item.TotalPrice);
            }
       
        }

        [Fact]
        public async Task CreateOrder_ValidOrder_10Percent_Discount_ReturnsOrderWithDiscount()
        {
            //arrange
            var user = await CreateUser();
            var product = await CreateProductsForOrders();
            var fakeData = new Faker<CreateOrderRequest>()
             .RuleFor(x => x.CustomerId, f => user.Id)
             .RuleFor(x => x.Branch, f => f.Commerce.Department())
             .RuleFor(x => x.Items, f => new Faker<OrderItemCommandDto>()
                .RuleFor(x => x.ProductId, f => product.Id)
                .RuleFor(x => x.Quantity, f => f.Random.Int(4, 9))
                .RuleFor(x => x.Price, f => product.Price).Generate(1)).Generate();


            //act
            var createOrderRequest = await _httpClient.PostAsJsonAsync("/api/Order", fakeData);
            createOrderRequest.EnsureSuccessStatusCode();
            var result = await createOrderRequest.Content.ReadFromJsonAsync<OrderResponseDto>();

            //assert

            result.Should().NotBeNull();
            result.Id.Should().BePositive();
            result.Branch.Should().Be(fakeData.Branch);
            result.CustomerId.Should().Be(fakeData.CustomerId);
            result.CreatedAt.Should().BeAfter(DateTime.Today);
            foreach (var item in result.Items)
            {
                item.Should().NotBeNull();
                item.Discount.Should().Be(10);
                item.TotalDiscount.Should().BeLessThan(item.TotalPrice);
            }

        }
        [Fact]
        public async Task CreateOrder_ValidOrder_20Percent_Discount_ReturnsOrderWithDiscount()
        {
            //arrange
            var user = await CreateUser();
            var product = await CreateProductsForOrders();
            var fakeData = new Faker<CreateOrderRequest>()
             .RuleFor(x => x.CustomerId, f => user.Id)
             .RuleFor(x => x.Branch, f => f.Commerce.Department())
             .RuleFor(x => x.Items, f => new Faker<OrderItemCommandDto>()
                .RuleFor(x => x.ProductId, f => product.Id)
                .RuleFor(x => x.Quantity, f => f.Random.Int(11, 20))
                .RuleFor(x => x.Price, f => product.Price).Generate(1)).Generate();


            //act
            var createOrderRequest = await _httpClient.PostAsJsonAsync("/api/Order", fakeData);
            createOrderRequest.EnsureSuccessStatusCode();
            var result = await createOrderRequest.Content.ReadFromJsonAsync<OrderResponseDto>();

            //assert

            result.Should().NotBeNull();
            result.Id.Should().BePositive();
            result.Branch.Should().Be(fakeData.Branch);
            result.CustomerId.Should().Be(fakeData.CustomerId);
            result.CreatedAt.Should().BeAfter(DateTime.Today);
            foreach (var item in result.Items)
            {
                item.Should().NotBeNull();
                item.Discount.Should().Be(20);
                item.TotalDiscount.Should().BeLessThan(item.TotalPrice);
            }

        }

        [Fact]
        public async Task UpdateOrder_ValidOrder_ReturnsUpdatedOrder()
        {
            //arrange
            var orderCreated = await CreateOrderForTestingPurposes();
            var newCustomer = await CreateUser();
            var orderUpdate = new Faker<UpdateOrderRequest>()
                .RuleFor(x => x.Id, f => orderCreated.Id)
                .RuleFor(x => x.CustomerId, f=> newCustomer.Id)
                .RuleFor(x => x.Branch, f=> f.Commerce.Department()).Generate();

            //act
            var updateOrderRequest = await _httpClient.PutAsJsonAsync($"/api/Order/{orderCreated.Id}", orderUpdate);
            updateOrderRequest.EnsureSuccessStatusCode();
            var result = await updateOrderRequest.Content.ReadFromJsonAsync<OrderResponseDto>();
            //asset

            result.Should().NotBeNull();
            result.Id.Should().Be(orderCreated.Id);
            result.CustomerId.Should().Be(newCustomer.Id);
            result.Branch.Should().Be(orderUpdate.Branch);
            result.Items.Should().NotBeNull();

        }
        [Fact]
        public async Task CancelOrder_ValidOrder_ReturnsOrderWithItemsCancelled()
        {
            //arrange
            var orderCreated = await CreateOrderForTestingPurposes();
            //act
            var deleteOrderRequest = await _httpClient.DeleteAsync($"/api/Order/{orderCreated.Id}");
            deleteOrderRequest.EnsureSuccessStatusCode();
            var getOrder = await _httpClient.GetAsync($"/api/Order/{orderCreated.Id}");
            getOrder.EnsureSuccessStatusCode();
            var result = await getOrder.Content.ReadFromJsonAsync<GetOrderResponseDto>();

            //assert
            result.Should().NotBeNull();
            result.OrderStatus.Should().Be("Canceled");
            result.TotalPrice.Should().Be(0);
            var itemsNotCancelled = result.Items.Any(x => x.OrderStatus != "Canceled");
            itemsNotCancelled.Should().BeFalse();

        }

        [Fact]
        public async Task CancelOrderItem_ForOrderWithMoreThanOneItem_OnlyOneItemIsCancelled()
        {
                    
        }

        private async Task<UserUtilityResponse> CreateUser() {
            var user = CreateUserTestData.GenerateValidCommand();

            //act
            var result = await _httpClient.PostAsJsonAsync("/api/Users", user);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<UserUtilityResponse>();
            return response!;
        }

        private async Task<CreateProductResponse> CreateProductsForOrders() {
            var requestBody = CreateProductTestData.GenerateValidData();
            var result = await _httpClient.PostAsJsonAsync("/api/Product", requestBody);
            result.EnsureSuccessStatusCode();

            var responseBody = await result.Content.ReadFromJsonAsync<CreateProductResponse>();
            return responseBody!;
        }
        private async Task<OrderResponseDto> CreateOrderForTestingPurposes()
        {
            var user = await CreateUser();
            var product = await CreateProductsForOrders();
            var fakeData = new Faker<CreateOrderRequest>()
             .RuleFor(x => x.CustomerId, f => user.Id)
             .RuleFor(x => x.Branch, f => f.Commerce.Department())
             .RuleFor(x => x.Items, f => new Faker<OrderItemCommandDto>()
                .RuleFor(x => x.ProductId, f => product.Id)
                .RuleFor(x => x.Quantity, f => f.Random.Int(1, 3))
                .RuleFor(x => x.Price, f => product.Price).Generate(1)).Generate();


            //act
            var createOrderRequest = await _httpClient.PostAsJsonAsync("/api/Order", fakeData);
            createOrderRequest.EnsureSuccessStatusCode();
            var result = await createOrderRequest.Content.ReadFromJsonAsync<OrderResponseDto>();

            return result!;
        }
    }
}
