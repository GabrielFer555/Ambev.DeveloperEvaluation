using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Emit;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>().ForMember(x => x.Address, opt => opt.MapFrom(src => new Address
        {
            Lat = src.Address.Geolocation.Lat,
            Long = src.Address.Geolocation.Long,
            City = src.Address.City,
            Number = src.Address.Number,
            Street = src.Address.Street,
            ZipCode = src.Address.Zipcode
		}));
        CreateMap<User, CreateUserResult>().ForPath(x => x.Address, opt => opt.MapFrom(src => new AddressDto(src.Address.City,
			src.Address.Street,
			src.Address.Number,
		    src.Address.ZipCode,
			new GeolocationDto(src.Address.Lat, src.Address.Long))));
    }
}
