using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TripBooking.Data.Context;
using TripBooking.Data.Trips.Model;

namespace TripBooking.Data.Trips.Repository
{
    public interface ITripRepository
    {
        Task<Trip?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }

    internal class TripRepository : ITripRepository
    {
        private readonly ApiContext apiContext;

        public TripRepository(ApiContext apiContext)
        {
            this.apiContext = apiContext;

            apiContext.Trips.Add(new Trip()
            {
                Name = "Ibiza",
                Description = "Ibiza party"
            });

            apiContext.SaveChanges();
        }

        public async Task<Trip?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var result = await apiContext.Trips.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

            return result;
        }
    }
}
