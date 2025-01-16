using Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
	public class GetAllUsersProfile:Profile
	{
		public GetAllUsersProfile() {
			CreateMap<GetAllUsersRequest, GetAllUsersQuery>();
			CreateMap<GetAllUsersResult, GetAllUsersResponse>();
		}
	}
}
