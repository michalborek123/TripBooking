using TripBooking.Core.Enums;

namespace TripBooking.Core.Trips.Responses
{
    public class TripResponse
    {
        /// <summary>
        /// Name of the trip
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Country destination
        /// </summary>
        public State? Country { get; init; } = State.None;

        /// <summary>
        /// Trip description
        /// </summary>
        public string Description { get; init; } = string.Empty;

        /// <summary>
        /// Trip start time
        /// </summary>
        public DateTime Start { get; init; } = DateTime.Now;

        /// <summary>
        /// Available seats
        /// </summary>
        public uint Seats { get; init; } = default;
    }
}
