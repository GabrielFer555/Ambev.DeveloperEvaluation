using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<User, GetUserResult>().ForPath(x => x.Address, opt => opt.MapFrom(src => new AddressDto(src.Address.City,
			src.Address.Street,
			src.Address.Number,
			src.Address.ZipCode,
			new GeolocationDto(src.Address.Lat, src.Address.Long)))); ;
    }
}
