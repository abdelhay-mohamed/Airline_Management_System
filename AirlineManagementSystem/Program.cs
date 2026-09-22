using AirlineManagementSystem.Data;
using AirlineManagementSystem.ENUMS;
using AirlineManagementSystem.Interfaces;
using AirlineManagementSystem.Models;
using AirlineManagementSystem.Payments;
using AirlineManagementSystem.Services;
using AirlineManagementSystem.Utilities;

namespace AirlineManagementSystem
{
    public class Program
    {
        private static AirlineData _data = new AirlineData();
        private static PassengerService _passengerService = new PassengerService(_data);
        private static EmployeeService _employeeService = new EmployeeService(_data);
        private static AircraftService _aircraftService = new AircraftService(_data);
        private static FlightService _flightService = new FlightService(_data);
        private static ReservationService _reservationService = new ReservationService(_data);

        public static void Main()
        {
            Run();
        }

        private static void Run()
        {
            bool running = true;

            while (running)
            {
                try
                {
                    ShowMainMenu();
                    string choice = InputHelper.ReadString("Choose an option: ");
                    running = HandleMainChoice(choice);
                }
                catch (Exception exception)
                {
                    ShowError(exception);
                }
            }
        }

        private static void ShowMainMenu()
        {
            Console.Clear();
            PrintHeader("AIRLINE MANAGEMENT SYSTEM");
            Console.WriteLine("1 - Manage Passengers");
            Console.WriteLine();
            Console.WriteLine("2 - Manage Employees");
            Console.WriteLine();
            Console.WriteLine("3 - Manage Aircraft");
            Console.WriteLine();
            Console.WriteLine("4 - Manage Flights");
            Console.WriteLine();
            Console.WriteLine("5 - Manage Reservations");
            Console.WriteLine();
            Console.WriteLine("6 - Manage Payments");
            Console.WriteLine();
            Console.WriteLine("7 - Search");
            Console.WriteLine();
            Console.WriteLine("8 - Reports");
            Console.WriteLine();
            Console.WriteLine("9 - Exit");
            Console.WriteLine();
        }

        private static bool HandleMainChoice(string choice)
        {
            switch (choice)
            {
                case "1": PassengerMenu(); break;
                case "2": EmployeeMenu(); break;
                case "3": AircraftMenu(); break;
                case "4": FlightMenu(); break;
                case "5": ReservationMenu(); break;
                case "6": PaymentMenu(); break;
                case "7": SearchMenu(); break;
                case "8": ReportsMenu(); break;
                case "9": return false;
                default: ShowMessage("Invalid option."); break;
            }

            return true;
        }

        private static void PassengerMenu()
        {
            RunMenu(
                "PASSENGER MANAGEMENT",
                new[] { "Add Passenger", "View All Passengers", "Search Passenger By ID", "Search Passenger By Email", "Remove Passenger", "Back" },
                choice => choice switch
                {
                    1 => Execute(AddPassenger),
                    2 => Execute(ViewPassengers),
                    3 => Execute(SearchPassengerById),
                    4 => Execute(SearchPassengerByEmail),
                    5 => Execute(RemovePassenger),
                    6 => false,
                    _ => InvalidChoice()
                });
        }

        private static void AddPassenger()
        {
            PrintHeader("ADD PASSENGER");
            int id = IdGenerator.GenerateId();
            string name = InputHelper.ReadName("Name: ");
            string email = InputHelper.ReadEmail("Email: ");
            string phone = InputHelper.ReadPhone("Phone: ");
            string passport = InputHelper.ReadPassportNumber("Passport Number: ");
            string nationality = InputHelper.ReadNationality("Nationality: ");
            DateTime dateOfBirth = InputHelper.ReadDateOfBirth("Date Of Birth (yyyy-MM-dd): ");
            string country = InputHelper.ReadName("Address Country: ");
            string city = InputHelper.ReadName("Address City: ");
            string street = InputHelper.ReadString("Address Street: ");

            Passenger passenger = new Passenger(
                id,
                name,
                email,
                phone,
                passport,
                nationality,
                dateOfBirth,
                new Address(country, city, street));

            ShowMessage(_passengerService.AddPassenger(passenger)
                ? $"Passenger added successfully. ID: {id}"
                : "Passenger could not be added. Check the ID or email.");
        }

