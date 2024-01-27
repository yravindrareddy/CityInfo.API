using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
                {
                    new CityDto() { Id = 1, Name = "New York City" , Description= "USA Tech hub" ,
                        PointOfInterests = new List<PointOfInterestDto> {
                           new PointOfInterestDto {
                                Id = 1,
                                Name= "City Park",
                                Description = "A large urban park with walking trails, picnic areas, and a beautiful lake."
                              },
                              new PointOfInterestDto {
                                Id = 2,
                                Name = "Historic Museum",
                                Description = "Explore the city's rich history through artifacts, exhibits, and interactive displays."
                              }
                    }
                    },
                    new CityDto() { Id = 2, Name= "Delhi" , Description= "Capital City of India" },
                    new CityDto() { Id = 3, Name="Copenhegan", Description="Denmark city" }
                };
        }
    }
}
