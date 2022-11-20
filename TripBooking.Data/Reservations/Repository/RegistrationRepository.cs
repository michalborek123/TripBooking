using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TripBooking.Core.Enums;
using TripBooking.Core.Reservations.Commands;
using TripBooking.Core.Reservations.Exceptions;
using TripBooking.Core.Reservations.Responses;
using TripBooking.Core.Trips.Exceptions;
using TripBooking.Data.Context;
using TripBooking.Data.Reservations.Model;
using TripBooking.Data.Trips.Repository;

namespace TripBooking.Data.Reservations.Repository
{
    public interface IRegistrationRepository
    {
        Task<TripRegistrationResponse> RegisterForTripAsync(TripRegistrationRequest request);
        Task<TripRegistrationResponse> UnregisterFromTripAsync(TripUnregistrationRequest request);
    }

    internal class RegistrationRepository : IRegistrationRepository
    {
        private readonly ApiContext apiContext;
        private readonly IMapper mapper;
        private readonly ITripRepository tripRepository;

        public RegistrationRepository(ApiContext apiContext, IMapper mapper, ITripRepository tripRepository)
        {
            this.apiContext = apiContext;
            this.mapper = mapper;
            this.tripRepository = tripRepository;
        }

        public async Task<TripRegistrationResponse> RegisterForTripAsync(TripRegistrationRequest request)
        {
            var trip = await tripRepository.GetByNameAsync(request.TripName);

            if (trip is null)
            {
                throw new TripDoesNotExistsException(request.TripName);
            }

            if (CheckExistsRegistration(request.TripName, request.Email))
            {
                throw new RegistrationExistsException(request.Email, request.TripName);
            }

            var registration = mapper.Map<TripRegistration>(request);
            registration.Status = RegistrationStatus.Registered;

            await apiContext.AddAsync(registration);
            await apiContext.SaveChangesAsync();

            var response = mapper.Map<TripRegistrationResponse>(registration);
            return response;
        }

        public async Task<TripRegistrationResponse> UnregisterFromTripAsync(TripUnregistrationRequest request)
        {
            if (!CheckExistsRegistration(request.TripName, request.Email))
            {
                throw new RegistrationDoesNotExistsException(request.Email, request.TripName);
            }

            var registration = await apiContext.TripRegistrations.SingleAsync(x => x.TripName == request.TripName
            && x.Email == request.Email
            && x.Status != RegistrationStatus.Cancelled);

            registration.Status = RegistrationStatus.Cancelled;
            apiContext.SaveChanges();

            var response = mapper.Map<TripRegistrationResponse>(registration);
            return response;
        }

        private bool CheckExistsRegistration(string tripName, string email)
            => apiContext.TripRegistrations.Any(x => x.TripName == tripName && x.Email == email && x.Status == RegistrationStatus.Registered);
    }
}
