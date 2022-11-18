using TripBooking.Core.Exceptions;

namespace TripBooking.Core.Reservations.Exceptions
{
    public class ResrvationExistsException : CustomException
    {
        public ResrvationExistsException(string email, string tripName)
            : base($"Email adress {email} is already register fo trip {tripName}.")
        {
        }
    }
}
