using TripBooking.Core.Exceptions;

namespace TripBooking.Core.Reservations.Exceptions
{
    public class RegistrationExistsException : CustomException
    {
        public RegistrationExistsException(string email, string tripName)
            : base($"Email adress {email} is already register fo trip {tripName}.")
        {
        }
    }
}
