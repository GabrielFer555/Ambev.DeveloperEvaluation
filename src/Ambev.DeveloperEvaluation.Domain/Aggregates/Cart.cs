using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates
{
	public class Cart :  Aggregate 
	{
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem> Products { get; set; }

    }
}
