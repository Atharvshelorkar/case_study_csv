Feature: CSV Comparison

  Scenario: Compare two CSV files
    Given I load expected file
    When I compare files
    Then comparison should complete
