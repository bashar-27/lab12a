using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab12a.Data;
using lab12a.Models;
using lab12a.Models.Interfaces;
using lab12a.Models.DTO;

namespace lab12a.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenities _amenity;

        public AmenitiesController(IAmenities amenity)
        {
            _amenity = amenity;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenitiesDto>>> Getamenities()
        {
            return await _amenity.GetAmenities();
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenitiesDto>> GetAmenityById(int id)
        {
            var amenitiesDto = await _amenity.GetAmenityById(id);
            return amenitiesDto;
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenities(AmenitiesDto amenitiesDto, int id)
        {
            if (id != amenitiesDto.Id)
            {
                return BadRequest();
            }
            try
            {
                await _amenity.UpdateAmenities(amenitiesDto);
            }
            catch(DbUpdateConcurrencyException)
            {
                if(amenitiesDto.Id != id)
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

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AmenitiesDto>> PostAmenities(AmenitiesDto amenitiesDto)
        {
            await _amenity.CreateAmenities(amenitiesDto);

            return CreatedAtAction("GetAmenityById", new { id = amenitiesDto.Id }, amenitiesDto);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AmenitiesDto>> DeleteAmenities(int id)
        {
            var amenDto = await _amenity.GetAmenityById(id);
            await _amenity.Delete(id);
            return Ok();
        }


    }
}
