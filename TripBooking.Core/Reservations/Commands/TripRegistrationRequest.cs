namespace TripBooking.Core.Reservations.Commands
{
    public record TripRegistrationRequest
    {
        public string TripName { get; private set; } = string.Empty;
        public string Email { get; init; } = string.Empty;

        public void SetTripName(string tripName) => TripName = tripName;
    }
}
