using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace YourNamespace
{
    [Binding]
    public class RetrieveBookingIDsSteps
    {
        private HttpClient httpClient = new HttpClient();
        private HttpResponseMessage response;
        private List<int> bookingIDs;

        [When(@"I send a GET request to ""(.*)""")]
        public void WhenISendAGETRequestTo(string url)
        {
            response = httpClient.GetAsync(url).Result;
        }

        [Then(@"the response status code should be 200")]
        public void ThenTheResponseStatusCodeShouldBe200()
        {
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Then(@"the response should contain booking IDs")]
        public void ThenTheResponseShouldContainBookingIDs()
        {
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(content);

                Assert.IsNotNull(jsonArray);

                bool containsBookingIDs = jsonArray.Any(item => item["bookingid"] != null);
                Assert.IsTrue(containsBookingIDs, "The response does not contain booking IDs.");
            }
            else
            {
                Assert.Fail("The request was not successful.");
            }
        }



        [Given(@"a booking with firstname ""(.*)""")]
        public void GivenABookingWithFirstname(string firstname)
        {
        }

        [When(@"I send a GET request with firstName to ""(.*)""")]
        public void WhenISendAGETRequestWithFirstNameTo(string url)
        {;
            response = httpClient.GetAsync(url).Result;
        }

        [Then(@"the response status code by firstName should be 200")]
        public void ThenTheResponseStatusCodeByFirstnameShouldBe200()
        {
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Then(@"the response by firstName should contain booking IDs")]
        public void ThenTheResponseWithFirstnameShouldContainBookingIDs()
        {
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(content);

                Assert.IsNotNull(jsonArray);

                bool containsBookingIDs = jsonArray.Any(item => item["bookingid"] != null);
                Assert.IsTrue(containsBookingIDs, "The response does not contain booking IDs.");
            }
            else
            {
                Assert.Fail("The request was not successful.");
            }
        }

        [Given(@"a booking with checkin date ""(.*)"" and checkout date ""(.*)""")]
        public void GivenABookingWithCheckinDateAndCheckoutDate(string checkin, string checkout)
        {
        }

        [When(@"I send a GET request with data to ""(.*)""")]
        public void WhenISendAGETWithDataRequestTo(string url)
        {
            response = httpClient.GetAsync(url).Result;
        }

        [Then(@"the response status code by data should be 200")]
        public void ThenTheResponseStatusCodeWithDataShouldBe200()
        {
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Then(@"the response by data should contain booking IDs")]
        public void ThenTheResponseByDataShouldContainBookingIDs()
        {
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(content);

                Assert.IsNotNull(jsonArray);

                bool containsBookingIDs = jsonArray.Any(item => item["bookingid"] != null);
                Assert.IsTrue(containsBookingIDs, "The response does not contain booking IDs.");
            }
            else
            {
                Assert.Fail("The request was not successful.");
            }
        }
    }
}
