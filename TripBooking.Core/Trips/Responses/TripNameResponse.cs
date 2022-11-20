namespace TripBooking.Core.Trips.Responses
{
    public record TripNameResponse
    {
        /// <summary>
        /// Name of the trip
        /// </summary>
        public string Name { get; }

        public TripNameResponse(string name)
        {
            Name = name;
        }
    }
}
