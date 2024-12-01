using System.Net.Http;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace YourNamespace
{
    [Binding]
    public class XkcdApiSteps
    {
        private HttpClient httpClient = new HttpClient();
        private HttpResponseMessage response;
        private string endpoint;

        [Given(@"the XKCD API endpoint ""(.*)""")]
        public void GivenTheXkcdApiEndpoint(string url)
        {
            endpoint = url;
        }

        [When(@"I send a GET request with comic number ""(.*)""")]
        public void WhenISendAGETRequestWithComicNumber(int comicNumber)
        {
            string requestUrl = endpoint.Replace("{comicNumber}", comicNumber.ToString());
            response = httpClient.GetAsync(requestUrl).Result;
        }

        [When(@"I send a GET request without specifying a comic number")]
        public void WhenISendAGETRequestWithoutSpecifyingComicNumber()
        {
            response = httpClient.GetAsync(endpoint).Result;
        }

        [Then(@"the response status code for comic by number should be 200")]
        public void ThenTheResponseStatusCodeForComicByNumberShouldBe200()
        {
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Then(@"the response should contain XKCD comic with number ""(.*)""")]
        public void ThenTheResponseShouldContainXkcdComicWithNumber(int expectedComicNumber)
        {
            if (response.IsSuccessStatusCode)
            {
                // Отримайте вміст відповіді та перевірте, чи містить він номер коміксу
                var content = response.Content.ReadAsStringAsync().Result;
                Assert.True(content.Contains($"\"num\": {expectedComicNumber}"));
            }
            else
            {
                Assert.Fail("The request was not successful.");
            }
        }

        [Then(@"the response status code for the latest comic should be 200")]
        public void ThenTheResponseStatusCodeForTheLatestComicShouldBe200()
        {
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        // Крок, який перевіряє, що відповідь містить дані останнього коміксу
        [Then(@"the response should contain the latest XKCD comic")]
        public void ThenTheResponseShouldContainTheLatestXkcdComic()
        {
            // Перевіряємо, що запит успішний
            if (response.IsSuccessStatusCode)
            {
                // Отримуємо текстовий контент відповіді
                var content = response.Content.ReadAsStringAsync().Result;
                // Перевіряємо, що контент містить поле "num", яке вказує номер коміксу (для останнього коміксу номер може змінюватися)
                Assert.True(content.Contains("\"num\":"));
                Assert.True(content.Contains("\"num\": 3") || content.Contains("\"num\": 4") || content.Contains("\"num\": 5"));
            }
            else
            {
                // Якщо запит не був успішним, виконуємо невдалу перевірку
                Assert.Fail("The request was not successful.");
            }
        }

    }
}
