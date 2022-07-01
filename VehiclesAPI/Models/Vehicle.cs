using System.ComponentModel.DataAnnotations;

namespace VehiclesAPI.Models
{
    public class Vehicle : IVehicle
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Make { get; set; } 
        public string Model { get; set; } 
    }
}
