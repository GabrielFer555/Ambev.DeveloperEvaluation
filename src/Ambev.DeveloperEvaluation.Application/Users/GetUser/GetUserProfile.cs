namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

public class GetUserProfile : Profile
{
    public GetUserProfile()
    {
        CreateMap<User, GetUserResult>().ForPath(x => x.Address, opt => opt.MapFrom(src => new AddressDto(src.Address.City,
			src.Address.Street,
			src.Address.Number,
			src.Address.ZipCode,
			new GeolocationDto(src.Address.Lat, src.Address.Long)))); ;
    }
}
