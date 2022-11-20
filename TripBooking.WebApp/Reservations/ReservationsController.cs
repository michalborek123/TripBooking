using Microsoft.AspNetCore.Mvc;
using TripBooking.Core.Reservations.Commands;
using TripBooking.Core.Reservations.Responses;
using TripBooking.Data.Reservations.Repository;

namespace TripBooking.WebApp.Reservations
{
    /// <summary>
    /// API for trip reservations
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IRegistrationRepository reservationRepository;

        public ReservationsController(IRegistrationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        /// <summary>
        /// Register for a trip
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(TripRegistrationResponse), 200)]
        public async Task<ActionResult<TripRegistrationResponse>> RegisterForTrip(TripRegistrationRequest request)
        {
            var registration = await reservationRepository.RegisterForTripAsync(request);

            return Ok(registration);
        }


        /// <summary>
        /// Unregister from existing registration
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Route("unregister")]
        [ProducesResponseType(typeof(TripRegistrationResponse), 200)]
        public async Task<ActionResult<TripRegistrationResponse>> UnregisterFromTrip(TripUnregistrationRequest request)
        {
            var registration = await reservationRepository.UnregisterFromTripAsync(request);

            return Ok(registration);
        }
    }
}
