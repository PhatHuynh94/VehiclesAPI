using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehiclesAPI.Data;
using VehiclesAPI.DTO;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : Controller
    {
        private readonly DataContext _dataContext;

        public VehiclesController(DataContext context)
        {
            _dataContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _dataContext.Vehicles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            return await _dataContext.Vehicles.FindAsync(id);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicle(string? make = null, string? model = null, int? year = null)
        {
                   
            //If only year has a value
            if (make == null && model == null && year != null)
                return await _dataContext.Vehicles.Where(x => x.Year == year).ToListAsync();
            //If only make has a value
            else if (make != null && model == null && year == null)
                return await _dataContext.Vehicles.Where(x => x.Make.ToLower() == make.ToLower()).ToListAsync();
            //If only model has a value
            else if (make == null && model != null && year == null)
                return await _dataContext.Vehicles.Where(x => x.Model.ToLower() == model.ToLower()).ToListAsync();
            //If only make and model have value
            else if (make != null && model != null && year == null)
                return await _dataContext.Vehicles.Where(x => x.Model.ToLower() == model.ToLower() && x.Make.ToLower() == make.ToLower()).ToListAsync();
            //If only make and year have values
            else if (make != null && model == null && year != null)
                return await _dataContext.Vehicles.Where(x => x.Year == year && x.Make.ToLower() == make.ToLower()).ToListAsync();
            //If only model and year have values
            else if (make == null && model != null && year != null)
                return await _dataContext.Vehicles.Where(x => x.Year == year && x.Model.ToLower() == model.ToLower()).ToListAsync();

            //If all three parameters are not null
            return await _dataContext.Vehicles.Where(x => x.Year == year && x.Model.ToLower() == model.ToLower() && x.Make.ToLower() == make.ToLower()).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> Post(VehicleRegisterDTO registerDto)
        {
            if (String.IsNullOrWhiteSpace(registerDto.Model))
                return BadRequest("Model field is empty");
            if (String.IsNullOrWhiteSpace(registerDto.Make))
                return BadRequest("Make field is empty");
            if (registerDto.Year > 2050 || registerDto.Year < 1950)
                return BadRequest("Vehicle year should be in between 1950 and 2050");

            var newVehicle = new Vehicle()
            {
                Year = registerDto.Year,
                Make = registerDto.Make,
                Model = registerDto.Model
            };

            _dataContext.Vehicles.Add(newVehicle);
            await _dataContext.SaveChangesAsync();

            return Ok(newVehicle);
        }

        [HttpPut]
        public async Task<ActionResult<VehicleDTO>> Put(VehicleDTO vehicleDto)
        {
            if (String.IsNullOrWhiteSpace(vehicleDto.Model))
                return BadRequest("Model field is empty");
            if (String.IsNullOrWhiteSpace(vehicleDto.Make))
                return BadRequest("Make field is empty");
            if (vehicleDto.Year > 2050 || vehicleDto.Year < 1950)
                return BadRequest("Vehicle year should be in between 1950 and 2050");

            var existingVehicle = _dataContext.Vehicles.Where(x => x.Id == vehicleDto.Id).SingleOrDefault<Vehicle>();

            if (existingVehicle != null)
            {
                existingVehicle.Make = vehicleDto.Make;
                existingVehicle.Model = vehicleDto.Model;
                existingVehicle.Year = vehicleDto.Year;
                await _dataContext.SaveChangesAsync();
                return Ok(vehicleDto);
            }

            return BadRequest("Vehicle does not exist");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> Delete(int id)
        {
            var delVehicle = _dataContext.Vehicles.Where(x => x.Id == id).SingleOrDefault<Vehicle>();

            if (delVehicle != null)
            {
                _dataContext.Vehicles.Remove(delVehicle);
                await _dataContext.SaveChangesAsync();
                return Ok(delVehicle);
            }

            return BadRequest("Vehicle does not exist");            
        }
    }
}
