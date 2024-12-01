Feature: Authentication with Restful Booker API

  Scenario: Authenticate user and get a token
    Given the base URL is "https://restful-booker.herokuapp.com"
    When I send a POST request to "/auth" with the following data:
      | username |   password  |
      | admin    | password123 |
    Then the response status code with token should be 200
    And the response should contain "token"


