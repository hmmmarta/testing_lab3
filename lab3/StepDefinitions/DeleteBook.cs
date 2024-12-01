using System.Net.Http;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace YourNamespace
{
    [Binding]
    public class DeleteBookingSteps
    {
        private HttpClient httpClient = new HttpClient();
        private HttpResponseMessage response;
        private string endpoint;

        [Given(@"the API endpoint for delete ""(.*)""")]
        public void GivenTheApiEndpoint(string url)
        {
            endpoint = url;
        }

        [When(@"I send a DELETE request with ID and a valid token")]
        public async Task WhenISendADELETERequestWithIDAndAValidToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, endpoint.Replace("{id}", "693"));

            // Додайте заголовок "Cookie" зі значенням токена
            request.Headers.Add("Cookie", $"token={CreateToken.token}");

            response = await httpClient.SendAsync(request);
        }

        [Then(@"the response status code should be 201")]
        public void ThenTheResponseStatusCodeShouldBe201()
        {
            Assert.AreEqual(201, (int)response.StatusCode);
        }

        [Then(@"the response for delete should contain ""Created""")]
        public async Task ThenTheResponseShouldContainOK()
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Assert.AreEqual("Created", content);
            }
            else
            {
                Assert.Fail("The request was not successful.");
            }
        }
    }
}
