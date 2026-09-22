namespace AirlineManagementSystem.Models
{
    public class Airport
    {
        public int Id { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        public Airport(int id, int code, string name, string city, string country)
        {
            Id = id;
            Code = code;
            Name = name?.Trim() ?? string.Empty;
            City = city?.Trim() ?? string.Empty;
            Country = country?.Trim() ?? string.Empty;
        }
    }
}
