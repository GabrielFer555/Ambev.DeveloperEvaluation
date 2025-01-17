using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
	public class CreateOrderProfile:Profile
	{
		public CreateOrderProfile() {
			CreateMap<CreateOrderCommand, Order>();
			CreateMap<Order, CreateOrderResult> ();
		}
	}
}
