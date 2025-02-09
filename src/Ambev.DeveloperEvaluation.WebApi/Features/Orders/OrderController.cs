﻿using Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;
using Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem;
using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.GetAllOrders;
using Ambev.DeveloperEvaluation.Application.Orders.GetOrderById;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetAllOrders;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrderById;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController(ISender sender, IMapper mapper) : ControllerBase
	{
		[HttpGet]
		[Authorize]
		[ProducesResponseType(typeof(GetAllOrdersResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersRequest request, CancellationToken cancellationToken)
		{
			var command = mapper.Map<GetAllOrdersQuery>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<GetAllOrdersResponse>(result);
			return Ok(response);
		}


		[HttpGet("{id:int}")]
		[Authorize]
		[ProducesResponseType(typeof(GetOrderByIdResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Get([FromRoute] GetOrderByIdRequest request, CancellationToken cancellationToken)
		{
			var query = mapper.Map<GetOrderByIdQuery>(request);
			var result = await sender.Send(query);
			var response = mapper.Map<GetOrderByIdResponse>(result);

			return Ok(response);
		}

		[HttpPost]
		[Authorize]
		[ProducesResponseType(typeof(CreateOrderResponse), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
		{	
			var command = mapper.Map<CreateOrderCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<CreateOrderResponse>(result);
			return Created($"Order/{response.Id}",response);
		}

		[HttpPut("{id:int}")]
		[Authorize]
		[ProducesResponseType(typeof(UpdateOrderResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] UpdateOrderRequest request, CancellationToken cancellationToken)
		{
			request.Id = id;
			var command = mapper.Map<UpdateOrderCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<UpdateOrderResponse>(result);

			return Ok(response);
		}

		[HttpDelete("{id:int}")]
		[Authorize]
		[ProducesResponseType(typeof(CancelOrderResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> CancelOrder([FromRoute] CancelOrderRequest request, CancellationToken cancellationToken)
		{
			var command = mapper.Map<CancelOrderCommand>(request);
			var result = await sender.Send(command);	
			var response = mapper.Map<CancelOrderResponse>(result);
			return Ok(response);

		}
		[HttpDelete("{orderId:int}/items/{orderItemId:int}")]
		[Authorize]
		[ProducesResponseType(typeof(CancelOrderItemResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> CancelOrder([FromRoute] CancelOrderItemRequest request, CancellationToken cancellationToken)
		{
			var command = mapper.Map<CancelOrderItemCommand>(request);
			var result = await sender.Send(command);
			var response = mapper.Map<CancelOrderItemResponse>(result);
			return Ok(response);

		}
	}
}
