﻿namespace Ambev.DeveloperEvaluation.Domain.Exceptions
{
	public class NotFoundException:Exception
	{
		public NotFoundException(string message) : base(message) { }
		
		public NotFoundException(string entity, int id): base($"{entity} ({id}) Not Found") { }
	}
}
