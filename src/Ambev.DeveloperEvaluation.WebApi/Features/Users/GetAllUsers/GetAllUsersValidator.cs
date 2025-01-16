namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
	public class GetAllUsersValidator:AbstractValidator<GetAllUsersRequest>
	{
		public GetAllUsersValidator()
		{
			RuleFor(x => x._Page).GreaterThanOrEqualTo(1);
			RuleFor(x => x._Limit).GreaterThanOrEqualTo(1);

		}
	}
}
