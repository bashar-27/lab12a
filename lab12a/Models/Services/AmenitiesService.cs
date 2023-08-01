using lab12a.Data;
using lab12a.Models.DTO;
using lab12a.Models.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace lab12a.Models.Services
{
    public class AmenitiesService : IAmenities
    {
        private readonly AsyncInnContext _context;

        public AmenitiesService(AsyncInnContext context)
        {
            _context = context;
        }
        public async Task<AmenitiesDto> CreateAmenities(AmenitiesDto amen)
        {
            Amenities amenity = new Amenities();
            {
                amenity.Name = amen.Name;
            }
            _context.amenities.Add(amenity);
            await _context.SaveChangesAsync();
            amen.Id = amenity.Id;
            return amen;
        }
        public async Task<List<AmenitiesDto>> GetAmenities()
        {
            var allAmenities = await _context.amenities.ToListAsync();
            List<AmenitiesDto> listDto = new List<AmenitiesDto>();
          
            foreach (var amen in allAmenities)
            {
                listDto.Add(await GetAmenityById(amen.Id));
             
            }
            return listDto;
            
        }

        public async Task Delete(int id)
        {
            var amenity = await _context.amenities.FindAsync(id);

            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAmenities(AmenitiesDto amen)
        {

            var existingAmen = await _context.amenities.FindAsync(amen.Id);
            existingAmen.Name = amen.Name;
            _context.Entry(existingAmen).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            
        }

        public async Task<AmenitiesDto> GetAmenityById(int id)
        {
            var amenity = await _context.amenities.FindAsync(id);
            AmenitiesDto amenitiesDto = new AmenitiesDto()
            {
                Id = amenity.Id,
                Name = amenity.Name
            };
            return amenitiesDto;
        }
    }
}
