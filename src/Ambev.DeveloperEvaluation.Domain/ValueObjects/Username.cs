namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
	public record Name
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public Name()
		{
			
		}

		public Name(string FirstName, string LastName)
		{
			this.FirstName = FirstName;
			this.LastName = LastName;	
		}
	}; 
}
