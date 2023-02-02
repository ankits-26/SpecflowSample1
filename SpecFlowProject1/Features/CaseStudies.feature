@CaseStudies

Feature: CaseStudies

@CaseStudies1
 Scenario: Search and verify Transportation and Logistics
    Given I launch Cognizant home page
    When I click on "Case Studies" link
    And Select Industry Type as "Transportation & Logistics"
    Then I verify all the cards are related to "Transportation & Logistics"

 @CaseStudies2
 Scenario: Search and verify Automotive
    Given I launch Cognizant home page
    When I click on "Case Studies" link
    And Select Industry Type as "Automotive"
    Then I verify all the cards are related to "Automotive"