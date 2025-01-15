using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
	public class GetAllProductsResult
	{
		public IEnumerable<Product> Data { get; set; }
		public int Page {  get; set; }
        public int Limit { get; set; }
        public int TotalPages { get; set; }

    }
}
