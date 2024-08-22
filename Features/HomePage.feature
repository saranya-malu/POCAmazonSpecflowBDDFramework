Feature: HomePage

Navigate to home page

@login
Scenario: Launch the browser and navigate to home page
	Given I navigate to home page
	Then I should see the page Title
	When I search for 'laptop i7'

@login1
Scenario: Amazon usecases
	Given I navigate to home page
	When I search for 'laptop i7'
	Then I click on 'laptop i7' option
	When I select brand name 'lenovo'
	Then I click on Add to cart button
	Then I click on cart button
	Then I click on Payment method button
	When I enter username and password
	Then I choose delivery address
	Then I select a payment method
	


