﻿

using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class CreateCartRequestProfile:Profile
	{
		public CreateCartRequestProfile() {
			CreateMap<CreateCartRequest, CreateCartCommand>();
			CreateMap<CreateCartResult, CreateCartResponse>();
		}
	}
}
