namespace AirlineManagementSystem.Utilities
{
    public static class IdGenerator
    {
        private static int _currentId;
        private static readonly object _lock = new object();

        public static int GenerateId()
        {
            lock (_lock)
            {
                if (_currentId == int.MaxValue)
                {
                    throw new InvalidOperationException("No more IDs are available.");
                }

                return ++_currentId;
            }
        }
    }
}
