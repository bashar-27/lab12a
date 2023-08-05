using lab12a.Data;
using lab12a.Models.DTO;
using lab12a.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lab12a.Models.Services
{
    public class HotelService : IHotel
    {
        private readonly AsyncInnContext _context;
        // private readonly IHotelRoom _hotelRooms;

        /// <summary>
        /// Creates a new hotel and adds it to the database.
        /// </summary>
        /// <param name="hotel">The hotel to be created.</param>
        /// <returns>A DTO representation of the created hotel.</returns>
        public HotelService(AsyncInnContext context)
        {
            _context = context;
          //  _hotelRooms = hotelRooms;
        }
        public async Task<HotelDto> CreateHotel(Hotel hotel)
        {
            var hotelDTO = new HotelDto();
            hotelDTO.Name = hotel.Name;
            hotelDTO.State = hotel.State;
            hotelDTO.StreetAddress = hotel.StreetAddress;
            hotelDTO.City = hotel.City;
            hotel.Country = hotel.Country;
            hotel.Phone = hotel.Phone;
            _context.hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotelDTO;
        }
        /// <summary>
        /// Retrieves a list of all hotels along with their details and associated rooms.
        /// </summary>
        /// <returns>A list of DTO representations of hotels.</returns>
        public async Task<List<HotelDto>> GetHotelAsync()
        {
            var hotels = await _context.hotels.Include(x => x.Rooms)
                                              .ThenInclude(x => x.room)
                                              .ToListAsync();
            List<HotelDto> dtos = new List<HotelDto>();
            foreach (var hotel in hotels)
                dtos.Add(await GetHotelById(hotel.Id));

            return dtos;
        }

        /// <summary>
        /// Deletes a hotel based on its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the hotel to delete.</param>
        /// <returns>A DTO representation of the deleted hotel.</returns>
        public async Task<HotelDto> Delete(int id)
        {
            var hotelDTO = await GetHotelById(id);
            var hotel = await _context.hotels.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return hotelDTO;

        }

        /// <summary>
        /// Updates the details of a hotel in the database.
        /// </summary>
        /// <param name="hotel">The updated hotel details.</param>
        /// <param name="id">The ID of the hotel to update.</param>
        /// <returns>A DTO representation of the updated hotel.</returns>
        public async Task<HotelDto> UpdateHotel(Hotel hotel , int id)
        {

            var existingHotel = await _context.hotels.Where(x => x.Id == hotel.Id).FirstOrDefaultAsync();

            var updatedHotel = new HotelDto
            {
                Id = id,
                Name = hotel.Name,
                Phone = hotel.Phone,
                City = hotel.City,
                StreetAddress = hotel.StreetAddress,
                State = hotel.State,
                Country = hotel.Country,

            };
                                                  


            existingHotel.Name = hotel.Name;
            existingHotel.Phone = hotel.Phone;
            existingHotel.City = hotel.City;
            existingHotel.StreetAddress = hotel.StreetAddress;
            existingHotel.State = hotel.State;
            existingHotel.Country = hotel.Country;


            _context.Entry(existingHotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedHotel;

        }
        /// <summary>
        /// Retrieves a hotel by its ID along with its details and associated rooms.
        /// </summary>
        /// <param name="id">The ID of the hotel to retrieve.</param>
        /// <returns>A DTO representation of the retrieved hotel.</returns>

        public async Task<HotelDto> GetHotelById(int id)
        {
            var hotel = await _context.hotels.Where(x => x.Id == id).FirstOrDefaultAsync();
            var dto = await _context.hotels.Select(x => new HotelDto {
                Id = hotel.Id,
                Name = hotel.Name,
                Phone = hotel.Phone,
                City = hotel.City,
                StreetAddress = hotel.StreetAddress,
                State = hotel.State,
                Country = hotel.Country

            }).FirstOrDefaultAsync();
                                             
          
            return dto;
        }

      
    }
}