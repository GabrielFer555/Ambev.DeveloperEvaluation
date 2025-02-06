namespace Ambev.DeveloperEvaluation.Domain.Exceptions
{
    public class UserNotFoundException:NotFoundException
    {
        public UserNotFoundException(Guid id) : base($"User ({id}) Not Found") { }
    }
}
