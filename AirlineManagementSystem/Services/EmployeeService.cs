using AirlineManagementSystem.Data;
using AirlineManagementSystem.Interfaces;
using AirlineManagementSystem.Models;
using System.Text.RegularExpressions;

namespace AirlineManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AirlineData _data;

        public EmployeeService(AirlineData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public bool AddEmployee(Employee employee)
        {
            if (employee is null || !IsValidEmployee(employee))
            {
                return false;
            }

            if (_data.Employees.ContainsKey(employee.Id))
            {
                return false;
            }

            if (_data.Employees.Values.Any(existing =>
                existing.EmployeeId == employee.EmployeeId ||
                existing.Email.Equals(employee.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            if (employee is Pilot pilot &&
                _data.Employees.Values.OfType<Pilot>().Any(existing =>
                    existing.LicenseNumber.Equals(pilot.LicenseNumber, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            _data.Employees.Add(employee.Id, employee);
            return true;
        }

        public Employee? GetEmployeeById(int id)
        {
            _data.Employees.TryGetValue(id, out Employee? employee);
            return employee;
        }

        public Employee? GetEmployeeByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return _data.Employees.Values.FirstOrDefault(employee =>
                employee.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public List<Employee> GetAllEmployees()
        {
            return _data.Employees.Values.ToList();
        }

        public bool RemoveEmployee(int id)
        {
            return _data.Employees.Remove(id);
        }

        private static bool IsValidEmployee(Employee employee)
        {
            if (employee.Id <= 0 ||
                employee.EmployeeId <= 0 ||
                employee.Salary <= 0 ||
                !Regex.IsMatch(employee.Name, @"^[\p{L}][\p{L} .'-]*$") ||
                !Regex.IsMatch(employee.Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$") ||
                !Regex.IsMatch(employee.Phone, @"^\+?[0-9]{7,15}$"))
            {
                return false;
            }

            if (employee is Pilot pilot)
            {
                return pilot.YearsOfExperience >= 0 &&
                       Regex.IsMatch(pilot.LicenseNumber, @"^[A-Z0-9-]{3,30}$");
            }

            if (employee is FlightAttendant attendant)
            {
                return attendant.Languages.Count > 0 &&
                       attendant.Languages.Count <= 20 &&
                       attendant.Languages.All(language =>
                           !string.IsNullOrWhiteSpace(language) &&
                           Regex.IsMatch(language.Trim(), @"^[\p{L}][\p{L} .'-]*$"));
            }

            return true;
        }
    }
}
