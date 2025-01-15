
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetCategories;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController(IMapper mapper, ISender sender) : ControllerBase
	{

		[HttpPost]
		public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
		{
			var validator = new CreateProductRequestValidator();	
			var validBody = await validator.ValidateAsync(request, cancellationToken);
			if(!validBody.IsValid) throw new ValidationException(validBody.Errors);
			var command = mapper.Map<CreateProductCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<CreateProductResponse>(result);
			return Created($"/products/{response.Id}", response);
		}
		[HttpGet]
		public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsRequest request, CancellationToken cancellationToken)
		{
			var validator = new GetAllProductsValidator();
			var isRequestValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isRequestValid.IsValid)
			{
				throw new ValidationException(isRequestValid.Errors);
			}
			var command = mapper.Map<GetAllProductsQuery>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<GetAllProductsResponse>(result);
			return Ok(response);
		}
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetProductById([FromRoute] GetProductsByIdRequest request, CancellationToken cancellationToken)
		{
			var validator = new GetProductByIdRequestValidator();
			var isRequestValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isRequestValid.IsValid)
			{
				throw new ValidationException(isRequestValid.Errors);
			}
			var query = mapper.Map<GetProductsByIdQuery>(request);
			var result = await sender.Send(query);
			var response = mapper.Map<GetProductsByIdResult>(result);
			return Ok(response);
		}
		[HttpPut("{id:int}")]
		public async Task<IActionResult> GetProductById([FromRoute] int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
		{
			var validator = new UpdateProductRequestValidator();
			var validBody = await validator.ValidateAsync(request, cancellationToken);
			if (!validBody.IsValid) {
				throw new ValidationException(validBody.Errors);
			}
			var command = mapper.Map<UpdateProductCommand>(request);
			command.Id = id;
			var result = await sender.Send(command);
			var response = mapper.Map<UpdateProductResponse>(result);
			return Ok(response);
		}
		[HttpGet("categories")]
		public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
		{
			var result = await sender.Send(new GetCategoriesQuery());
			var response = mapper.Map<GetCategoriesResponse>(result);
			return Ok(response.Categories);
		}
		[HttpGet("categories/{category}")]
		public async Task<IActionResult> GetProductsByCategories(string category,[FromQuery] QueryPagination queryParams, CancellationToken cancellationToken)
		{
			var request = new GetProductsByCategoriesRequest
			{
				Category = category,
				_Limit = queryParams._Limit,
				_Page = queryParams._Page
			};
			var validator = new GetProductsByCategoriesRequestValidator();
			var validBody = await validator.ValidateAsync(request, cancellationToken);
			if (!validBody.IsValid)
			{
				throw new ValidationException(validBody.Errors);
			}
			var query = mapper.Map<GetProductsByCategoriesQuery>(request);
			var result = await sender.Send(query);
			var response = mapper.Map<GetProductsByCategoriesResponse>(result);
			return Ok(response);
		}
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductRequest request, CancellationToken cancellation)
		{
			var validator = new DeleteProductRequestValidator();
			var isRequestBodyValid = await validator.ValidateAsync(request);
			if (!isRequestBodyValid.IsValid)
			{
				throw new ValidationException(isRequestBodyValid.Errors);
			}
			var command = mapper.Map<DeleteProductCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<DeleteProductResponse>(result);
			return Ok(response);
		}
		
	}
	public record QueryPagination(int? _Page, int? _Limit);
}