        private static void ViewPassengers()
        {
            PrintHeader("PASSENGERS");
            List<Passenger> passengers = _passengerService.GetAllPassengers();

            if (passengers.Count == 0)
            {
                ShowMessage("No passengers found.");
                return;
            }

            foreach (Passenger passenger in passengers)
            {
                PrintPassenger(passenger);
            }

            Pause();
        }

        private static void SearchPassengerById()
        {
            int id = InputHelper.ReadInt("Passenger ID: ");
            Passenger? passenger = _passengerService.GetPassengerById(id);
            PrintPassengerResult(passenger);
        }

        private static void SearchPassengerByEmail()
        {
            string email = InputHelper.ReadEmail("Passenger Email: ");
            Passenger? passenger = _passengerService.GetPassengerByEmail(email);
            PrintPassengerResult(passenger);
        }

        private static void PrintPassengerResult(Passenger? passenger)
        {
            if (passenger == null)
            {
                ShowMessage("Passenger not found.");
                return;
            }

            PrintPassenger(passenger);
            Pause();
        }

        private static void RemovePassenger()
        {
            int id = InputHelper.ReadInt("Passenger ID: ");
            bool removed = _passengerService.RemovePassenger(id);
            ShowMessage(removed ? "Passenger removed successfully." : "Passenger cannot be removed or was not found.");
        }

        private static void PrintPassenger(Passenger passenger)
        {
            WriteSpaced($"ID: {passenger.Id}");
            WriteSpaced($"Name: {passenger.Name}");
            WriteSpaced($"Email: {passenger.Email}");
            WriteSpaced($"Phone: {passenger.Phone}");
            WriteSpaced($"Passport: {passenger.PassportNumber}");
            WriteSpaced($"Nationality: {passenger.Nationality}");
            WriteSpaced($"Date Of Birth: {passenger.DateOfBirth:yyyy-MM-dd}");
            WriteSpaced($"Address: {passenger.Address.Country}, {passenger.Address.City}, {passenger.Address.Street}");
        }

        private static void EmployeeMenu()
        {
            RunMenu(
                "EMPLOYEE MANAGEMENT",
                new[] { "Add Pilot", "Add Flight Attendant", "View All Employees", "Search Employee By ID", "Search Employee By Email", "Remove Employee", "Back" },
                choice => choice switch
                {
                    1 => Execute(AddPilot),
                    2 => Execute(AddFlightAttendant),
                    3 => Execute(ViewEmployees),
                    4 => Execute(SearchEmployeeById),
                    5 => Execute(SearchEmployeeByEmail),
                    6 => Execute(RemoveEmployee),
                    7 => false,
                    _ => InvalidChoice()
                });
        }

        private static void AddPilot()
        {
            PrintHeader("ADD PILOT");
            int id = IdGenerator.GenerateId();
            string name = InputHelper.ReadName("Name: ");
            string email = InputHelper.ReadEmail("Email: ");
            string phone = InputHelper.ReadPhone("Phone: ");
            int employeeId = InputHelper.ReadPositiveInt("Employee ID: ");
            decimal salary = InputHelper.ReadPositiveDecimal("Salary: ");
            string license = InputHelper.ReadLicenseNumber("License Number: ");
            int experience = InputHelper.ReadNonNegativeInt("Years Of Experience: ");

            Pilot pilot = new Pilot(id, name, email, phone, employeeId, salary, license, experience);
            ShowMessage(_employeeService.AddEmployee(pilot)
                ? $"Pilot added successfully. ID: {id}"
                : "Pilot could not be added. Check the employee ID or email.");
        }

