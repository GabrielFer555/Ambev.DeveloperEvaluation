

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
	public class UpdateOrderRequestProfile:Profile
	{
		public UpdateOrderRequestProfile() { 
			CreateMap<UpdateOrderRequest, UpdateOrderCommand>();
			CreateMap<UpdateOrderResult, UpdateOrderResponse>();
		}
	}
}
