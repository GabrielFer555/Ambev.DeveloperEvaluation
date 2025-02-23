﻿namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{

	public record Address
	{
		public string City { get; set; } = string.Empty;
		public string Street { get; set; } = string.Empty ;
		public int Number {  get; set; }
		public string ZipCode {  get; set; } = string.Empty ;
		public string Long { get; set; } = default!;
		public string Lat { get; set; } = default!;
		public Address() { }

		public Address(string city, string street, int number, string zipCode, string Long, string lat)
		{
			City = city;
			Street = street;
			Number = number;
			ZipCode = zipCode;
			this.Long = Long;
			Lat = lat;
		}

	}
}
