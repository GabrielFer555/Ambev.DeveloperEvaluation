using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions
{
    public class UserNotFoundException:NotFoundException
    {
        public UserNotFoundException(Guid id) : base($"User ({id}) Not Found") { }
    }
}
