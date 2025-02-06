
using Ambev.DeveloperEvaluation.Application.Products.GetCategories;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController(IMapper mapper, ISender sender) : ControllerBase
	{

		[HttpPost]
		[Authorize]
		[ProducesResponseType(typeof(CreateProductResponse), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
		[Authorize]
		[ProducesResponseType(typeof(GetAllProductsResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsRequest request, CancellationToken cancellationToken)
		{
			var command = mapper.Map<GetAllProductsQuery>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<GetAllProductsResponse>(result);
			return Ok(response);
		}
		[HttpGet("{id:int}")]
		[Authorize]
		[ProducesResponseType(typeof(GetProductsByIdResult), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetProductById([FromRoute] GetProductsByIdRequest request, CancellationToken cancellationToken)
		{
			var query = mapper.Map<GetProductsByIdQuery>(request);
			var result = await sender.Send(query);
			var response = mapper.Map<GetProductsByIdResult>(result);
			return Ok(response);
		}
		[HttpPut("{id:int}")]
		[Authorize]
		[ProducesResponseType(typeof(UpdateProductResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetProductById([FromRoute] int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
		{
			var command = mapper.Map<UpdateProductCommand>(request);
			command.Id = id;
			var result = await sender.Send(command);
			var response = mapper.Map<UpdateProductResponse>(result);
			return Ok(response);
		}
		[HttpGet("categories")]
		[Authorize]
		[ProducesResponseType(typeof(GetCategoriesResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
		{
			var result = await sender.Send(new GetCategoriesQuery());
			var response = mapper.Map<GetCategoriesResponse>(result);
			return Ok(response.Categories);
		}
		[HttpGet("categories/{category}")]
		[Authorize]
		[ProducesResponseType(typeof(GetProductsByCategoriesResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetProductsByCategories(string category,[FromQuery] QueryPagination queryParams, CancellationToken cancellationToken)
		{
			var request = new GetProductsByCategoriesRequest
			{
				Category = category,
				_Limit = queryParams._Limit,
				_Page = queryParams._Page
			};
			var query = mapper.Map<GetProductsByCategoriesQuery>(request);
			var result = await sender.Send(query);
			var response = mapper.Map<GetProductsByCategoriesResponse>(result);
			return Ok(response);
		}
		[HttpDelete("{id:int}")]
		[Authorize]
		[ProducesResponseType(typeof(DeleteProductResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductRequest request, CancellationToken cancellation)
		{
			var command = mapper.Map<DeleteProductCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<DeleteProductResponse>(result);
			return Ok(response);
		}
		
	}
	public record QueryPagination(int? _Page, int? _Limit);
}