        private static void AddFlightAttendant()
        {
            PrintHeader("ADD FLIGHT ATTENDANT");
            int id = IdGenerator.GenerateId();
            string name = InputHelper.ReadName("Name: ");
            string email = InputHelper.ReadEmail("Email: ");
            string phone = InputHelper.ReadPhone("Phone: ");
            int employeeId = InputHelper.ReadPositiveInt("Employee ID: ");
            decimal salary = InputHelper.ReadPositiveDecimal("Salary: ");

            FlightAttendant attendant = new FlightAttendant(id, name, email, phone, employeeId, salary);
            int languageCount = InputHelper.ReadBoundedPositiveInt("Number Of Languages: ", 20);

            for (int i = 0; i < languageCount; i++)
            {
                attendant.Languages.Add(InputHelper.ReadName($"Language {i + 1}: "));
            }

            ShowMessage(_employeeService.AddEmployee(attendant)
                ? $"Flight attendant added successfully. ID: {id}"
                : "Flight attendant could not be added. Check the employee ID or email.");
        }

        private static void ViewEmployees()
        {
            PrintHeader("EMPLOYEES");
            List<Employee> employees = _employeeService.GetAllEmployees();

            if (employees.Count == 0)
            {
                ShowMessage("No employees found.");
                return;
            }

            foreach (Employee employee in employees)
            {
                PrintEmployee(employee);
            }

            Pause();
        }

        private static void SearchEmployeeById()
        {
            int id = InputHelper.ReadInt("Employee ID: ");
            PrintEmployeeResult(_employeeService.GetEmployeeById(id));
        }

        private static void SearchEmployeeByEmail()
        {
            string email = InputHelper.ReadEmail("Employee Email: ");
            PrintEmployeeResult(_employeeService.GetEmployeeByEmail(email));
        }

        private static void PrintEmployeeResult(Employee? employee)
        {
            if (employee == null)
            {
                ShowMessage("Employee not found.");
                return;
            }

            PrintEmployee(employee);
            Pause();
        }

        private static void RemoveEmployee()
        {
            int id = InputHelper.ReadInt("Employee ID: ");
            ShowMessage(_employeeService.RemoveEmployee(id)
                ? "Employee removed successfully."
                : "Employee not found.");
        }

        private static void PrintEmployee(Employee employee)
        {
            WriteSpaced($"ID: {employee.Id}");
            WriteSpaced($"Employee ID: {employee.EmployeeId}");
            WriteSpaced($"Name: {employee.Name}");
            WriteSpaced($"Email: {employee.Email}");
            WriteSpaced($"Phone: {employee.Phone}");
            WriteSpaced($"Salary: {employee.Salary:F2}");

            if (employee is Pilot pilot)
            {
                WriteSpaced("Type: Pilot");
                WriteSpaced($"License: {pilot.LicenseNumber}");
                WriteSpaced($"Experience: {pilot.YearsOfExperience} years");
            }
            else if (employee is FlightAttendant attendant)
            {
                WriteSpaced("Type: Flight Attendant");
                WriteSpaced($"Languages: {string.Join(", ", attendant.Languages)}");
            }

            Console.WriteLine();
        }

        private static void AircraftMenu()
        {
            RunMenu(
                "AIRCRAFT MANAGEMENT",
                new[] { "Add Aircraft", "View All Aircraft", "View Aircraft Seats", "Search Aircraft By ID", "Remove Aircraft", "Back" },
                choice => choice switch
                {
                    1 => Execute(AddAircraft),
                    2 => Execute(ViewAircraft),
                    3 => Execute(ViewAircraftSeats),
                    4 => Execute(SearchAircraft),
                    5 => Execute(RemoveAircraft),
                    6 => false,
                    _ => InvalidChoice()
                });
        }

        private static void AddAircraft()
        {
            PrintHeader("ADD AIRCRAFT");
            int id = IdGenerator.GenerateId();
            string model = InputHelper.ReadString("Model: ");
            int capacity = InputHelper.ReadBoundedPositiveInt("Capacity: ", 500);
            Aircraft aircraft = new Aircraft(id, model, capacity);
            BuildSeats(aircraft);

            ShowMessage(_aircraftService.AddAircraft(aircraft)
                ? $"Aircraft added successfully. ID: {id}"
                : "Aircraft could not be added.");
        }

        private static void BuildSeats(Aircraft aircraft)
        {
            for (int index = 0; index < aircraft.Capacity; index++)
            {
                string seatClass = GetSeatClass(index, aircraft.Capacity);
                decimal multiplier = GetPriceMultiplier(seatClass);
                string seatNumber = $"{(char)('A' + index % 6)}{index / 6 + 1}";
                aircraft.Seats[index] = new Seat(seatNumber, seatClass, true, multiplier);
            }
        }

