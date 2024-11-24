using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Eco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint to check and update a car if it exists
        [HttpPut("check-or-update")]
        [AllowAnonymous]
        public IActionResult CheckAndUpdateCar(CarDetails car)
        {
            // Find the existing car by ID
            var existingCar = _context.Cars.FirstOrDefault(x => x.Id == car.Id);

            if (existingCar != null)
            {
                // Update existing car details
                existingCar.Lang = car.Lang;
                existingCar.Tud = car.Tud;

                _context.SaveChanges();
                return Ok(new { message = "Car updated successfully.", car = existingCar });
            }

            // If the car does not exist, create a new car
            var newCar = new CarDetails
            {
                Id = car.Id,
                Lang = car.Lang,
                Tud = car.Tud
            };

            _context.Cars.Add(newCar);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCarById), new { id = newCar.Id }, new { message = "Car created successfully.", car = newCar });
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetCarById(Guid id)
        {
            var car = _context.Cars.Where(x => x.Id == id)
                .Select(x=> new
                {
                    x.Lang,
                    x.Tud,
                });
            if (car == null) return NotFound();
           
            return Ok(car);
        }

    }
}
