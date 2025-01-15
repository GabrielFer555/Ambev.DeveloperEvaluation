using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
	public class GetProductsByIdProfile:Profile
	{
		public GetProductsByIdProfile() { 
			CreateMap<Product, GetProductsByIdResult>().ForMember(x=> x.Rating, opt => opt.MapFrom(x=> x.ProductRating));
		}
	}
}
