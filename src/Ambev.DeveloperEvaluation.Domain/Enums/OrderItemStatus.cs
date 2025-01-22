using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Enums
{
	public enum OrderItemStatus
	{
		[Description("Active")]
        Active=0,
		[Description("Canceled")]
		Canceled =1
    }
}
