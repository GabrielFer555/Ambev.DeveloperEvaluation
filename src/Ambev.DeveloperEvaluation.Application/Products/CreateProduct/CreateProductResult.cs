using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
	public class CreateProductResult
	{
		
			public int Id { get; set; }
			public decimal Price { get; set; }
			public string Title { get; set; } = default!;

			public string Description { get; set; } = default!;

			public string Category { get; set; } = default!;
			public string Image { get; set; } = default!;
			public ProductRatingDto Rating { get; set; } = default!;
	}

}
