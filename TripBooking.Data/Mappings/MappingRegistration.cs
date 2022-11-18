using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace TripBooking.Data.Mappings
{
    public static class MappingRegistration
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
