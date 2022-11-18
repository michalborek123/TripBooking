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

            return CreatedAtRoute("GetTripByName", new { name = trip.Name }, trip);
        }


        [HttpPut("{name}", Name = "UpdateTrip")]
        public async Task<ActionResult<TripResponse>> UpdateTrip(string name, UpdateTripRequest request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.UpdateAsync(name, request, cancellationToken);

            return Ok(trip);
        }

        [HttpGet("{name}", Name = "GetTripByName")]
        public async Task<ActionResult<TripResponse>> GetTripByName([FromRoute] string name, CancellationToken cancellationToken)
        {
            var result = await _tripRepository.GetByNameAsync(name, cancellationToken);

            return Ok(result);
        }

        [HttpGet(Name = "GetAllTrips")]
        public async Task<ActionResult<IEnumerable<TripNameResponse>>> GetAllTrips(CancellationToken cancellationToken)
        {
            var result = await _tripRepository.GetAllAsync(cancellationToken);

            if (result is null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }


        [HttpGet("country/{country}", Name = "GetTripsByCountry")]
        public async Task<ActionResult<IEnumerable<TripNameResponse>>> GetTripsByCountry([FromRoute] State country, CancellationToken cancellationToken)
        {
            var result = await _tripRepository.GetByCountryAsync(country, cancellationToken);

            if (result is null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpDelete("{name}", Name = "DeleteTrip")]
        public async Task<IActionResult> DeleteTrip(string name, CancellationToken cancellationToken)
        {
            await _tripRepository.DeleteAsync(name, cancellationToken);

            return Ok($"Trip {name} deleted.");
        }
    }
}
