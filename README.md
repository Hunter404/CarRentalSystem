# Code Test for Candidates

## Scenario
As a member of a team developing a versatile car rental system, your task will involve implementing a specific part of the business logic and corresponding test cases. This system must cater to a variety of customer requirements, encompassing data storage and UI specifications. Your test cases will help validate the functionality of the business logic as per the details specified below.

## Specification
The car rental system categorizes vehicles into three types: Small Car, Combi, and Truck. The system may include additional categories in the future. Each rental is unique and can be identified by a booking number, with each rental referencing only a single car.

### Rental Rates
Rental rates are calculated differently based on the car category, using two varying input parameters: `baseDayRental` and `baseKmPrice`.

- **Small Car**: Price = `baseDayRental` * `numberOfDays`
- **Combi**: Price = (`baseDayRental` * `numberOfDays` * 1.3) + (`baseKmPrice` * `numberOfKm`)
- **Truck**: Price = (`baseDayRental` * `numberOfDays` * 1.5) + (`baseKmPrice` * `numberOfKm` * 1.5)

## Use Case
Implement the following use cases:

### Registration of Car Delivery
Upon handing over a car to a customer, the car rental company registers the following details:
- Booking number
- Registration number
- Customer's social security number
- Car category
- Date and time of pick-up
- Current odometer reading on the car (in km)

### Registration of Returned Car
When a car is returned, the rental company's agent records the following information:
- Booking number
- Date and time of return
- Current odometer reading on the car (in km)

After registration, the system calculates the rental period's cost using the above-mentioned formulas.

## Your Task
Implement the provided use cases and corresponding test cases. A simple console application should suffice for running the tests, but a test framework is also acceptable.

## Handling Ambiguities
In case of any ambiguities in the specifications, use your best judgement to interpret and make necessary assumptions. Ensure to present these assumptions to us.

## Presentation
Prepare to present, justify, and discuss your solution with us. You should be ready to discuss your implementation and answer questions about your approach.
