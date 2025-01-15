using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
	public class GetProductsByIdQuery:IRequest<GetProductsByIdResult>
	{
        public int Id { get; set; }
    }
}
