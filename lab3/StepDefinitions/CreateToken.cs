using RestSharp;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using SpecFlow.Internal.Json;
using Newtonsoft.Json;
using Gherkin;

[Binding]
public class CreateToken
{
    public static string token;
    private RestClient client;
    private RestResponse response;

    [Given(@"the base URL is ""(.*)""")]
    public void GivenTheBaseURLIs(string url)
    {
        client = new RestClient(url);
    }

    [When(@"I send a POST request to ""(.*)"" with the following data:")]
    public void WhenISendAPOSTRequestToWithTheFollowingData(string resource, Table table)
    {
        var request = new RestRequest(resource, Method.Post);

        var requestBody = new
        {
            username = table.Rows[0][0],
            password = table.Rows[0][1]
        };

        string jsonBody = JsonConvert.SerializeObject(requestBody);

        request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

        response = client.Execute(request);
    }

    [Then(@"the response status code with token should be (\d+)")]
    public void ThenTheResponseStatusCodeShouldBe(int statusCode)
    {
        Assert.AreEqual(statusCode, (int)response.StatusCode);
    }

    [Then(@"the response should contain ""(.*)""")]
    public void ThenTheResponseShouldContain(string content)
    {
        var jsonResponse = JObject.Parse(response.Content);
        token = jsonResponse[content].ToString();
        Assert.IsTrue(jsonResponse[content] != null);
    }
}

