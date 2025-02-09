﻿using Ambev.DeveloperEvaluation.Application.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers
{
	public class GetAllUsersHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, GetAllUsersResult>
	{
		public async Task<GetAllUsersResult> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
		{
			var validator = new GetAllUsersValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);

			if (!validationResult.IsValid)
				throw new ValidationException(validationResult.Errors);

			int page = request._Page??1;
			int limit = request._Limit ?? 10; 

			var users = await repository.GetAllAsync(page, limit, cancellationToken);
			var totalPages = await repository.GetTotalPages(limit);


			List<GetUserResult> usersResult = new();
			foreach (var user in users) {
				var userConverted = mapper.Map<GetUserResult>(user);
				usersResult.Add(userConverted);
			}

			return new GetAllUsersResult
			{
				Data = usersResult,
				TotalPages = totalPages,
				Limit = limit,
				Page = page
			};
		}
	}
}
