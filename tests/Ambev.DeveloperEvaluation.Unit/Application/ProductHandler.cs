using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Product;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using Xunit;
using Xunit.Sdk;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
	public class CreateProductHandlerTests
	{
		private readonly CreateProductHandler _handler;
		private readonly IMapper _mapper;

		[Fact]
		public async Task Handle_CreatedProduct_ReturnsValid()
		{
			//given
			var product = CreateProductHandlerTestsData.GenerateValidCommand();
			//act
			

			//Assert
		}
	}
}
