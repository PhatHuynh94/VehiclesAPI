namespace VehiclesAPI.Models
{
    public interface IVehicle
    {
        int Id { get; set; }
        int Year { get; set; }
        string Make { get; set; }
        string Model { get; set; }
    }
}
