using System.Net.Http.Headers;

namespace Ambev.DeveloperEvaluation.Integration.Orders
{
    public class OrderControllerTests
    {
        public HttpClient _httpClient { get; private set; }
        public WebApplicationEvaluationFactory _evaluationFactory { get; private set; }

        public OrderControllerTests()
        {
            _evaluationFactory = new WebApplicationEvaluationFactory();
            _httpClient = _evaluationFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthHandler.GenerateJwtToken());
        }
    }
}
