using TripBooking.Core.Enums;

namespace TripBooking.Core.Reservations.Responses
{
    public record TripRegistrationResponse
    {
        public RegistrationStatus Status { get; init; }
        public string TripName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }
}
