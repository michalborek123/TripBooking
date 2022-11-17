using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TripBooking.Core.Trips.Commands;
using TripBooking.Data.Trips.Model;
using TripBooking.Data.Trips.Repository;

namespace TripBooking.WebApp.Trips.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController :ControllerBase
    {
        public TripController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;


        }

        private readonly ITripRepository _tripRepository;

        [HttpPost(Name = "CreateTrip")]
        public Task CreateTrip(CreateTripRequest request)
        {
            return Task.CompletedTask;
        }

        [HttpGet(Name = "GetTripByName")]
        public async Task<ActionResult<Trip>> GetTripByName(string name, CancellationToken cancellationToken)
        {
            var result = await _tripRepository.GetByNameAsync(name, cancellationToken);

            return Ok(result);
        }

        [HttpDelete(Name = "DeleteTrip")]
        public IActionResult DeleteTrip(string name)
        {
            return Ok();
        }
    }
}
