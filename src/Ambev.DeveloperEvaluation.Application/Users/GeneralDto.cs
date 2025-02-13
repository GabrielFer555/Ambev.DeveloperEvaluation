namespace Ambev.DeveloperEvaluation.Application.Users
{
	public record GeolocationDto
	{
		public string Lat { get; set; } = string.Empty;
		public string Long { get; set; } = string.Empty ;
		public GeolocationDto()
		{
			
		}

		public GeolocationDto(string Lat, string Long)
		{
			this.Lat = Lat;
			this.Long = Long;
		}
	};


	public record AddressDto
	{
		public string City { get; set; } = string.Empty;
		public string Street { get; set; } = string.Empty ;
		public int Number { get; set; }
		public string Zipcode { get; set; } = string.Empty;
		public GeolocationDto Geolocation { get; set; } = default!;
		public AddressDto()
		{
			
		}

		public AddressDto(string City, string Street, int Number, string Zipcode, GeolocationDto Geolocation)
		{
			this.City = City;
			this.Street = Street;
			this.Number = Number;
			this.Zipcode = Zipcode;
			this.Geolocation = Geolocation;
		}
	}
	public class NameValidator : AbstractValidator<Name>
	{
		public NameValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty();
			RuleFor(x => x.LastName).NotEmpty();
		}
		public class GeolocationDtoValidator : AbstractValidator<GeolocationDto>
		{
			public GeolocationDtoValidator()
			{
				RuleFor(x => x.Lat)
					.NotEmpty().WithMessage("Latitude is required.");

				RuleFor(x => x.Long)
					.NotEmpty().WithMessage("Longitude is required.");
			}
		}

		public class AddressDtoValidator : AbstractValidator<AddressDto>
		{
			public AddressDtoValidator()
			{
				RuleFor(x => x.City)
					.NotEmpty().WithMessage("City is required.")
					.MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

				RuleFor(x => x.Street)
					.NotEmpty().WithMessage("Street is required.")
					.MaximumLength(200).WithMessage("Street cannot exceed 200 characters.");

				RuleFor(x => x.Number)
					.GreaterThan(0).WithMessage("Number must be greater than zero.");

				RuleFor(x => x.Zipcode)
					.NotEmpty().WithMessage("Zipcode is required.")
					.Matches(@"^\d{5}(-\d{4})?$").WithMessage("Zipcode must be in the format 12345 or 12345-6789.");

				RuleFor(x => x.Geolocation)
					.NotNull().WithMessage("Geolocation is required.")
					.SetValidator(new GeolocationDtoValidator());
			}
		}
	}
}
