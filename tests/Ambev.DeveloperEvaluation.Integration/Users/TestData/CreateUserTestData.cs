﻿using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Integration.Users.TestData
{
    public static class CreateUserTestData
    {
        private static readonly Faker<CreateUserRequest> createUserHandlerFaker = new Faker<CreateUserRequest>()
            .RuleFor(x => x.Username, f => f.Internet.UserName())
                .RuleFor(x => x.Password, f => UserUtility.GenerateValidPassword())
                .RuleFor(x => x.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Name, f => new Faker<Name>()
                    .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                    .RuleFor(x => x.LastName, f => f.Name.LastName()).Generate())
                .RuleFor(x => x.Address, f => new Faker<AddressDto>()
                    .RuleFor(x => x.City, f => f.Address.City())
                    .RuleFor(x => x.Street, f => f.Address.StreetName())
                    .RuleFor(x => x.Number, f => int.Parse(f.Address.BuildingNumber()))
                    .RuleFor(x => x.Zipcode, f => f.Address.ZipCode().ToString())
                    .RuleFor(x => x.Geolocation, f => new Faker<GeolocationDto>()
                        .RuleFor(x => x.Lat, f => Convert.ToString(f.Address.Latitude().ToString()))
                        .RuleFor(x => x.Long, f => f.Address.Longitude().ToString())
                    ).Generate())
                .RuleFor(x => x.Status, f => "Active")
                .RuleFor(x => x.Role, f => "Admin");

        public static CreateUserRequest GenerateValidCommand()
        {
            return createUserHandlerFaker.Generate();
        }
    }
}
