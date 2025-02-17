using Ambev.DeveloperEvaluation.Integration.Carts.TestData;
using Ambev.DeveloperEvaluation.Integration.Products.TestData;
using Ambev.DeveloperEvaluation.Integration.Users.TestData;
using Ambev.DeveloperEvaluation.Integration.Users.Utility;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using FluentAssertions.Extensions;

namespace Ambev.DeveloperEvaluation.Integration.Carts
{
	[Collection("Ambev.DeveloperEvaluation")]
	public class CartControllerTest
	{
		private readonly HttpClient _httpClient;
		private readonly WebApplicationEvaluationFactory _evaluationFactory;
		public CartControllerTest()
		{
			_evaluationFactory = new WebApplicationEvaluationFactory();
			_httpClient = _evaluationFactory.CreateClient();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthHandler.GenerateJwtToken());
		}
		[Fact]
		public async Task CreateCart_ValidCart_ReturnsObj()
		{
			//arrange
			await CreateProductsForCart();
			var user = await CreateUser();
			
			var cart = CreateCartTestData.GenerateValidData();
			cart.UserId = user.Id;
			//act

			var act = await _httpClient.PostAsJsonAsync("/api/Cart", cart);
			act.EnsureSuccessStatusCode();
			var result = await act.Content.ReadFromJsonAsync<CreateCartResponse>();

			//assert
			result.Should().NotBeNull();
			result.Id.Should().BePositive();
			result.UserId.Should().Be(cart.UserId);
			result.Date.Should().BeAfter(1.January(2020));
			result.Products.Should().BeEquivalentTo(cart.Products);

		}
		[Fact]
		public async Task CreateCart_InvalidCart_ReturnsException()
		{
			//arrange

			var cart = CreateCartTestData.GenerateValidData();
			cart.Products = new();

			//act

			var act = await _httpClient.PostAsJsonAsync("/api/Cart", cart);
			
			//assert

			act.StatusCode.Should().Be(HttpStatusCode.BadRequest);

		}

		[Fact]
		public async Task UpdateCart_ValidCart_ReturnsObj()
		{
			//arrange
			var productCreated = await CreateProductsForCart();
			var newUser = await CreateUser();
			var cart = await CreateCartForTestingPurposes();
			var updatedCart = UpdateCartTestData.GenerateValidData();
			updatedCart.Id = cart.Id;
			updatedCart.UserId = newUser.Id;
			updatedCart.Products[0].ProductId = productCreated.Id;

			//act

			var act = await _httpClient.PutAsJsonAsync($"/api/Cart/{cart.Id}", updatedCart);
			act.EnsureSuccessStatusCode();
			var result = await act.Content.ReadFromJsonAsync<UpdateCartResponse>();

			//assert

			result.Should().NotBeNull();
			result.Id.Should().Be(cart.Id);
			result.Products.Should().BeEquivalentTo(updatedCart.Products);
			result.Date.Should().BeAfter(cart.Date);	
			result.UserId.Should().Be(newUser.Id);	


		}
		[Fact]
		public async Task UpdateCart_InvalidCart_ReturnsException()
		{
			//arrange

			var updatedCart = UpdateCartTestData.GenerateValidData();

			//act

			var act = await _httpClient.PutAsJsonAsync($"/api/Cart/{updatedCart.Id}", updatedCart);

			//assert
			act.StatusCode.Should().Be(HttpStatusCode.BadRequest);


		}
		[Fact]
		public async Task DeleteCart_ValidCart_DeletesCart()
		{
			//arrange

			var cart = await CreateCartForTestingPurposes();

			//act
			var deleteCartRequest = await _httpClient.DeleteAsync($"/api/Cart/{cart.Id}");
			deleteCartRequest.EnsureSuccessStatusCode();
			var act = await _httpClient.GetAsync($"/api/Cart/{cart.Id}");

			//assert
			act.StatusCode.Should().Be(HttpStatusCode.NotFound);		
	
		}
		[Fact]
		public async Task DeleteCart_InvalidCart_ReturnsNotFound()
		{
			//arrange

			var cartId = new Random().Next(10000, 56000);
			//act
			var deleteCartRequest = await _httpClient.DeleteAsync($"/api/Cart/{cartId}");


			//assert
			deleteCartRequest.StatusCode.Should().Be(HttpStatusCode.NotFound);

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
		private async Task<CreateProductResponse> CreateProductsForCart()
		{
			var requestBody = CreateProductTestData.GenerateValidData();
			var result = await _httpClient.PostAsJsonAsync("/api/Product", requestBody);
			result.EnsureSuccessStatusCode();

			var responseBody = await result.Content.ReadFromJsonAsync<CreateProductResponse>();
			return responseBody!;
		}
		private async Task <CreateCartResponse> CreateCartForTestingPurposes()
		{
			await CreateProductsForCart();
			var user = await CreateUser();

			var cart = CreateCartTestData.GenerateValidData();
			cart.UserId = user.Id;
			var request = await _httpClient.PostAsJsonAsync("/api/Cart", cart);
			request.EnsureSuccessStatusCode();
			var result = await request.Content.ReadFromJsonAsync<CreateCartResponse>();

			return result!;
		}
	}
}
