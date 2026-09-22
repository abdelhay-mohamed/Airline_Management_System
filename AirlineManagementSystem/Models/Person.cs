namespace AirlineManagementSystem.Models
{
    public abstract class Person
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        protected Person(int id, string name, string email, string phone)
        {
            Id = id;
            Name = name?.Trim() ?? string.Empty;
            Email = email?.Trim() ?? string.Empty;
            Phone = phone?.Trim() ?? string.Empty;
        }
    }
}
