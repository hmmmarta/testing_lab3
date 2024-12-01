using System.Net.Http;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace YourNamespace
{
    [Binding]
    public class PingSteps
    {
        private HttpClient httpClient = new HttpClient();
        private HttpResponseMessage response;
        private string endpoint;

        [Given(@"the API endpoint for ping ""(.*)""")]
        public void GivenTheApiEndpoint(string url)
        {
            endpoint = url;
        }

        [When(@"I send a GET request to the endpoint")]
        public async Task WhenISendAGETRequestToTheEndpoint()
        {
            response = await httpClient.GetAsync(endpoint);
        }

        [Then(@"the response status code for ping should be 201")]
        public void ThenTheResponseStatusCodeShouldBe200()
        {
            Assert.AreEqual(201, (int)response.StatusCode);
        }

        [Then(@"the response for ping should contain ""Created""")]
        public async Task ThenTheResponseShouldContainCreated()
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
