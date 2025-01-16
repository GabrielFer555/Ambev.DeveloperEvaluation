using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
	public class UpdateUserProfile:Profile
	{
		public UpdateUserProfile() {
			CreateMap<UpdateUserRequest, UpdateUserCommand>();
			CreateMap<UpdateUserResult, UpdateUserResponse>();
		}
	}
}
