using Ambev.DeveloperEvaluation.Application.Users;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class UpdateUserHandlerTests
{
	private readonly IUserRepository _repository = Substitute.For<IUserRepository>();
	private readonly IMapper _mapper = Substitute.For<IMapper>();
	private readonly IPasswordHasher _passwordHasher = Substitute.For<IPasswordHasher>();
	private readonly UpdateUserHandler _handler;
	private readonly Faker _faker = new();

	public UpdateUserHandlerTests()
	{
		_handler = new UpdateUserHandler(_repository, _mapper, _passwordHasher);
	}

	[Fact]
	public async Task Handle_Should_Update_User_When_Valid_Command()
	{
		// Arrange
		var fakeUser = new Faker<User>()
			.RuleFor(u => u.Id, f => f.Random.Guid())
			.RuleFor(u => u.Username, f => f.Internet.UserName())
			.RuleFor(u => u.Email, f => f.Internet.Email())
			.RuleFor(u => u.Password, f => f.Internet.Password())
			.RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
			.RuleFor(u => u.Status, f => f.PickRandom<UserStatus>())
			.RuleFor(u => u.Role, f => f.PickRandom<UserRole>())
			.RuleFor(u => u.Name, f => new Name { FirstName = f.Name.FirstName(), LastName = f.Name.LastName() })
			.RuleFor(u => u.Address, f => new Address
			{
				Street = f.Address.StreetName(),
				City = f.Address.City(),
				Number = f.Random.Int(1, 9999),
				ZipCode = f.Address.ZipCode(),
				Lat = Convert.ToString(f.Address.Latitude()),
				Long = Convert.ToString(f.Address.Longitude())

			});

		var fakeCommand = new Faker<UpdateUserCommand>()
			.RuleFor(c => c.Id, f => fakeUser.Generate().Id)
			.RuleFor(x => x.Username, f => fakeUser.Generate().Username)
			.RuleFor(x => x.Password, f=> $"Test@#${f.Random.Number(1, 888)}")
			.RuleFor(x => x.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
				.RuleFor(x => x.Email, f => f.Internet.Email())
				.RuleFor(x => x.Name, f => new Faker<Name>()
					.RuleFor(x => x.FirstName, f => f.Name.FirstName())
					.RuleFor(x => x.LastName, f => f.Name.LastName()).Generate())
				.RuleFor(x => x.Address, f => new Faker<AddressDto>()
					.RuleFor(x => x.City, f => f.Address.City())
					.RuleFor(x => x.Street, f => f.Address.StreetName())
					.RuleFor(x => x.Number, f => int.Parse(f.Address.BuildingNumber()))
					.RuleFor(x => x.Zipcode, f => f.Address.ZipCode())
					.RuleFor(x => x.Geolocation, f => new Faker<GeolocationDto>()
						.RuleFor(x => x.Lat, f => Convert.ToString(f.Address.Latitude()))
						.RuleFor(x => x.Long, f => Convert.ToString(f.Address.Longitude()))
					).Generate())
				.RuleFor(x => x.Status, f => f.Random.Enum<UserStatus>(UserStatus.Unknown))
				.RuleFor(x => x.Role, f => f.Random.Enum<UserRole>(UserRole.None))
			.Generate();

		var expectedUser = fakeUser.Generate();
		_mapper.Map<User>(fakeCommand).Returns(expectedUser);
		_passwordHasher.HashPassword(fakeCommand.Password).Returns("hashed_password");
		expectedUser.Password = "hashed_password";
		_repository.UpdateUser(expectedUser).Returns(Task.FromResult(expectedUser));
		_mapper.Map<UpdateUserResult>(expectedUser).Returns(new UpdateUserResult
		{
			Id = expectedUser.Id,
			Username = expectedUser.Username,
			Email = expectedUser.Email,
			Phone = expectedUser.Phone,
			Status = expectedUser.Status,
			Role = expectedUser.Role,
			Name = expectedUser.Name,
			Address = new AddressDto
			{
				City = expectedUser.Address.City,
				Number = expectedUser.Address.Number,
				Street = expectedUser.Address.Street,
				Zipcode = expectedUser.Address.ZipCode,
				Geolocation = new GeolocationDto
				{
					Lat = expectedUser.Address.Lat,
					Long = expectedUser.Address.Long
				},
			}
		});

		// act
		var result = await _handler.Handle(fakeCommand, CancellationToken.None);

		// assert
		result.Should().NotBeNull();
		result.Id.Should().Be(expectedUser.Id);
		result.Username.Should().Be(expectedUser.Username);
		result.Email.Should().Be(expectedUser.Email);
		result.Phone.Should().Be(expectedUser.Phone);
		result.Status.Should().Be(expectedUser.Status);
		result.Role.Should().Be(expectedUser.Role);
		result.Name.Should().BeEquivalentTo(expectedUser.Name);
		await _repository.Received(1).UpdateUser(expectedUser);
	}

	[Fact]
	public async Task Handle_Should_Throw_ValidationException_When_Command_Is_Invalid()
	{
		// Arrange
		var invalidCommand = new UpdateUserCommand();

		// Act
		var act = async () => await _handler.Handle(invalidCommand, CancellationToken.None);

		// Assert
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}
}
