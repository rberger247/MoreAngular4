﻿//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using vega.Controllers.Resources;
//using vega.Core.Models;
//using vega.Core;

//namespace vega.Controllers
//{
//    [Route("/api/vehicles")]
//    public class VehiclesController : Controller
//    {
//        private readonly IMapper mapper;
//        private readonly IVehicleRepository repository;
//        private readonly IUnitOfWork unitOfWork;

//        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
//        {
//            this.unitOfWork = unitOfWork;
//            this.repository = repository;
//            this.mapper = mapper;
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
//            vehicle.LastUpdate = DateTime.Now;

//            repository.Add(vehicle);
//            await unitOfWork.CompleteAsync();

//            vehicle = await repository.GetVehicle(vehicle.Id);

//            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

//            return Ok(result);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var vehicle = await repository.GetVehicle(id);

//            if (vehicle == null)
//                return NotFound();

//            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
//            vehicle.LastUpdate = DateTime.Now;

//            await unitOfWork.CompleteAsync();

//            vehicle = await repository.GetVehicle(vehicle.Id);
//            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

//            return Ok(result);
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteVehicle(int id)
//        {
//            var vehicle = await repository.GetVehicle(id, includeRelated: false);

//            if (vehicle == null)
//                return NotFound();

//            repository.Remove(vehicle);
//            await unitOfWork.CompleteAsync();

//            return Ok(id);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetVehicle(int id)
//        {
//            var vehicle = await repository.GetVehicle(id);

//            if (vehicle == null)
//                return NotFound();

//            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

//            return Ok(vehicleResource);
//        }


//    }
//}









using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AThirdCarDealership.Persistence;
using vega.Models;

namespace AThirdCarDealership.Controllers
{
    [Produces("application/json")]
    [Route("api/Vehicles")]
    public class VehiclesController : Controller
    {
        private readonly VegaDbContext _context;

        public VehiclesController(VegaDbContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public IEnumerable<Vehicle> GetVehicle()
        {
            return _context.Vehicle;
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await _context.Vehicle.SingleOrDefaultAsync(m => m.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle([FromRoute] int id, [FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vehicles
        [HttpPost]
        public async Task<IActionResult> PostVehicle([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           

            _context.Vehicle.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await _context.Vehicle.SingleOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(vehicle);
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}

