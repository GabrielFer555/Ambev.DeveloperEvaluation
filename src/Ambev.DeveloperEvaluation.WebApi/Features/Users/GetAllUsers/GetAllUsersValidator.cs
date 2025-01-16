namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
	public class GetAllUsersValidator:AbstractValidator<GetAllUsersRequest>
	{
		public GetAllUsersValidator()
		{
			RuleFor(x => x._Page).Must(IfNotNullMustBePositive);
			RuleFor(x => x._Limit).Must(IfNotNullMustBePositive);

		}
		public bool IfNotNullMustBePositive(int? number)
		{
			if (number.HasValue)
			{
				return number >= 1;
			}
			else
			{
				return true;
			}
		}
	}
}
