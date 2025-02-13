namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
	public class UpdateUserHandler(IUserRepository repository, IMapper _mapper, IPasswordHasher passwordHasher) : IRequestHandler<UpdateUserCommand, UpdateUserResult>
	{
		public async Task<UpdateUserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateUserCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);

			if (!validationResult.IsValid)
				throw new ValidationException(validationResult.Errors);

			var user = _mapper.Map<User>(request);
			user.Password = passwordHasher.HashPassword(request.Password);

			var createdUser = await repository.UpdateUser(user);
			var result = _mapper.Map<UpdateUserResult>(createdUser);
			return result;
		}
	}
}
