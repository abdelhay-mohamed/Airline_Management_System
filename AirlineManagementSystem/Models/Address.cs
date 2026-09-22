namespace AirlineManagementSystem.Models
{
    public struct Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public Address(string country, string city, string street)
        {
            Country = country;
            City = city;
            Street = street;
        }
    }
}