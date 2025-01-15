using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
	public class DeleteProductCommand:IRequest<DeleteProductResult>
	{
        public int Id { get; set; }
    }
}
