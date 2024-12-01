Feature: CreateBooking
  Scenario: Create a new booking
    Given the API endpoint "https://restful-booker.herokuapp.com/booking"
    And the request body contains the following JSON:
    """
    {
    "firstname" : "Jim",
    "lastname" : "Brown",
    "totalprice" : 111,
    "depositpaid" : true,
    "bookingdates" : {
        "checkin" : "2018-01-01",
        "checkout" : "2019-01-01"
    },
    "additionalneeds" : "Breakfast"
}
    """
    When I send a POST request
    Then the response status code CreateBooking should be 200
    And the response should contain a newly created booking
