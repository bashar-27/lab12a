using lab12a.Data;
using lab12a.Models.Interfaces;
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
        public async Task<Amenities> CreateAmenities(Amenities amen)
        {
            _context.amenities.Add(amen);
            await _context.SaveChangesAsync();
            return amen;
        }
        public async Task<List<Amenities>> GetAmenities()
        {
            var allAmenities = await _context.amenities.ToListAsync();
            return allAmenities;
        }

        public async Task Delete(int id)
        {
            Amenities amenity = await GetAmenityById(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }


        public async Task<Amenities> UpdateAmenities(Amenities amen, int id)
        {

            var existingAmen = await _context.amenities.FindAsync(id);

            if (existingAmen == null)
            {


                throw new("Amenity is not found");
            }
            existingAmen.Name = amen.Name;

            await _context.SaveChangesAsync();

            return existingAmen;
        }

        public async Task<Amenities> GetAmenityById(int id)
        {
            Amenities amenity = await _context.amenities.FindAsync(id);
            return amenity;
        }
    }
}
