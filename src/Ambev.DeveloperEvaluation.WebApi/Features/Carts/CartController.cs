using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.Application.Carts.GetCartById;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{
    [Route("api/[controller]")]
	[ApiController]
	public class CartController(IMapper mapper, ISender sender) : ControllerBase
	{
		// GET: api/<CartController>
		[HttpGet]
		[Authorize]
		[ProducesResponseType(typeof(GetAllCartsResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetAllProducts([FromQuery] GetAllCartsRequest request, CancellationToken cancellationToken)
		{
			var query = mapper.Map<GetAllCartsQuery>(request);
			var result = await sender.Send(query);
			var response = mapper.Map<GetAllCartsResponse>(result);
			return Ok(response);
		}

		// GET api/<CartController>/5
		[HttpGet("{id}")]
		[Authorize]
		[ProducesResponseType(typeof(GetCartByIdResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetCartById([FromRoute] GetCartByIdRequest request, CancellationToken cancellationToken)
		{
			var query = mapper.Map<GetCartByIdQuery>(request);
			var result = await sender.Send(query);
			var response = mapper.Map<GetCartByIdResponse>(result);
			return Ok(response);
		}

		// POST api/<CartController>
		[HttpPost]
		[Authorize]
		[ProducesResponseType(typeof(CreateCartResponse), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
		{
			var command = mapper.Map<CreateCartCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<CreateCartResponse>(result);
			return Ok(response);
		}

		// PUT api/<CartController>/5
		[HttpPut("{id}")]
		[Authorize]
		[ProducesResponseType(typeof(UpdateCartResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateCartRequest request, CancellationToken cancellationToken)
		{
			request.Id = id;
			var command = mapper.Map<UpdateCartCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<UpdateCartResponse>(result);
			return Ok(response);

		}

		// DELETE api/<CartController>/5
		[HttpDelete("{id}")]
		[Authorize]
		[ProducesResponseType(typeof(DeleteCartResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete([FromRoute] DeleteCartRequest request, CancellationToken cancellationToken)
		{
			var command = mapper.Map<DeleteCartCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<DeleteCartResponse>(result);
			return Ok(response);
		}
	}
}
