using System.Text.Json.Serialization;

namespace TripBooking.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RegistrationStatus
    {
        Registered,
        Cancelled
    }
}
