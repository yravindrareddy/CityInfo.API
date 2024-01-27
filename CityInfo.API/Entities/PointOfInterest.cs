using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("CityId")]
        public City? City { get; set; }
        public int CityId { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        public PointOfInterest(string name)
        {
            Name = name;
        }
    }
}
