using System.ComponentModel.DataAnnotations;
using TripBooking.Core.Enums;

namespace TripBooking.Data.Trips.Model
{
    public class Trip
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the trip
        /// </summary>
        [Key]
        public string Name { get; set; }

        /// <summary>
        /// Country destination
        /// </summary>
        public State? Country { get; set; }

        /// <summary>
        /// Trip description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Trip start time
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Available seats
        /// </summary>
        public uint Seats { get; set; }
    }
}
