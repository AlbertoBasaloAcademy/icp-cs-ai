# Rocket Management API Specification
## Problem Description
- As a travel operations manager, I want to **create and maintain rockets in AstroBookings** so that the application has an accurate catalog of available spacecraft.
- As a booking agent, I want to **view rocket details such as name, range, and passenger capacity** so that I can choose the right rocket for a trip.
- As a business administrator, I want to **ensure rocket data is valid and consistent** so that booking decisions are based on reliable information.

## Solution Overview
- Add a rocket management API endpoint to AstroBookings that supports basic management of rocket records.
- Represent each rocket with a name, a supported travel range, and a passenger capacity.
- Validate all incoming rocket data before it is accepted, ensuring only supported ranges are allowed and capacity stays within the defined limits.
- Store and return rocket information in a consistent format so it can be used by the rest of the application.

## Acceptance Criteria
- [ ] When a client creates a rocket with a valid name, valid range, and capacity between 1 and 10, the system shall store the rocket and return the created rocket details.
- [ ] When a client requests the list of rockets, the system shall return all stored rockets with their name, range, and capacity.
- [ ] When a client requests a specific existing rocket, the system shall return that rocket's details.
- [ ] When a client updates an existing rocket with valid data, the system shall persist the new values and return the updated rocket details.
- [ ] When a client deletes an existing rocket, the system shall remove it from the catalog and confirm the deletion.
- [ ] If a client submits a rocket with a range other than "suborbital", "orbital", "moon", or "mars", then the system shall reject the request and report the range as invalid.
- [ ] If a client submits a rocket with a capacity lower than 1 or higher than 10, then the system shall reject the request and report the capacity constraint.
- [ ] If a client requests, updates, or deletes a rocket that does not exist, then the system shall return a not found result.
- [ ] While processing rocket management requests, the system shall preserve the rocket name as required data for every stored rocket.
