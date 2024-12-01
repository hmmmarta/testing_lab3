Feature: XKCD API
  Scenario: Retrieve XKCD comic by number
    Given the XKCD API endpoint "https://xkcd.com/{comicNumber}/info.0.json"
    When I send a GET request with comic number "615"
    Then the response status code for comic by number should be 200
    And the response should contain XKCD comic with number "615"

  Scenario: Retrieve the latest XKCD comic
    Given the XKCD API endpoint "https://xkcd.com/info.0.json"
    When I send a GET request without specifying a comic number
    Then the response status code for the latest comic should be 200
    And the response should contain the latest XKCD comic
