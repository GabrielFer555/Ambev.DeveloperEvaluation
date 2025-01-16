using System.Threading;
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
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{
    [Route("api/[controller]")]
	[ApiController]
	public class CartController(IMapper mapper, ISender sender) : ControllerBase
	{
		// GET: api/<CartController>
		[HttpGet]
		public async Task<IActionResult> GetAllProducts([FromQuery] GetAllCartsRequest request, CancellationToken cancellationToken)
		{
			var validator = new GetAllCartsRequestValidator();
			var isBodyValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isBodyValid.IsValid) throw new ValidationException(isBodyValid.Errors);
			var query = mapper.Map<GetAllCartsQuery>(request);
			var result = await sender.Send(query);
			var response = mapper.Map<GetAllCartsResponse>(result);
			return Ok(response);
		}

		// GET api/<CartController>/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCartById([FromRoute] GetCartByIdRequest request, CancellationToken cancellationToken)
		{
			var validator = new GetCartByIdRequestValidator();
			var isBodyValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isBodyValid.IsValid) throw new ValidationException(isBodyValid.Errors);
			var query = mapper.Map<GetCartByIdQuery>(request);
			var result = await sender.Send(query);
			var response = mapper.Map<GetCartByIdResponse>(result);
			return Ok(response);
		}

		// POST api/<CartController>
		[HttpPost]
		public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
		{
			var validator = new CreateCartRequestValidator();
			var isBodyValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isBodyValid.IsValid) throw new ValidationException(isBodyValid.Errors);
			var command = mapper.Map<CreateCartCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<CreateCartResponse>(result);
			return Ok(response);
		}

		// PUT api/<CartController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateCartRequest request, CancellationToken cancellationToken)
		{
			request.Id = id;
			UpdateCartValidator validator = new();
			var isValid = await validator.ValidateAsync(request);
			if (!isValid.IsValid) throw new ValidationException(isValid.Errors);

			var command = mapper.Map<UpdateCartCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<UpdateCartResponse>(result);
			return Ok(response);

		}

		// DELETE api/<CartController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] DeleteCartRequest request, CancellationToken cancellationToken)
		{
			var validator = new DeleteCartRequestValidator();
			var isBodyValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isBodyValid.IsValid) throw new ValidationException(isBodyValid.Errors);
			var command = mapper.Map<DeleteCartCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<DeleteCartResponse>(result);
			return Ok(response);
		}
	}
}
