using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile: Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<PointOfInterest, PointOfInterestDto>();
            CreateMap<PointOfInterestCreateDto, PointOfInterest>();
            CreateMap<PointOfInterestUpdateDto, PointOfInterest>();
            CreateMap<PointOfInterest, PointOfInterestUpdateDto>();
        }
    }
}
