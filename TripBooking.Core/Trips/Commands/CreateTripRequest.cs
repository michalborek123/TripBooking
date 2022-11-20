using TripBooking.Core.Enums;

namespace TripBooking.Core.Trips.Commands
{
    public class CreateTripRequest
    {
        /// <summary>
        /// Unique id of command
        /// </summary>
        private Guid Id { get; init; } = Guid.NewGuid();

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

        public Guid GetId()
        {
            return Id;
        }
    }
}
