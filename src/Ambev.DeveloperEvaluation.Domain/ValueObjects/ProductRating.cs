using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
		public class ProductRating
		{
			public int Count { get; private set; } = default!;
			public double Rating { get; private set; }

			private ProductRating(int count, double rating)
			{
				this.Count = count;
				this.Rating = rating;
			}
			public static ProductRating Of(int count, double rating)
			{

				return new ProductRating(count, rating);
			}
		}
	}

