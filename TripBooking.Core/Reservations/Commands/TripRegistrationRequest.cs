namespace TripBooking.Core.Reservations.Commands
{
    public record TripRegistrationRequest
    {
        public string TripName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }
}
