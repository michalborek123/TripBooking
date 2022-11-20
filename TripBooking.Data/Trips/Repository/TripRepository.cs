using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TripBooking.Core.Enums;
using TripBooking.Core.Trips.Commands;
using TripBooking.Core.Trips.Exceptions;
using TripBooking.Core.Trips.Responses;
using TripBooking.Data.Context;
using TripBooking.Data.Trips.Model;

namespace TripBooking.Data.Trips.Repository
{
    public interface ITripRepository
    {
        Task<TripResponse> AddTripAsync(CreateTripRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<TripNameResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<TripNameResponse>> GetByCountryAsync(State country, CancellationToken cancellationToken);
        Task<TripResponse?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<TripResponse> UpdateAsync(string name, UpdateTripRequest request, CancellationToken cancellationToken);
    }

    internal class TripRepository : ITripRepository
    {
        private readonly ApiContext apiContext;
        private readonly IMapper mapper;

        public TripRepository(ApiContext apiContext, IMapper mapper)
        {
            this.apiContext = apiContext;
            this.mapper = mapper;
        }

        public async Task<TripResponse> AddTripAsync(CreateTripRequest request, CancellationToken cancellationToken = default)
        {
            var trip = await GetTripByNameAsync(request.Name, cancellationToken);

            if (trip is not null)
            {
                throw new TripExisistsException(request.Name);
            }

            var newTrip = mapper.Map<Trip>(request);

            await apiContext.Trips.AddAsync(newTrip, cancellationToken);
            await apiContext.SaveChangesAsync(cancellationToken);

            var respone = mapper.Map<TripResponse>(newTrip);

            return respone;
        }

        public async Task DeleteAsync(string name, CancellationToken cancellationToken)
        {
            var trip = await GetTripByNameAsync(name, cancellationToken);

            if (trip is null)
            {
                throw new TripDoesNotExistsException(name);
            }

            apiContext.Trips.Remove(trip);
            await apiContext.SaveChangesAsync(cancellationToken);
        }

        public Task<IEnumerable<TripNameResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var trips = apiContext.Trips.Select(x => new TripNameResponse(x.Name)).AsEnumerable();

            return Task.FromResult(trips);
        }

        public async Task<IEnumerable<TripNameResponse>> GetByCountryAsync(State country, CancellationToken cancellationToken)
        {
            var tripsByCountry = await apiContext.Trips.Where(x => x.Country == country)
                .Select(x => new TripNameResponse(x.Name)).ToListAsync(cancellationToken);

            return tripsByCountry;
        }

        public async Task<TripResponse?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var trip = await GetTripByNameAsync(name, cancellationToken);

            var response = mapper.Map<TripResponse>(trip);

            return response;
        }

        public async Task<TripResponse> UpdateAsync(string name, UpdateTripRequest request, CancellationToken cancellationToken)
        {
            var trip = await GetTripByNameAsync(name, cancellationToken);

            if (trip is null)
            {
                throw new TripDoesNotExistsException(name);
            }

            trip.Description = request.Description;
            trip.Start = request.Start;
            trip.Seats = request.Seats;
            trip.Country = request.Country;
            trip.Start = request.Start;

            await apiContext.SaveChangesAsync(cancellationToken);

            var respone = mapper.Map<TripResponse>(trip);

            return respone;
        }

        private async Task<Trip?> GetTripByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var trip = await apiContext.Trips.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

            return trip;
        }
    }
}
