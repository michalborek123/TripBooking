using TripBooking.Core.Exceptions;

namespace TripBooking.Core.Trips.Exceptions
{
    public class TripExisistsException : CustomException
    {
        public TripExisistsException(string? name) 
            : base($"Trip {name} already exists.")
        {
        }
    }
}
