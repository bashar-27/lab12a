using lab12a.Data;
using lab12a.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lab12a.Models.Services
{
    public class HotelService : IHotel
    {
        private readonly AsyncInnContext _context;

        public HotelService(AsyncInnContext context)
        {
            _context = context;
        }
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            _context.hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }
        public async Task<List<Hotel>> GetHotelAsync()
        {
            var hotels = await _context.hotels.ToListAsync();
            return hotels;
        }

        public async Task<Hotel> Delete(int id)
        {
            Hotel hotel = await GetHotelById(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return hotel;
        }


        public async Task<Hotel> UpdateHotel(Hotel hotel, int id)
        {

            var existingHotel = await _context.hotels.FindAsync(id);

            if (existingHotel == null)
            {


                throw new("Hotel not found");
            }
            existingHotel.Name = hotel.Name;
            existingHotel.Phone = hotel.Phone;
            existingHotel.City = hotel.City;
            existingHotel.StreetAddress = hotel.StreetAddress;
            existingHotel.Country = hotel.Country;
            existingHotel.State = hotel.State;


            await _context.SaveChangesAsync();

            return existingHotel;
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            Hotel hotel = await _context.hotels.FindAsync(id);
            return hotel;
        }
    }
}
