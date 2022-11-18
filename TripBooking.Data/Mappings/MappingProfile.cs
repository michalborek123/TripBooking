using AutoMapper;
using TripBooking.Core.Trips.Commands;
using TripBooking.Core.Trips.Responses;
using TripBooking.Data.Trips.Model;

namespace TripBooking.Data.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTripRequest, Trip>();
            CreateMap<Trip, TripResponse>();
        }
    }
}
