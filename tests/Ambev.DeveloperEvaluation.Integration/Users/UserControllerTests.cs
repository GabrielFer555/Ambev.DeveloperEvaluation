using Ambev.DeveloperEvaluation.Integration.Users.TestData;
using Ambev.DeveloperEvaluation.Integration.Users.Utility;
using Ambev.DeveloperEvaluation.WebApi.Common;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Ambev.DeveloperEvaluation.Integration.Users
{
    public class UserControllerTests
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationEvaluationFactory _evaluationFactory;

        public UserControllerTests()
        {
            this._evaluationFactory = new WebApplicationEvaluationFactory();
            this._httpClient = _evaluationFactory.CreateClient();
           _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthHandler.GenerateJwtToken());

        }

        [Fact]
        public async Task CreateUser_ValidUser_ReturnsObject()
        {
            //arrange
            var user = CreateUserTestData.GenerateValidCommand();

            //act
            var result = await _httpClient.PostAsJsonAsync("/api/Users", user);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<UserUtilityResponse>();

            //assert
            response.Should().NotBeNull();
            response.Id.Should().NotBeEmpty();
            response.Email.Should().Be(user.Email);
            response.Phone.Should().Be(user.Phone);
            response.Role.Should().Be(user.Role);
            response.Status.Should().Be(user.Status);
            response.Address.Should().BeEquivalentTo(user.Address);
            response.Name.Should().BeEquivalentTo(user.Name);
            response.Username.Should().Be(user.Username);
            response.Password.Should().NotBe(user.Password); 
        }
        [Fact]
        public async Task CreateUser_InvalidUser_ReturnsException()
        {
            //arrange
            var user = CreateUserTestData.GenerateValidCommand();
            user.Email = String.Empty;

            //act
            var result = await _httpClient.PostAsJsonAsync("/api/Users", user);

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateUser_InvalidUser_ThrowsException()
        {
            //arrange
            
            var updateData = UpdateUserTestData.GenerateValidData();
            
            var responseFromCreation = await CreateUser();
            updateData.Id = responseFromCreation!.Id;
            updateData.Password = string.Empty;

            //act
            var updateUserRequest = await _httpClient.PutAsJsonAsync($"/api/Users/{responseFromCreation.Id}", updateData);

            //assert
            updateUserRequest.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

        }
        [Fact]
        public async Task UpdateUser_NonExistentUser_ThrowsException()
        {
            //arrange

            var updateData = UpdateUserTestData.GenerateValidData();
  
            //act
            var updateUserRequest = await _httpClient.PutAsJsonAsync($"/api/Users/{updateData.Id}", updateData);

            //assert
            updateUserRequest.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        }
        [Fact]
        public async Task DeleteUser_ValidUser_ReturnsOk()
        {

            //arrange
            var userCreated = await CreateUser();

            //act
            var deleteUserRequest = await _httpClient.DeleteAsync($"/api/Users/{userCreated.Id}");
            deleteUserRequest.EnsureSuccessStatusCode();
            var deleteUserResponse = await deleteUserRequest.Content.ReadFromJsonAsync<ApiResponse>();

            var getUser = await _httpClient.GetAsync($"/api/Users/{userCreated.Id}");
            getUser.EnsureSuccessStatusCode();
            var getUserResponse = await getUser.Content.ReadFromJsonAsync<UserUtilityResponse>();

            //assert
            deleteUserResponse.Should().NotBeNull();
            getUserResponse.Should().NotBeNull();
            deleteUserResponse.Message.Should().Be("User deactivated successfully");
            getUser.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            getUserResponse.Status.Should().Be("Inactive");

        }
        [Fact]
        public async Task DeleteUser_InvalidUser_ThrowsException()
        {

            //arrange
            var randomGuid = Guid.NewGuid().ToString();

            //act
            var deleteUserRequest = await _httpClient.DeleteAsync($"/api/Users/{randomGuid}");

            //assert
            deleteUserRequest.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        }

        private async Task<UserUtilityResponse> CreateUser()
        {
            var user = CreateUserTestData.GenerateValidCommand();

            var result = await _httpClient.PostAsJsonAsync("/api/Users", user);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<UserUtilityResponse>();
            return response!;
        }
    } 
    }