        private static string GetSeatClass(int index, int capacity)
        {
            double position = (double)index / capacity;

            if (position < 0.10)
            {
                return SeatClass.FirstClass.ToString();
            }

            if (position < 0.30)
            {
                return SeatClass.Business.ToString();
            }

            return SeatClass.Economy.ToString();
        }

        private static decimal GetPriceMultiplier(string seatClass)
        {
            return seatClass switch
            {
                nameof(SeatClass.FirstClass) => 2.0m,
                nameof(SeatClass.Business) => 1.5m,
                _ => 1.0m
            };
        }

        private static void ViewAircraft()
        {
            PrintHeader("AIRCRAFT");
            List<Aircraft> aircrafts = _aircraftService.GetAllAircraft();

            if (aircrafts.Count == 0)
            {
                ShowMessage("No aircraft found.");
                return;
            }

            foreach (Aircraft aircraft in aircrafts)
            {
                WriteSpaced($"ID: {aircraft.Id}");
                WriteSpaced($"Model: {aircraft.Model}");
                WriteSpaced($"Capacity: {aircraft.Capacity}");
            }

            Pause();
        }

        private static void ViewAircraftSeats()
        {
            int id = InputHelper.ReadInt("Aircraft ID: ");
            Aircraft? aircraft = _aircraftService.GetAircraftById(id);

            if (aircraft == null)
            {
                ShowMessage("Aircraft not found.");
                return;
            }

            PrintHeader($"SEATS FOR {aircraft.Model}");

            foreach (Seat seat in aircraft.Seats)
            {
                Console.WriteLine($"{seat.SeatNumber} | {seat.Class} | {(seat.IsAvailable ? "Available" : "Occupied")} | Multiplier: {seat.PriceMultiplier:F1}");
            }

            Pause();
        }

        private static void SearchAircraft()
        {
            int id = InputHelper.ReadInt("Aircraft ID: ");
            Aircraft? aircraft = _aircraftService.GetAircraftById(id);

            if (aircraft == null)
            {
                ShowMessage("Aircraft not found.");
                return;
            }

            WriteSpaced($"ID: {aircraft.Id}");
            WriteSpaced($"Model: {aircraft.Model}");
            WriteSpaced($"Capacity: {aircraft.Capacity}");
            Pause();
        }

        private static void RemoveAircraft()
        {
            int id = InputHelper.ReadInt("Aircraft ID: ");
            ShowMessage(_aircraftService.RemoveAircraft(id)
                ? "Aircraft removed successfully."
                : "Aircraft could not be removed. It may not exist or may be used by a flight.");
        }

        private static void FlightMenu()
        {
            RunMenu(
                "FLIGHT MANAGEMENT",
                new[] { "Add Airport", "View Airports", "Add Flight", "View All Flights", "Search Flight By ID", "Search Flight By Number", "Remove Flight", "Back" },
                choice => choice switch
                {
                    1 => Execute(AddAirport),
                    2 => Execute(ViewAirports),
                    3 => Execute(AddFlight),
                    4 => Execute(ViewFlights),
                    5 => Execute(SearchFlightById),
                    6 => Execute(SearchFlightByNumber),
                    7 => Execute(RemoveFlight),
                    8 => false,
                    _ => InvalidChoice()
                });
        }

        private static void AddAirport()
        {
            PrintHeader("ADD AIRPORT");
            int id = IdGenerator.GenerateId();
            int code = InputHelper.ReadPositiveInt("Airport Code: ");
            string name = InputHelper.ReadName("Name: ");
            string city = InputHelper.ReadName("City: ");
            string country = InputHelper.ReadName("Country: ");

            if (_data.Airports.Any(airport => airport.Code == code))
            {
                ShowMessage("Airport code already exists.");
                return;
            }

            _data.Airports.Add(new Airport(id, code, name, city, country));
            ShowMessage($"Airport added successfully. ID: {id}");
        }

