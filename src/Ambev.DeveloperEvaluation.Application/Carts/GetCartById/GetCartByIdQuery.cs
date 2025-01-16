using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartById
{
	public class GetCartByIdQuery:IRequest<GetCartByIdResult>
	{
        public int Id { get; set; }
    }
}
