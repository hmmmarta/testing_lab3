using System;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Net.Http.Headers;

namespace YourNamespace
{
    [Binding]
    public class CreateBookingSteps
    {
        public static string id;
        private HttpClient httpClient = new HttpClient();
        private HttpResponseMessage response;
        private string endpoint;
        private string requestBody;

        [Given(@"the API endpoint ""(.*)""")]
        public void GivenTheApiEndpoint(string url)
        {
            endpoint = url;
        }

        [Given(@"the request body contains the following JSON:")]
        public void GivenTheRequestBodyContainsTheFollowingJSON(string multilineText)
        {
            requestBody = multilineText;
        }

        [When(@"I send a POST request")]
        public void WhenISendAPOSTRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Content = new StringContent(requestBody);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = httpClient.Send(request);
        }

        [Then(@"the response status code CreateBooking should be 200")]
        public void ThenTheResponseStatusCodeShouldBe200()
        {
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Then(@"the response should contain a newly created booking")]
        public void ThenTheResponseShouldContainANewlyCreatedBooking()
        {
            if (response.IsSuccessStatusCode)
            {
                
                var content = response.Content.ReadAsStringAsync().Result;
                var bookingResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<BookingResponse>(content);
                id = (bookingResponse.bookingid).ToString();

                Assert.IsNotNull(bookingResponse);
                Assert.IsTrue(bookingResponse.bookingid > 0);
                Assert.IsNotNull(bookingResponse.booking);
                Assert.IsFalse(string.IsNullOrEmpty(bookingResponse.booking.firstname));
                Assert.IsFalse(string.IsNullOrEmpty(bookingResponse.booking.lastname));
                Assert.IsTrue(bookingResponse.booking.totalprice > 0);
                Assert.IsTrue(bookingResponse.booking.depositpaid);
                Assert.IsNotNull(bookingResponse.booking.bookingdates);
                Assert.IsTrue(DateTime.TryParse(bookingResponse.booking.bookingdates.checkin, out _));
                Assert.IsTrue(DateTime.TryParse(bookingResponse.booking.bookingdates.checkout, out _));
                Assert.IsFalse(string.IsNullOrEmpty(bookingResponse.booking.additionalneeds));
            }
            else
            {
                Assert.Fail("The request was not successful.");
            }
        }
    }

    public class BookingResponse
    {
        public int bookingid { get; set; }
        public Booking booking { get; set; }
    }

    public class Booking
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int totalprice { get; set; }
        public bool depositpaid { get; set; }
        public BookingDates bookingdates { get; set; }
        public string additionalneeds { get; set; }
    }

    public class BookingDates
    {
        public string checkin { get; set; }
        public string checkout { get; set; }
    }
}
