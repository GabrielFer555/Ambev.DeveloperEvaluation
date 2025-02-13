using System.Net.Http.Headers;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Integration.Products.TestData;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using FluentAssertions;

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
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestingPurposes");
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
	}
}
