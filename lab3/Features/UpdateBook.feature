Feature: UpdateBooking
  Scenario: Update an existing booking
    Given the API endpoint for update "https://restful-booker.herokuapp.com/booking/{id}"
    And the request body contains the following JSON for update:
    """
    {
        "firstname": "James",
        "lastname": "Brown",
        "totalprice": 111,
        "depositpaid": true,
        "bookingdates": {
            "checkin": "2018-01-01",
            "checkout": "2019-01-01"
        },
        "additionalneeds": "Breakfast"
    }
    """
    When I send a PUT request with ID
    Then the response status code for update should be 200
    And the response should contain the updated booking details