        private static void ViewAirports()
        {
            PrintHeader("AIRPORTS");

            if (_data.Airports.Count == 0)
            {
                ShowMessage("No airports found.");
                return;
            }

            foreach (Airport airport in _data.Airports)
            {
                WriteSpaced($"ID: {airport.Id}");
                WriteSpaced($"Code: {airport.Code}");
                WriteSpaced($"Name: {airport.Name}");
                WriteSpaced($"City: {airport.City}");
                WriteSpaced($"Country: {airport.Country}");
            }

            Pause();
        }

        private static void AddFlight()
        {
            PrintHeader("ADD FLIGHT");

            if (_data.Airports.Count < 2)
            {
                ShowMessage("Create at least two airports before creating a flight.");
                return;
            }

            if (_data.Aircrafts.Count == 0)
            {
                ShowMessage("Create an aircraft before creating a flight.");
                return;
            }

            int flightId = IdGenerator.GenerateId();
            string flightNumber = InputHelper.ReadFlightNumber("Flight Number: ");
            Aircraft? aircraft = SelectAircraft();
            Airport? departure = SelectAirport("Departure Airport Code: ");
            Airport? arrival = SelectAirport("Arrival Airport Code: ");

            if (aircraft == null || departure == null || arrival == null)
            {
                ShowMessage("Flight creation cancelled because a required item was not found.");
                return;
            }

            if (departure.Id == arrival.Id)
            {
                ShowMessage("Departure and arrival airports must be different.");
                return;
            }

            DateTime departureTime = InputHelper.ReadDateTime("Departure Time (yyyy-MM-dd HH:mm): ");
            DateTime arrivalTime = InputHelper.ReadDateTime("Arrival Time (yyyy-MM-dd HH:mm): ");
            decimal basePrice = InputHelper.ReadPositiveDecimal("Base Price: ");

            Flight flight = new Flight(
                flightId,
                flightNumber,
                departure,
                arrival,
                departureTime,
                arrivalTime,
                aircraft,
                basePrice,
                FlightStatus.Scheduled);

            ShowMessage(_flightService.AddFlight(flight)
                ? $"Flight added successfully. ID: {flightId}"
                : "Flight could not be added. Check the flight number, times, and price.");
        }

        private static Aircraft? SelectAircraft()
        {
            int id = InputHelper.ReadPositiveInt("Aircraft ID: ");
            return _aircraftService.GetAircraftById(id);
        }

        private static Airport? SelectAirport(string message)
        {
            int code = InputHelper.ReadPositiveInt(message);
            return _data.Airports.FirstOrDefault(airport => airport.Code == code);
        }

        private static void ViewFlights()
        {
            PrintHeader("FLIGHTS");
            List<Flight> flights = _flightService.GetAllFlights();

            if (flights.Count == 0)
            {
                ShowMessage("No flights found.");
                return;
            }

            foreach (Flight flight in flights)
            {
                PrintFlight(flight);
            }

            Pause();
        }

        private static void SearchFlightById()
        {
            int id = InputHelper.ReadInt("Flight ID: ");
            PrintFlightResult(_flightService.GetFlightById(id));
        }

        private static void SearchFlightByNumber()
        {
            string number = InputHelper.ReadFlightNumber("Flight Number: ");
            PrintFlightResult(_flightService.GetFlightByNumber(number));
        }

        private static void PrintFlightResult(Flight? flight)
        {
            if (flight == null)
            {
                ShowMessage("Flight not found.");
                return;
            }

            PrintFlight(flight);
            Pause();
        }

        private static void PrintFlight(Flight flight)
        {
            WriteSpaced($"ID: {flight.FlightId}");
            WriteSpaced($"Flight Number: {flight.FlightNumber}");
            WriteSpaced($"From: {flight.DepartureAirport.Name} ({flight.DepartureAirport.Code})");
            WriteSpaced($"To: {flight.ArrivalAirport.Name} ({flight.ArrivalAirport.Code})");
            WriteSpaced($"Aircraft: {flight.Aircraft.Model}");
            WriteSpaced($"Departure: {flight.DepartureTime:yyyy-MM-dd HH:mm}");
            WriteSpaced($"Arrival: {flight.ArrivalTime:yyyy-MM-dd HH:mm}");
            WriteSpaced($"Base Price: {flight.BasePrice:F2}");
            WriteSpaced($"Status: {flight.Status}");
        }

