using lab12a.Data;
using lab12a.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab12a.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {
        private readonly AsyncInnContext _context;
        private readonly IRoom _rooms;

        public HotelRoomService(AsyncInnContext context, IRoom rooms)
        {
            _context = context;
            _rooms = rooms;
        }
        public async Task<HotelRoom> CreateHotelRoom(int hotelId, int roomNumber, int roomId, decimal rate, bool petFriendly)
        {
            HotelRoom hotelRoom = new HotelRoom()
            {
                HotelId = hotelId,
                RoomNumber = roomNumber,
                RoomId = roomId,
                Rate = rate,
                PetFriendly = petFriendly,
            };

            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return hotelRoom;

        }

        public async Task DeleteHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.hotel_rooms.FindAsync(hotelId, roomNumber);

            if (hotelRoom != null)
            {
                _context.Entry(hotelRoom).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.hotel_rooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                                                   .Include(x => x.room)
                                                   .ThenInclude(x => x.roomAmen)
                                                   .ThenInclude(x => x.amenities)
                                                   .FirstOrDefaultAsync();

            return hotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _context.hotel_rooms.Where(x => x.HotelId == hotelId)
                                          .Include(x => x.room)
                                          .ThenInclude(x => x.Rooms)
                                          .ToListAsync();

            return hotelRooms;

        }

        public async Task UpdateRoom(int hotelId, int roomNumber, int roomId, decimal rate, bool petFriendly)
        {
            var hotelRoom = await _context.room_amenities.FindAsync(hotelId, roomNumber);

            if (hotelRoom != null)
            {
                hotelRoom.RoomID = roomId;
                // hotelRoom.Rate = rate;
                //hotelRoom.PetFriendly = petFriendly;

                _context.Entry(hotelRoom).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}