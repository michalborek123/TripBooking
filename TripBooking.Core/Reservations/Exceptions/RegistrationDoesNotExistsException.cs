using TripBooking.Core.Exceptions;

namespace TripBooking.Core.Reservations.Exceptions
{
    public class RegistrationDoesNotExistsException : CustomException
    {
        public RegistrationDoesNotExistsException(string email, string tripName)
            : base($"Registration for trip {tripName} and email adress {email} deos not exists.")
        {
        }
    }
}