        private static void RemoveFlight()
        {
            int id = InputHelper.ReadInt("Flight ID: ");
            ShowMessage(_flightService.RemoveFlight(id)
                ? "Flight removed successfully."
                : "Flight could not be removed. It may not exist or may have active reservations.");
        }

        private static void ReservationMenu()
        {
            RunMenu(
                "RESERVATION MANAGEMENT",
                new[] { "Create Reservation", "View All Reservations", "Search Reservation", "Cancel Reservation", "Back" },
                choice => choice switch
                {
                    1 => Execute(CreateReservation),
                    2 => Execute(ViewReservations),
                    3 => Execute(SearchReservation),
                    4 => Execute(CancelReservation),
                    5 => false,
                    _ => InvalidChoice()
                });
        }

        private static void CreateReservation()
        {
            PrintHeader("CREATE RESERVATION");
            Passenger? passenger = _passengerService.GetPassengerById(InputHelper.ReadInt("Passenger ID: "));
            Flight? flight = _flightService.GetFlightById(InputHelper.ReadInt("Flight ID: "));

            if (passenger == null || flight == null)
            {
                ShowMessage("Passenger or flight was not found.");
                return;
            }

            if (flight.Status is FlightStatus.Cancelled or FlightStatus.Departed or FlightStatus.Arrived)
            {
                ShowMessage("This flight is not available for reservations.");
                return;
            }

            PrintAvailableSeats(flight);
            string seatNumber = InputHelper.ReadSeatNumber("Seat Number: ");
            Seat? selectedSeat = flight.Aircraft.Seats.FirstOrDefault(seat =>
                seat.SeatNumber.Equals(seatNumber, StringComparison.OrdinalIgnoreCase));

            if (selectedSeat == null)
            {
                ShowMessage("Seat not found.");
                return;
            }

            Reservation? reservation = _reservationService.CreateReservation(passenger, flight, selectedSeat);
            PrintReservationCreationResult(reservation);
        }

        private static void PrintAvailableSeats(Flight flight)
        {
            Console.WriteLine();
            Console.WriteLine("Available Seats");
            Console.WriteLine();

            foreach (Seat seat in flight.Aircraft.Seats.Where(seat => !_reservationService.GetAllReservations().Any(reservation =>
                reservation.Flight.FlightId == flight.FlightId &&
                reservation.Seat.SeatNumber.Equals(seat.SeatNumber, StringComparison.OrdinalIgnoreCase) &&
                reservation.Status != ReservationStatus.Cancelled)))
            {
                decimal price = flight.BasePrice * seat.PriceMultiplier;
                Console.WriteLine($"{seat.SeatNumber} | {seat.Class} | Price: {price:F2}");
            }

            Console.WriteLine();
        }

        private static void PrintReservationCreationResult(Reservation? reservation)
        {
            if (reservation == null)
            {
                ShowMessage("Reservation could not be created. Check the seat and passenger reservation rules.");
                return;
            }

            Console.WriteLine("Reservation created successfully.");
            PrintReservation(reservation);
            Pause();
        }

        private static void ViewReservations()
        {
            PrintHeader("RESERVATIONS");
            List<Reservation> reservations = _reservationService.GetAllReservations();

            if (reservations.Count == 0)
            {
                ShowMessage("No reservations found.");
                return;
            }

            foreach (Reservation reservation in reservations)
            {
                PrintReservation(reservation);
            }

            Pause();
        }

        private static void SearchReservation()
        {
            int id = InputHelper.ReadInt("Reservation ID: ");
            Reservation? reservation = _reservationService.GetReservationById(id);

            if (reservation == null)
            {
                ShowMessage("Reservation not found.");
                return;
            }

            PrintReservation(reservation);
            Pause();
        }

        private static void CancelReservation()
        {
            int id = InputHelper.ReadInt("Reservation ID: ");
            ShowMessage(_reservationService.CancelReservation(id)
                ? "Reservation cancelled successfully."
                : "Reservation could not be cancelled.");
        }

