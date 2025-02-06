namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
	public class UpdateUserRequestValidator:AbstractValidator<UpdateUserRequest>
	{
		public UpdateUserRequestValidator() {
			RuleFor(user => user.Id).NotEmpty().WithMessage("User Id must be informed");
			RuleFor(user => user.Email).SetValidator(new EmailValidator());
			RuleFor(user => user.Username).NotEmpty().Length(3, 50);
			RuleFor(user => user.Password).SetValidator(new PasswordValidator());
			RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
			RuleFor(user => user.Status).NotEmpty().NotEqual("Unknown").Must(EnumHelper.IsValidEnumDescription<UserStatus>).WithMessage("User Status must be a valid");
			RuleFor(user => user.Role).NotEmpty().NotEqual("None").Must(EnumHelper.IsValidEnumDescription<UserRole>).WithMessage("User Status must be a valid");
			RuleFor(user => user.Address).SetValidator(new AddressDtoValidator());
			RuleFor(user => user.Name).SetValidator(new NameValidator());
		}
	}
}
