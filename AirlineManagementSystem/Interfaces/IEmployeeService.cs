using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.Interfaces
{
    public interface IEmployeeService
    {
        bool AddEmployee(Employee employee);
        Employee? GetEmployeeById(int id);
        Employee? GetEmployeeByEmail(string email);
        List<Employee> GetAllEmployees();
        bool RemoveEmployee(int id);
    }
}