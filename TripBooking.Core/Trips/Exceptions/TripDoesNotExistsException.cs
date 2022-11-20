using TripBooking.Core.Exceptions;

namespace TripBooking.Core.Trips.Exceptions
{
    public class TripDoesNotExistsException : CustomException
    {
        public TripDoesNotExistsException(string? name)
            : base($"Trip {name} does not exists.")
        {
        }
    }
}
