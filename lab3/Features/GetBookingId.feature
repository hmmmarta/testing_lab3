Feature: Get Booking IDs
  In order to retrieve booking IDs
  As a user of the API
  I want to get booking IDs with optional search criteria

  Scenario: Retrieve all booking IDs
    When I send a GET request to "https://restful-booker.herokuapp.com/booking"
    Then the response status code should be 200
    And the response should contain booking IDs

  Scenario: Retrieve booking IDs by firstname
    Given a booking with firstname "John"
    When I send a GET request with firstName to "https://restful-booker.herokuapp.com/booking?firstname=John"
    Then the response status code by firstName should be 200
    And the response by firstName should contain booking IDs

  Scenario: Retrieve booking IDs by checkin/checkout date
    Given a booking with checkin date "2013-01-15" and checkout date "2023-01-20"
    When I send a GET request with data to "https://restful-booker.herokuapp.com/booking?checkin=2013-01-15&checkout=2023-01-20"
    Then the response status code by data should be 200
    And the response by data should contain booking IDs