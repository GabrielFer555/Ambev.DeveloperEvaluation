using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategories
{
	public class GetProductsByCategoriesResult
	{
		public List<Product> Products { get; set; } = new List<Product>();
		public int Page {  get; set; }
		public int Limit { get; set; }
        public int TotalPages { get; set; }
    }
}
