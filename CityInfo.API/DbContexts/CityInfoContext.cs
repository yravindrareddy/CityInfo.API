using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointOfInterest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("New York")
                {
                    Id = 1,
                    Description = "Great place"
                },
                new City("Delhi")
                {
                    Id = 2,
                    Description = "Capital City"
                },
                new City("Copenhegan")
                {
                    Id = 3,
                    Description = "Denmark capital"
                });

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("City Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "A large urban park with walking trails, picnic areas, and a beautiful lake."
                },
                new PointOfInterest("Historic Museum")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "Explore the city's rich history through artifacts, exhibits, and interactive displays."
                },
                new PointOfInterest("The Little Mermaid")
                {
                    Id = 3,
                    CityId = 3,
                    Description = "The Little Mermaid sits on a rock by the waterside at the Langelinie promenade."
                },
                new PointOfInterest("Tivoli Gardens")
                {
                    Id = 4,
                    CityId = 3,
                    Description = "Tivoli offers a mix of historic charm and modern entertainment."
                },
                new PointOfInterest("Red Fort")
                {
                    Id = 5,
                    CityId = 2,
                    Description = "Historic monument."
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
