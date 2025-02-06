namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers
{
    public class GetAllUsersQuery:IRequest<GetAllUsersResult>
    {
		public int? _Page { get; set; }
		public int? _Limit { get; set; }
	}
}
