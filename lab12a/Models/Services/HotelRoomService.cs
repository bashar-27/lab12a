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
        public async Task<HotelRoom> CreateHotelRoom(HotelRoom HotelRoom)
        {


            HotelRoom HotelRooms = new HotelRoom()
            {
                HotelId = HotelRoom.HotelId,
                Rate = HotelRoom.Rate,
                RoomNumber = HotelRoom.RoomNumber,
                room = await _context.rooms.Where(x => x.ID == HotelRoom.RoomId).FirstOrDefaultAsync(),
                hotel = await _context.hotels.Where(x => x.Id == HotelRoom.RoomId).FirstOrDefaultAsync()
            };
            // HotelRoom.Hotel = await _context.Hotels.Where(x => x.Id == HotelRoom.HotelId).FirstOrDefaultAsync();
            //HotelRoom.Room = await _context.Rooms.Where(x => x.Id == HotelRoom.RoomId).FirstOrDefaultAsync();
            //  _context.Entry(HotelRooms).State= EntityState.Added;
            _context.hotel_rooms.Add(HotelRoom);
            await _context.SaveChangesAsync();
            return HotelRooms;
        }

        public async Task DeleteHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.hotel_rooms.FindAsync(hotelId, roomNumber);

            if (hotelRoom != null)
            {
                _context.hotel_rooms.Remove(hotelRoom);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            //var hotelRoom = await _context.hotel_rooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
            //                                       .Include(x => x.room)
            //                                       .ThenInclude(x => x.roomAmen)
            //                                       .ThenInclude(x => x.amenities)
            //                                       .FirstOrDefaultAsync();

            //return hotelRoom;

            var HotelRoom = await _context.hotel_rooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber).FirstOrDefaultAsync();
            return HotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _context.hotel_rooms.Where(x=>x.HotelId ==hotelId)
                                                       .Include(x=>x.room)
                                                       .ThenInclude(x=>x.Rooms)
                                                       .ToListAsync();

            List<HotelRoom> HR = new List<HotelRoom>();
            foreach(var hotelRoom in hotelRooms) {
                HR.Add(await GetHotelRoom(hotelRoom.HotelId, hotelRoom.RoomNumber));
            }
            return HR;


        }
        public async Task UpdateRoom(int hotelId, int roomNumber,HotelRoom hotelRoom)
        {
            //var hotelRoom = await _context.room_amenities.FindAsync(hotelId, roomNumber);
            var Temproom = await GetHotelRoom(hotelId, roomNumber);
            Temproom.RoomNumber = roomNumber;
            Temproom.Rate = hotelRoom.Rate;
            Temproom.PetFriendly =hotelRoom.PetFriendly;
            _context.Entry(Temproom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    } 
}
    
