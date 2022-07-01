using System.ComponentModel.DataAnnotations;

namespace VehiclesAPI.DTO
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        [Range(1950, 2050)]
        public int Year { get; set; }
        [Required, MinLength(1)]
        public string Make { get; set; }
        [Required, MinLength(1)]
        public string Model { get; set; }
    }
}
