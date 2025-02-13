using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Integration.Products.TestData;
using Ambev.DeveloperEvaluation.Integration.Users.TestData;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Integration.Products
{
    public class ProductControllerTests
	{
		private readonly WebApplicationEvaluationFactory _evaluationFactory;
		private readonly HttpClient _httpClient;
		public ProductControllerTests()
		{
			_evaluationFactory = new WebApplicationEvaluationFactory();
			_httpClient = _evaluationFactory.CreateClient();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthHandler.GenerateJwtToken());
		}

		[Fact]
		public async Task CreateProduct_ValidProduct_ReturnsObject()
		{
			
			//arrange
			var requestBody = CreateProductTestData.GenerateValidData();
			
			//act
			var result = await _httpClient.PostAsJsonAsync("/api/Product", requestBody);

			//assert
			result.EnsureSuccessStatusCode();

			var responseBody = await result.Content.ReadFromJsonAsync<CreateProductResponse>();

			responseBody.Should().NotBeNull();
			responseBody.Id.Should().BePositive();
			responseBody.Image.Should().Be(requestBody.Image);
			responseBody.Price.Should().Be(requestBody.Price);
			responseBody.Title.Should().Be(requestBody.Title);
			responseBody.Category.Should().Be(requestBody.Category);
			responseBody.Title.Should().Be(requestBody.Title);
			responseBody.Rating.Should().BeEquivalentTo(requestBody.Rating);
		
		}
		[Fact]
		public async Task CreateProduct_InvalidProduct_ReturnsBadRequest()
		{

			//arrange
			var requestBody = CreateProductTestData.GenerateValidData();
			requestBody.Title = string.Empty;

			//act
			var result = await _httpClient.PostAsJsonAsync("/api/Product", requestBody);

			//assert

			result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

		[Fact]
		public async Task UpdateProduct_ValidProduct_ReturnsProduct()
		{
            var createdProduct = CreateProductTestData.GenerateValidData();
			var updateProduct = UpdateProductTestData.GenerateValidData();
            var productCreated = await _httpClient.PostAsJsonAsync("/api/Product", createdProduct);
            productCreated.EnsureSuccessStatusCode();

            //act

            var productCreatedResponseBody = await productCreated.Content.ReadFromJsonAsync<CreateProductResponse>();

			updateProduct.Id = productCreatedResponseBody!.Id;

			var updatedProduct = await _httpClient.PutAsJsonAsync($"/api/Product/{updateProduct.Id}", updateProduct);
			var response = await updatedProduct.Content.ReadFromJsonAsync<UpdateProductResponse>();
           
			
			//assert

            updatedProduct.EnsureSuccessStatusCode();
			response.Should().NotBeNull();
            response.Id.Should().Be(productCreatedResponseBody.Id);
			response.Rating.Should().BeEquivalentTo(updateProduct.Rating);
			response.Title.Should().Be(updateProduct.Title);
			response.Image.Should().Be(updateProduct.Image);
			response.Price.Should().Be(updateProduct.Price);
			response.Description.Should().Be(updateProduct.Description);
			response.Category.Should().Be(updateProduct.Category);

        }
		[Fact]
		public async Task UpdateProduct_NonExistentProduct_ReturnsNotFound()
		{
			//arrange
			var updateProduct = UpdateProductTestData.GenerateValidData();

            //act

			var updatedProduct = await _httpClient.PutAsJsonAsync($"/api/Product/{updateProduct.Id}", updateProduct);

			//assert
			updatedProduct.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

		[Fact]
		public async Task DeleteProduct_ValidProduct_ReturnsSuccess()
		{

			//arrange
            var createdProduct = CreateProductTestData.GenerateValidData();
            var productCreated = await _httpClient.PostAsJsonAsync("/api/Product", createdProduct);
            productCreated.EnsureSuccessStatusCode();
            var productCreatedResponseBody = await productCreated.Content.ReadFromJsonAsync<CreateProductResponse>();

			//act
			var request = await _httpClient.DeleteAsync($"/api/Product/{productCreatedResponseBody!.Id}");

			//assert

			request.StatusCode.Should().Be(HttpStatusCode.OK);

        }
		[Fact]
 		public async Task DeleteProduct_When_ProductHasOrder_ReturnsException()
		{
			var product = CreateProductTestData.GenerateValidData();
			var user = CreateUserTestData.GenerateValidCommand();
			

			var userCreated = await _httpClient.PostAsJsonAsync("/api/Users", user);
			userCreated.EnsureSuccessStatusCode();
            var jsonResponse = await userCreated.Content.ReadAsStringAsync();

            // Parse the JSON
            using var document = JsonDocument.Parse(jsonResponse);

            // Navigate to data -> id
            var root = document.RootElement;
            var idValue = root.GetProperty("id").GetString();

            var order = new Faker<CreateOrderCommand>()
			.RuleFor(x => x.CustomerId,f =>  new Guid(idValue!))
            .RuleFor(x => x.Branch, f => f.Commerce.Department())
            .RuleFor(x => x.Items, f =>
                new Faker<OrderItemCommandDto>().RuleFor(x => x.ProductId, f => 1)
                .RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(x => x.Quantity, f => f.Random.Int(1, 20)).Generate(1)
            ).Generate();


            var productCreated = await _httpClient.PostAsJsonAsync("/api/Product", product);
            productCreated.EnsureSuccessStatusCode();
            var productCreatedResponseBody = await productCreated.Content.ReadFromJsonAsync<CreateProductResponse>();

            var orderCreated = await _httpClient.PostAsJsonAsync("/api/Order", order);
			orderCreated.EnsureSuccessStatusCode();

			//act
			var response = await _httpClient.DeleteAsync($"/api/Product/{productCreatedResponseBody!.Id}");

			//assert
			response.Should().NotBeNull();
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }
}
}