        private static void PrintReservation(Reservation reservation)
        {
            WriteSpaced($"Reservation ID: {reservation.ReservationId}");
            WriteSpaced($"Passenger: {reservation.Passenger.Name}");
            WriteSpaced($"Flight: {reservation.Flight.FlightNumber}");
            WriteSpaced($"Seat: {reservation.Seat.SeatNumber}");
            WriteSpaced($"Class: {reservation.Seat.Class}");
            WriteSpaced($"Reservation Date: {reservation.ReservationDate:yyyy-MM-dd HH:mm}");
            WriteSpaced($"Status: {reservation.Status}");
            WriteSpaced($"Payment Status: {reservation.PaymentStatus}");
            WriteSpaced($"Total Price: {reservation.TotalPrice:F2}");
        }

        private static void PaymentMenu()
        {
            RunMenu(
                "PAYMENT MANAGEMENT",
                new[] { "Pay With Cash", "Pay With Credit Card", "Pay With PayPal", "Back" },
                choice => choice switch
                {
                    1 => Execute(ProcessCashPayment),
                    2 => Execute(ProcessCreditCardPayment),
                    3 => Execute(ProcessPayPalPayment),
                    4 => false,
                    _ => InvalidChoice()
                });
        }

        private static void ProcessCashPayment()
        {
            ProcessPayment(new CashPayment(), "Cash");
        }

        private static void ProcessCreditCardPayment()
        {
            string cardNumber = InputHelper.ReadCardNumber("Card Number: ");
            ProcessPayment(new CreditCardPayment(cardNumber), "Credit Card");
        }

        private static void ProcessPayPalPayment()
        {
            string email = InputHelper.ReadEmail("PayPal Email: ");
            ProcessPayment(new PayPalPayment(email), "PayPal");
        }

        private static void ProcessPayment(IPayment payment, string paymentName)
        {
            int reservationId = InputHelper.ReadInt("Reservation ID: ");
            Reservation? reservation = _reservationService.GetReservationById(reservationId);

            if (reservation == null)
            {
                ShowMessage("Reservation not found.");
                return;
            }

            Console.WriteLine($"Amount Required: {reservation.TotalPrice:F2}");
            decimal amount = InputHelper.ReadPositiveDecimal("Payment Amount: ");
            PaymentService paymentService = new PaymentService(payment);
            bool result = paymentService.ProcessPayment(reservation, amount);

            ShowMessage(result
                ? $"{paymentName} payment successful."
                : $"{paymentName} payment failed.");
        }

        private static void SearchMenu()
        {
            RunMenu(
                "SEARCH",
                new[] { "Search Passenger", "Search Employee", "Search Aircraft", "Search Flight", "Search Reservation", "Back" },
                choice => choice switch
                {
                    1 => Execute(SearchPassengerById),
                    2 => Execute(SearchEmployeeById),
                    3 => Execute(SearchAircraft),
                    4 => Execute(SearchFlightById),
                    5 => Execute(SearchReservation),
                    6 => false,
                    _ => InvalidChoice()
                });
        }

        private static void ReportsMenu()
        {
            RunMenu(
                "REPORTS",
                new[] { "Passenger Report", "Employee Report", "Aircraft Report", "Flight Report", "Reservation Report", "Revenue Report", "Seat Report", "Back" },
                choice => choice switch
                {
                    1 => Execute(PassengerReport),
                    2 => Execute(EmployeeReport),
                    3 => Execute(AircraftReport),
                    4 => Execute(FlightReport),
                    5 => Execute(ReservationReport),
                    6 => Execute(RevenueReport),
                    7 => Execute(SeatReport),
                    8 => false,
                    _ => InvalidChoice()
                });
        }

        private static void PassengerReport()
        {
            PrintHeader("PASSENGER REPORT");
            Console.WriteLine($"Total Passengers: {_data.Passengers.Count}");
            Pause();
        }

        private static void EmployeeReport()
        {
            PrintHeader("EMPLOYEE REPORT");
            int pilots = _data.Employees.Values.OfType<Pilot>().Count();
            int attendants = _data.Employees.Values.OfType<FlightAttendant>().Count();
            Console.WriteLine($"Total Employees: {_data.Employees.Count}");
            Console.WriteLine();
            Console.WriteLine($"Pilots: {pilots}");
            Console.WriteLine();
            Console.WriteLine($"Flight Attendants: {attendants}");
            Pause();
        }

