using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TripBooking.Core.Trips.Commands;
using TripBooking.Data.Context;
using TripBooking.Data.Trips.Model;

namespace TripBooking.Data.Trips.Repository
{
    public interface ITripRepository
    {
        Task<Trip> AddTripAsync(CreateTripRequest request, CancellationToken cancellationToken = default);
        Task<Trip?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }

    internal class TripRepository : ITripRepository
    {
        private readonly ApiContext apiContext;

        public TripRepository(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }

        public async Task<Trip> AddTripAsync(CreateTripRequest request, CancellationToken cancellationToken = default)
        {
            var trip = await GetByNameAsync(request.Name, cancellationToken);

            if (trip is not null)
            {
                throw new Exception($"Trip {request.Name} already exists.");
            }

            var newTrip = new Trip()
            {
                Name = request.Name,
                Description = request.Description,
                Country = request.Country,
                Start = request.Start,
                Seats = request.Seats
            };


            await apiContext.Trips.AddAsync(newTrip, cancellationToken);
            apiContext.SaveChanges();

            return newTrip;
        }

        public async Task<Trip?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var result = await apiContext.Trips.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

            return result;
        }
    }
}
