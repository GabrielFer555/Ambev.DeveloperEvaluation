﻿namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartById
{
	public class GetCartByIdResult
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public Guid UserId { get; set; }
		public List<CartItemDto> Products { get; set; } = new();
	}
}