        private static void AircraftReport()
        {
            PrintHeader("AIRCRAFT REPORT");
            int totalSeats = _data.Aircrafts.Values.Sum(aircraft => aircraft.Seats.Length);
            Console.WriteLine($"Total Aircraft: {_data.Aircrafts.Count}");
            Console.WriteLine();
            Console.WriteLine($"Total Seats: {totalSeats}");
            Pause();
        }

        private static void FlightReport()
        {
            PrintHeader("FLIGHT REPORT");
            Console.WriteLine($"Total Flights: {_data.Flights.Count}");
            Console.WriteLine();

            foreach (FlightStatus status in Enum.GetValues<FlightStatus>())
            {
                int count = _data.Flights.Values.Count(flight => flight.Status == status);
                Console.WriteLine($"{status}: {count}");
                Console.WriteLine();
            }

            if (_data.Flights.Count > 0)
            {
                decimal average = _data.Flights.Values.Average(flight => flight.BasePrice);
                Flight mostExpensive = _data.Flights.Values.OrderByDescending(flight => flight.BasePrice).First();
                Console.WriteLine($"Average Flight Price: {average:F2}");
                Console.WriteLine();
                Console.WriteLine($"Most Expensive Flight: {mostExpensive.FlightNumber}");
                Console.WriteLine();
                Console.WriteLine($"Most Expensive Price: {mostExpensive.BasePrice:F2}");
            }

            Pause();
        }

        private static void ReservationReport()
        {
            PrintHeader("RESERVATION REPORT");
            Console.WriteLine($"Total Reservations: {_data.Reservations.Count}");
            Console.WriteLine();

            foreach (ReservationStatus status in Enum.GetValues<ReservationStatus>())
            {
                int count = _data.Reservations.Values.Count(reservation => reservation.Status == status);
                Console.WriteLine($"{status}: {count}");
                Console.WriteLine();
            }

            int business = _data.Reservations.Values.Count(reservation =>
                reservation.Seat.Class.Equals(nameof(SeatClass.Business), StringComparison.OrdinalIgnoreCase));

            Console.WriteLine($"Business Class Reservations: {business}");
            Pause();
        }

        private static void RevenueReport()
        {
            PrintHeader("REVENUE REPORT");
            decimal revenue = _data.Reservations.Values
                .Where(reservation => reservation.PaymentStatus == PaymentStatus.Paid)
                .Sum(reservation => reservation.TotalPrice);

            Console.WriteLine($"Paid Revenue: {revenue:F2}");
            Pause();
        }

        private static void SeatReport()
        {
            PrintHeader("SEAT REPORT");
            IEnumerable<Seat> seats = _data.Aircrafts.Values.SelectMany(aircraft => aircraft.Seats);
            int total = seats.Count();
            int available = seats.Count(seat => seat.IsAvailable);
            int occupied = total - available;

            Console.WriteLine($"Total Seats: {total}");
            Console.WriteLine();
            Console.WriteLine($"Available Seats: {available}");
            Console.WriteLine();
            Console.WriteLine($"Occupied Seats: {occupied}");
            Pause();
        }

        private static void RunMenu(string title, string[] options, Func<int, bool> handler)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                PrintHeader(title);

                for (int index = 0; index < options.Length; index++)
                {
                    Console.WriteLine($"{index + 1} - {options[index]}");
                    Console.WriteLine();
                }

                int choice = InputHelper.ReadInt("Choose an option: ");
                running = handler(choice);
            }
        }

        private static bool Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                ShowError(exception);
            }

            return true;
        }

        private static bool InvalidChoice()
        {
            ShowMessage("Invalid option.");
            return true;
        }

        private static void WriteSpaced(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }

        private static void PrintHeader(string title)
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"        {title}");
            Console.WriteLine("========================================");
            Console.WriteLine();
        }

        private static void ShowMessage(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
            Pause();
        }

        private static void ShowError(Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("The operation could not be completed.");
            Console.WriteLine($"Reason: {exception.Message}");
            Console.WriteLine();
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
