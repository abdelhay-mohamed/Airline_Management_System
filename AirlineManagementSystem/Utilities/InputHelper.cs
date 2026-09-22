using System.Globalization;
using System.Text.RegularExpressions;

namespace AirlineManagementSystem.Utilities
{
    public static class InputHelper
    {
        public static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
                {
                    return value;
                }

                Console.WriteLine("Please enter a valid integer.");
                Console.WriteLine();
            }
        }

        public static int ReadPositiveInt(string message)
        {
            while (true)
            {
                int value = ReadInt(message);

                if (value > 0)
                {
                    return value;
                }

                Console.WriteLine("Value must be greater than zero.");
                Console.WriteLine();
            }
        }

        public static int ReadNonNegativeInt(string message)
        {
            while (true)
            {
                int value = ReadInt(message);

                if (value >= 0)
                {
                    return value;
                }

                Console.WriteLine("Value cannot be negative.");
                Console.WriteLine();
            }
        }

        public static int ReadBoundedPositiveInt(string message, int maximum)
        {
            while (true)
            {
                int value = ReadPositiveInt(message);

                if (value <= maximum)
                {
                    return value;
                }

                Console.WriteLine($"Value must be between 1 and {maximum}.");
                Console.WriteLine();
            }
        }

        public static decimal ReadDecimal(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine() ?? string.Empty;

                if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value) &&
                    value <= decimal.MaxValue)
                {
                    return value;
                }

                Console.WriteLine("Please enter a valid decimal number.");
                Console.WriteLine();
            }
        }

        public static decimal ReadPositiveDecimal(string message)
        {
            while (true)
            {
                decimal value = ReadDecimal(message);

                if (value > 0)
                {
                    return value;
                }

                Console.WriteLine("Value must be greater than zero.");
                Console.WriteLine();
            }
        }

        public static string ReadString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string value = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value.Trim();
                }

                Console.WriteLine("Input cannot be empty.");
                Console.WriteLine();
            }
        }

        public static string ReadName(string message)
        {
            while (true)
            {
                string value = ReadString(message);

                if (Regex.IsMatch(value, @"^[\p{L}][\p{L} .'-]*$"))
                {
                    return value;
                }

                Console.WriteLine("Name can contain letters, spaces, apostrophes, periods, and hyphens only.");
                Console.WriteLine();
            }
        }

        public static string ReadEmail(string message)
        {
            while (true)
            {
                string value = ReadString(message);

                if (Regex.IsMatch(value, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
                {
                    return value;
                }

                Console.WriteLine("Please enter a valid email address.");
                Console.WriteLine();
            }
        }

        public static string ReadPhone(string message)
        {
            while (true)
            {
                string value = ReadString(message);

                if (Regex.IsMatch(value, @"^\+?[0-9]{7,15}$"))
                {
                    return value;
                }

                Console.WriteLine("Phone must contain 7 to 15 digits and may start with +.");
                Console.WriteLine();
            }
        }

        public static string ReadPassportNumber(string message)
        {
            while (true)
            {
                string value = ReadString(message).ToUpperInvariant();

                if (Regex.IsMatch(value, @"^[A-Z0-9]{5,20}$"))
                {
                    return value;
                }

                Console.WriteLine("Passport number must contain 5 to 20 letters or digits only.");
                Console.WriteLine();
            }
        }

        public static string ReadNationality(string message)
        {
            return ReadName(message);
        }

        public static string ReadLicenseNumber(string message)
        {
            while (true)
            {
                string value = ReadString(message).ToUpperInvariant();

                if (Regex.IsMatch(value, @"^[A-Z0-9-]{3,30}$"))
                {
                    return value;
                }

                Console.WriteLine("License number must contain 3 to 30 letters, digits, or hyphens.");
                Console.WriteLine();
            }
        }

        public static string ReadFlightNumber(string message)
        {
            while (true)
            {
                string value = ReadString(message).ToUpperInvariant();

                if (Regex.IsMatch(value, @"^[A-Z0-9-]{2,10}$"))
                {
                    return value;
                }

                Console.WriteLine("Flight number must contain 2 to 10 letters, digits, or hyphens.");
                Console.WriteLine();
            }
        }

        public static string ReadSeatNumber(string message)
        {
            while (true)
            {
                string value = ReadString(message).ToUpperInvariant();

                if (Regex.IsMatch(value, @"^[A-F][1-9][0-9]?$"))
                {
                    return value;
                }

                Console.WriteLine("Seat number must use a letter A-F followed by a seat number.");
                Console.WriteLine();
            }
        }

        public static string ReadCardNumber(string message)
        {
            while (true)
            {
                string value = ReadString(message);
                string digits = value.Replace(" ", string.Empty).Replace("-", string.Empty);

                if (Regex.IsMatch(digits, @"^[0-9]{13,19}$"))
                {
                    return digits;
                }

                Console.WriteLine("Card number must contain 13 to 19 digits.");
                Console.WriteLine();
            }
        }

        public static DateTime ReadDate(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine() ?? string.Empty;

                if (DateTime.TryParseExact(
                    input,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime value))
                {
                    return value;
                }

                Console.WriteLine("Use the format yyyy-MM-dd.");
                Console.WriteLine();
            }
        }

        public static DateTime ReadDateOfBirth(string message)
        {
            while (true)
            {
                DateTime value = ReadDate(message);

                if (value.Date <= DateTime.Today)
                {
                    return value;
                }

                Console.WriteLine("Date of birth cannot be in the future.");
                Console.WriteLine();
            }
        }

        public static DateTime ReadDateTime(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine() ?? string.Empty;

                if (DateTime.TryParseExact(
                    input,
                    "yyyy-MM-dd HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime value))
                {
                    return value;
                }

                Console.WriteLine("Use the format yyyy-MM-dd HH:mm.");
                Console.WriteLine();
            }
        }

        public static bool ReadYesNo(string message)
        {
            while (true)
            {
                string value = ReadString(message).ToLowerInvariant();

                if (value == "y" || value == "yes")
                {
                    return true;
                }

                if (value == "n" || value == "no")
                {
                    return false;
                }

                Console.WriteLine("Please enter y or n.");
                Console.WriteLine();
            }
        }
    }
}
