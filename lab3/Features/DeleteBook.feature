Feature: DeleteBooking
  Scenario: Delete a booking
    Given the API endpoint for delete "https://restful-booker.herokuapp.com/booking/{id}"
    When I send a DELETE request with ID and a valid token
    Then the response status code should be 201
    And the response for delete should contain "Created"
