namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
	public class UpdateUserProfile:Profile
	{
		public UpdateUserProfile()
		{
			CreateMap<UpdateUserCommand, User>().ForMember(x => x.Address, opt => opt.MapFrom(src => new Address
			{
				Lat = src.Address.Geolocation.Lat,
				Long = src.Address.Geolocation.Long,
				City = src.Address.City,
				Number = src.Address.Number,
				Street = src.Address.Street,
				ZipCode = src.Address.Zipcode
			}));

			CreateMap<User, UpdateUserResult>().ForPath(x => x.Address, opt => opt.MapFrom(src => new AddressDto(src.Address.City,
			src.Address.Street,
			src.Address.Number,
			src.Address.ZipCode,
			new GeolocationDto(src.Address.Lat, src.Address.Long))));
		}
	}
}
