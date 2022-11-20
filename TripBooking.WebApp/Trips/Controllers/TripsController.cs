using Microsoft.AspNetCore.Mvc;
using TripBooking.Core.Enums;
using TripBooking.Core.Trips.Commands;
using TripBooking.Core.Trips.Responses;
using TripBooking.Data.Trips.Repository;

namespace TripBooking.WebApp.Trips.Controllers
{
    /// <summary>
    /// Trips booking controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TripsController : ControllerBase
    {
        public TripsController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        private readonly ITripRepository _tripRepository;

        /// <summary>
        /// Create new trip with all details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(TripResponse), 201)]
        [ProducesResponseType(400)]
        [HttpPost(Name = "CreateTrip")]
        public async Task<ActionResult<TripResponse>> CreateTrip(CreateTripRequest request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.AddTripAsync(request, cancellationToken);

            return CreatedAtRoute("GetTripByName", new { tripName = trip.Name }, trip);
        }

        /// <summary>
        /// Update trip details
        /// </summary>
        /// <param name="tripName">Trip name to updade</param>
        /// <param name="request">Trip data to update</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(TripResponse), 200)]
        [ProducesResponseType(400)]
        [HttpPut("{tripName}", Name = "UpdateTrip")]
        public async Task<ActionResult<TripResponse>> UpdateTrip(string tripName, UpdateTripRequest request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.UpdateAsync(tripName, request, cancellationToken);

            return Ok(trip);
        }

        /// <summary>
        /// Get trip data by name
        /// </summary>
        /// <param name="tripName">Trip name</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{tripName}", Name = "GetTripByName")]
        [ProducesResponseType(typeof(TripResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<TripResponse>> GetTripByName([FromRoute] string tripName, CancellationToken cancellationToken)
        {
            var result = await _tripRepository.GetByNameAsync(tripName, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Get all trips
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of all trips (names)</returns>
        [HttpGet(Name = "GetAllTrips")]
        [ProducesResponseType(typeof(IEnumerable<TripNameResponse>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<TripNameResponse>>> GetAllTrips(CancellationToken cancellationToken)
        {
            var result = await _tripRepository.GetAllAsync(cancellationToken);

            if (result is null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get all trips by country
        /// </summary>
        /// <param name="country">Country name</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of all trips for country (names)</returns>
        [HttpGet("country/{country}", Name = "GetTripsByCountry")]
        [ProducesResponseType(typeof(IEnumerable<TripNameResponse>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<TripNameResponse>>> GetTripsByCountry([FromRoute] State country, CancellationToken cancellationToken)
        {
            var result = await _tripRepository.GetByCountryAsync(country, cancellationToken);

            if (result is null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        /// <summary>
        /// Remove trip
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{name}", Name = "DeleteTrip")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteTrip(string name, CancellationToken cancellationToken)
        {
            await _tripRepository.DeleteAsync(name, cancellationToken);

            return Ok($"Trip {name} deleted.");
        }
    }
}
