using System.Text.Json.Serialization;

namespace TripBooking.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum State
    {
        None,
        Poland,
        Germany,
        Spain,
        Egipt,
        Italy,
        France
    }
}
