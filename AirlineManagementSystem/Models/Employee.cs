namespace AirlineManagementSystem.Models
{
    public class Employee : Person
    {
        public int EmployeeId { get; set; }
        public decimal Salary { get; set; }

        public Employee(
            int id,
            string name,
            string email,
            string phone,
            int employeeId,
            decimal salary
        ) : base(id, name, email, phone)
        {
            EmployeeId = employeeId;
            Salary = salary;
        }
    }
}
