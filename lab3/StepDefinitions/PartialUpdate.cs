using System.Net.Http;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Newtonsoft.Json;
using lab3.Features;

namespace YourNamespace
{
    [Binding]
    public class PartialUpdateBookingSteps
    {
        private HttpClient httpClient = new HttpClient();
        private HttpResponseMessage response;
        private string endpoint;
        private string requestBody;

        [Given(@"the API endpoint for partial update ""(.*)""")]
        public void GivenTheApiEndpoint(string url)
        {
            endpoint = url;
        }

        [Given(@"the request body for partial update contains the following JSON:")]
        public void GivenTheRequestBodyContainsTheFollowingJSON(string multilineText)
        {
            requestBody = multilineText;
        }

        [When(@"I send a PATCH request with ID")]
        public async Task WhenISendAPATCHRequestWithID()
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), endpoint.Replace("{id}", CreateBookingSteps.id));
            request.Content = new StringContent(requestBody);
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Cookie", $"token={CreateToken.token}");

            response = httpClient.Send(request);
        }

        [Then(@"the response status code for partial update should be 200")]
        public void ThenTheResponseStatusCodeShouldBe200()
        {
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Then(@"the response should contain the partial updated booking details")]
        public async Task ThenTheResponseShouldContainTheUpdatedBookingDetails()
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var updatedBooking = JsonConvert.DeserializeObject<Booking>(content);

                // Перевірка оновлених деталей бронювання
                Assert.AreEqual("James", updatedBooking.firstname);
                Assert.AreEqual("Brown", updatedBooking.lastname);
                // Додайте інші перевірки, які вам потрібні
            }
            else
            {
                Assert.Fail("The request was not successful.");
            }
        }
    }
}
