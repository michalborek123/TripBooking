using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TripBooking.Core.Reservations.Commands;
using TripBooking.Core.Reservations.Validations;

namespace TripBooking.Core.Infrastructure
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            services.AddScoped<IValidator<TripRegistrationRequest>, TripRegistrationRequestValidator>();

            return services;
        }
    }
}
