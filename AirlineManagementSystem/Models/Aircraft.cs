using AirlineManagementSystem.ENUMS;

namespace AirlineManagementSystem.Models
{
    public class Aircraft
    {
        public int Id { get; private set; }
        public string Model { get; private set; }
        public int Capacity { get; private set; }
        public Seat[] Seats { get; private set; }

        public Aircraft(int id, string model, int capacity)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Aircraft ID must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentException("Aircraft model is required.");
            }

            if (capacity <= 0 || capacity > 500)
            {
                throw new ArgumentException("Aircraft capacity must be between 1 and 500.");
            }

            Id = id;
            Model = model.Trim();
            Capacity = capacity;
            Seats = CreateDefaultSeats(capacity);
        }

        private static Seat[] CreateDefaultSeats(int capacity)
        {
            Seat[] seats = new Seat[capacity];

            for (int index = 0; index < capacity; index++)
            {
                string seatNumber = $"{(char)('A' + index % 6)}{index / 6 + 1}";
                seats[index] = new Seat(
                    seatNumber,
                    SeatClass.Economy.ToString(),
                    true,
                    1.0m);
            }

            return seats;
        }
    }
}
