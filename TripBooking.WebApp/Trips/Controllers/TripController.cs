using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TripBooking.Core.Trips.Commands;
using TripBooking.Core.Trips.Responses;
using TripBooking.Data.Trips.Repository;

namespace TripBooking.WebApp.Trips.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {
        public TripController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        private readonly ITripRepository _tripRepository;

        [HttpPost(Name = "CreateTrip")]
        public async Task<ActionResult<TripResponse>> CreateTrip(CreateTripRequest request)
        {
            var trip = await _tripRepository.AddTripAsync(request);

            return trip;
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

        [HttpDelete("{name}", Name = "DeleteTrip")]
        public async Task<IActionResult> DeleteTrip(string name, CancellationToken cancellationToken)
        {
            await _tripRepository.DeleteAsync(name, cancellationToken);

            return Ok($"Trip {name} deleted.");
        }



    }
}
