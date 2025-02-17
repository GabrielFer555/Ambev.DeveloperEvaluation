using Ambev.DeveloperEvaluation.Integration.Orders.TestData;
using Ambev.DeveloperEvaluation.Integration.Orders.Utility;
using Ambev.DeveloperEvaluation.Integration.Products.TestData;
using Ambev.DeveloperEvaluation.Integration.Users.TestData;
using Ambev.DeveloperEvaluation.Integration.Users.Utility;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.Integration.Orders
{
	[Collection("Ambev.DeveloperEvaluation")] 
	public class OrderControllerTests
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationEvaluationFactory _evaluationFactory;

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
        public async Task CreateOrder_InvalidOrder_ReturnsException()
        {

			var fakeData = new Faker<CreateOrderRequest>()
             .RuleFor(x => x.CustomerId, f => Guid.NewGuid())
             .RuleFor(x => x.Branch, f => f.Commerce.Department()).Generate();


            //act
            var createOrderRequest = await _httpClient.PostAsJsonAsync("/api/Order", fakeData);

            createOrderRequest.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
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
                .RuleFor(x => x.CustomerId, f => newCustomer.Id)
                .RuleFor(x => x.Branch, f => f.Commerce.Department()).Generate();

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
            var user = await CreateUser();
            var product1 = await CreateProductsForOrders();
            var product2 = await CreateProductsForOrders();

            var fakeData = new Faker<CreateOrderRequest>()
                .RuleFor(x => x.CustomerId, f => user.Id)
                .RuleFor(x => x.Branch, f => f.Commerce.Department())
                .RuleFor(x => x.Items, f => new List<OrderItemCommandDto>
                {
            new OrderItemCommandDto { ProductId = product1.Id, Quantity = 2, Price = product1.Price },
            new OrderItemCommandDto { ProductId = product2.Id, Quantity = 3, Price = product2.Price }
                }).Generate();

            var createOrderRequest = await _httpClient.PostAsJsonAsync("/api/Order", fakeData);
            createOrderRequest.EnsureSuccessStatusCode();
            var order = await createOrderRequest.Content.ReadFromJsonAsync<OrderResponseDto>();

            var itemToCancel = order!.Items.First();
            var cancelItemRequest = await _httpClient.DeleteAsync($"/api/Order/{order.Id}/Items/{itemToCancel.ProductId}");
            cancelItemRequest.EnsureSuccessStatusCode();

            var getOrder = await _httpClient.GetAsync($"/api/Order/{order.Id}");
            getOrder.EnsureSuccessStatusCode();
            var updatedOrder = await getOrder.Content.ReadFromJsonAsync<GetOrderResponseDto>();

            updatedOrder.Should().NotBeNull();
            updatedOrder.Items.Should().NotBeEmpty();

            var canceledItem = updatedOrder.Items.FirstOrDefault(x => x.ProductId == itemToCancel.ProductId);
            canceledItem.Should().NotBeNull();
            canceledItem.OrderStatus.Should().Be("Canceled");

            var activeItems = updatedOrder.Items.Where(x => x.ProductId != itemToCancel.ProductId);
            activeItems.Should().NotBeEmpty();
            activeItems.All(x => x.OrderStatus != "Canceled").Should().BeTrue();
        }

        [Fact]
        public async Task CancelOrderItem_CancelAllOrderItems_ShouldCancelOrder()
        {
			var user = await CreateUser();
            var product1 = await CreateProductsForOrders();
            var product2 = await CreateProductsForOrders();

            var fakeData = new Faker<CreateOrderRequest>()
                .RuleFor(x => x.CustomerId, f => user.Id)
                .RuleFor(x => x.Branch, f => f.Commerce.Department())
                .RuleFor(x => x.Items, f => new List<OrderItemCommandDto>
                {
            new OrderItemCommandDto { ProductId = product1.Id, Quantity = 2, Price = product1.Price },
            new OrderItemCommandDto { ProductId = product2.Id, Quantity = 3, Price = product2.Price }
                }).Generate();

            var createOrderRequest = await _httpClient.PostAsJsonAsync("/api/Order", fakeData);
            createOrderRequest.EnsureSuccessStatusCode();
            var order = await createOrderRequest.Content.ReadFromJsonAsync<OrderResponseDto>();

            foreach (var item in order!.Items)
            {
                var cancelItemRequest = await _httpClient.DeleteAsync($"/api/Order/{order.Id}/Items/{item.ProductId}");
                cancelItemRequest.EnsureSuccessStatusCode();
            }


            var getOrder = await _httpClient.GetAsync($"/api/Order/{order.Id}");
            getOrder.EnsureSuccessStatusCode();
            var updatedOrder = await getOrder.Content.ReadFromJsonAsync<GetOrderResponseDto>();

            updatedOrder.Should().NotBeNull();
            updatedOrder.Items.Should().NotBeEmpty();
            updatedOrder.Items.Any(x => x.OrderStatus != "Canceled").Should().BeFalse();
            updatedOrder.OrderStatus.Should().Be("Canceled");
            updatedOrder.TotalPrice.Should().Be(0);


        }
        [Fact]
        public async Task CreateOrder_OrderWithNoItems_ShouldReturnException()
        {
            //arrange

            var order = CreateOrderTestData.GenerateValidData();
            order.Items = new();

            //act
            var act = await _httpClient.PostAsJsonAsync("/api/Order", order);

            //assert
            act.StatusCode.Should().Be(HttpStatusCode.BadRequest);

		}

		private async Task<UserUtilityResponse> CreateUser()
        {
            var user = CreateUserTestData.GenerateValidCommand();

            //act
            var result = await _httpClient.PostAsJsonAsync("/api/Users", user);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<UserUtilityResponse>();
            return response!;
        }

        private async Task<CreateProductResponse> CreateProductsForOrders()
        {
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