using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Integration.Users.TestData
{
    public static class UserUtility
    {
        public static string GenerateValidPassword()
        {
            var faker = new Faker();
            var upper = faker.Random.Char('A', 'Z').ToString();
            var lower = faker.Random.Char('a', 'z').ToString();
            var digit = faker.Random.Char('0', '9').ToString();
            var special = faker.PickRandom("!?.@#$%^&+=");  // Special character

            var rest = faker.Internet.Password(8, false);

            var password = upper + lower + digit + special + rest;

            return Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\!\?\*\.\@\#\$\%\^\&\+\=]).{8,}$")
                ? password
                : GenerateValidPassword(); // Recursively retry if needed
        }
    }
}
