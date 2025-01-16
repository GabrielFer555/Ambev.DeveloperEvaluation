using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users
{
	public record GeolocationDto(string Lat, string Long);


	public record AddressDto(
		string City,
		string Street,
		int Number,
		string Zipcode,
		GeolocationDto Geolocation
	);
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
