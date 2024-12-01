using System.Net.Http;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace YourNamespace
{
    [Binding]
    public class UpdateBookingSteps
    {
        private HttpClient httpClient = new HttpClient();
        private HttpResponseMessage response;
        private string endpoint;
        private string requestBody;

        [Given(@"the API endpoint for update ""(.*)""")]
        public void GivenTheApiEndpoint(string url)
        {
            endpoint = url;
        }

        [Given(@"the request body contains the following JSON for update:")]
        public void GivenTheRequestBodyContainsTheFollowingJSON(string multilineText)
        {
            requestBody = multilineText;
        }

        [When(@"I send a PUT request with ID")]
        public async Task WhenISendAPUTRequestWithID()
        {
            var request = new HttpRequestMessage(HttpMethod.Put, endpoint.Replace("{id}", CreateBookingSteps.id));
            request.Content = new StringContent(requestBody);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content.Headers.Add("Cookie", $"token={CreateToken.token}");
            response = httpClient.Send(request);
        }

        [Then(@"the response status code for update should be 200")]
        public void ThenTheResponseStatusCodeShouldBe200()
        {
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Then(@"the response should contain the updated booking details")]
        public async Task ThenTheResponseShouldContainTheUpdatedBookingDetails()
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var updatedBooking = JsonConvert.DeserializeObject<Booking>(content);

                // Перевірка оновлених деталей бронювання
                Assert.AreEqual("James", updatedBooking.firstname);
                Assert.AreEqual("Brown", updatedBooking.lastname);
                Assert.AreEqual(111, updatedBooking.totalprice);
                Assert.IsTrue(updatedBooking.depositpaid);
                Assert.AreEqual("2018-01-01", updatedBooking.bookingdates.checkin);
                Assert.AreEqual("2019-01-01", updatedBooking.bookingdates.checkout);
                Assert.AreEqual("Breakfast", updatedBooking.additionalneeds);
            }
            else
            {
                Assert.Fail("The request was not successful.");
            }
        }
    }
}
