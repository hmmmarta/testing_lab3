Feature: Ping
  Scenario: Perform a health check on the API
    Given the API endpoint for ping "https://restful-booker.herokuapp.com/ping"
    When I send a GET request to the endpoint
    Then the response status code for ping should be 201
    And the response for ping should contain "Created"
