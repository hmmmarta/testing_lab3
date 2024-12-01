Feature: PartialUpdateBooking
  Scenario: Partially update an existing booking
    Given the API endpoint for partial update "https://restful-booker.herokuapp.com/booking/{id}"
    And the request body for partial update contains the following JSON:
    """
    {
        "firstname": "James",
        "lastname": "Brown"
    }
    """
    When I send a PATCH request with ID
    Then the response status code for partial update should be 200
    And the response should contain the partial updated booking details
