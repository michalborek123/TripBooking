using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using TripBooking.Data.Reservations;
using TripBooking.Data.Trips.Repository;

namespace TripBooking.Data.Context
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase(databaseName: "TripsBooking"));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITripRepository, TripRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

            return services;
        }
    }
}
