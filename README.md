# Airline Management System

A console-based Airline Management System built with C#.

I built this project to practice C# in a more realistic way instead of working with small separate exercises. The system manages the main operations of an airline, including passengers, employees, aircrafts, flights, reservations, seats, baggage, and payments.

The project is organized into different folders and services so that each part of the system has its own responsibility.

## What The Project Does

The system allows the user to manage an airline through a console menu.

### Passengers

* Add a new passenger
* View passenger information
* Search for a passenger by ID
* View all passengers
* Remove a passenger

### Employees

* Add and manage airline employees
* Store employee information such as ID, name, salary, and other details
* Support different employee types such as pilots and flight attendants

### Aircrafts

* Add aircrafts
* View aircraft information
* Search for an aircraft by ID
* View available aircrafts

### Flights

* Create flights using an aircraft and two airports
* Set departure and arrival times
* Set the base price
* Check flight information
* Search for flights

### Reservations

* Create a reservation for a passenger
* Select a flight and seat
* Calculate the reservation price
* Cancel reservations
* View reservation information
* Check reservation status

### Seats

The system supports different seat classes and uses the seat class and price multiplier when calculating the final reservation price.

### Baggage

Passengers can have baggage with information such as weight and baggage type.

### Payments

The project includes different payment methods:

* Cash
* Credit Card
* PayPal

The payment system checks that the payment amount is valid and matches the reservation price before processing the payment.

## Project Structure

The project is divided into several parts:

```text
AirlineManagementSystem
│
├── Data
│   └── Stores the application's data
│
├── ENUMS
│   └── Contains the different system enums
│
├── Interfaces
│   └── Defines the contracts for services
│
├── Models
│   └── Contains the main entities of the system
│
├── Payments
│   └── Contains the different payment methods
│
├── Services
│   └── Contains the business logic
│
└── Program.cs
    └── Starts the application and handles the console flow
```

## How The Program Runs

When the application starts, the user is presented with the main menu.

From there, the user can choose the operation they want to perform.

A typical flow can be:

```text
Start Application
       │
       ▼
   Main Menu
       │
       ├── Manage Passengers
       │
       ├── Manage Employees
       │
       ├── Manage Aircrafts
       │
       ├── Manage Airports
       │
       ├── Manage Flights
       │
       ├── Manage Reservations
       │
       ├── Manage Payments
       │
       └── Reports / Search
```

For example, creating a reservation can follow this flow:

```text
Select Reservation
        │
        ▼
Select Passenger
        │
        ▼
Select Flight
        │
        ▼
Select Available Seat
        │
        ▼
Calculate Price
        │
        ▼
Create Reservation
        │
        ▼
Choose Payment Method
        │
        ▼
Process Payment
```

The application validates user input during these operations so invalid data does not directly break the program.

## Technologies Used

* C#
* .NET
* Object-Oriented Programming
* Interfaces
* Inheritance
* Polymorphism
* Encapsulation
* SOLID Principles
* Collections
* Dictionaries
* Lists
* Enums
* Structs
* Exception Handling
* Console Application

## Why I Built This Project

The main goal of this project was to bring together the C# & OOP concepts I learned and use them in one complete application.

Instead of creating separate examples for classes, inheritance, collections, interfaces, exception handling, and other topics, I wanted to see how these concepts work together inside a real system.

The project also gave me practice with organizing a larger C# application into models, interfaces, services, and other separate components.

## How To Run

Clone the repository:

```bash
git clone <your-repository-url>
```

Open the project in Visual Studio and run the application.

You can also run it from the terminal:

```bash
dotnet run
```

The application will start from the console and display the main menu.

## Future Improvements

This project is currently a console-based application. Possible future improvements include:

* Connecting the system to a real database
* Building an ASP.NET Core Web API
* Adding authentication and authorization
* Creating a web-based frontend
* Using Entity Framework Core
* Adding automated tests

## Author

Abdelhay Mohammed

This project was created as a C# & OOP practice project focused on applying object-oriented programming and building a complete console-based system.
