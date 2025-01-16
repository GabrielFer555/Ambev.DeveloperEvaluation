using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartById
{
	public class GetCartByIdProfile:Profile
	{
		public GetCartByIdProfile() {
			CreateMap<Cart, GetCartByIdResult>();
		}
	}
}
