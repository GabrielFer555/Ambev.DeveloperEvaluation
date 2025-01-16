using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Validation;
using static Ambev.DeveloperEvaluation.Application.Users.NameValidator;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
	public class UpdateUserCommandValidator:AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserCommandValidator()
		{
			RuleFor(user => user.Id).NotEmpty().WithMessage("User Id must be informed");
			RuleFor(user => user.Email).SetValidator(new EmailValidator());
			RuleFor(user => user.Username).NotEmpty().Length(3, 50);
			RuleFor(user => user.Password).SetValidator(new PasswordValidator());
			RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
			RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
			RuleFor(user => user.Role).NotEqual(UserRole.None);
			RuleFor(user => user.Address).SetValidator(new AddressDtoValidator());
			RuleFor(user => user.Name).SetValidator(new NameValidator());
		}
	}
}
