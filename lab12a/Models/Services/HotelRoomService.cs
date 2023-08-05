using lab12a.Data;
using lab12a.Models.DTO;
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
        /// <summary>
        /// Creates a new hotel room and adds it to the database.
        /// </summary>
        /// <param name="HotelRoom">The hotel room to be created.</param>
        /// <returns>A DTO representation of the created hotel room.</returns>
        public async Task<HotelRoomDto> CreateHotelRoom(HotelRoom HotelRoom)
        {


            HotelRoom HotelRooms = new HotelRoom()
            {
                HotelId = HotelRoom.HotelId,
                Rate = HotelRoom.Rate,
                RoomNumber = HotelRoom.RoomNumber,
                PetFriendly = HotelRoom.PetFriendly,
                RoomId = HotelRoom.RoomId,
                //ملاحظة 
                room = await _context.rooms.Where(x=>x.ID==HotelRoom.RoomId).FirstOrDefaultAsync(),
                hotel = await _context.hotels.Where(x=>x.Id==HotelRoom.HotelId).FirstOrDefaultAsync(),
            };

            var hotDto = new HotelRoomDto()
            {
                HotelId = HotelRoom.HotelId,
                RoomNumber = HotelRoom.RoomNumber,
                RoomId = HotelRoom.RoomId,
                Rate = HotelRoom.Rate,
                PetFriendly = HotelRoom.PetFriendly,
            };
            _context.hotel_rooms.Add(HotelRoom);
            await _context.SaveChangesAsync();
            return hotDto;
        }


        /// <summary>
        /// Deletes a hotel room based on the provided hotel and room numbers.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <param name="roomNumber">The number of the room.</param>
        /// <returns>A DTO representation of the deleted hotel room.</returns>
        public async Task<HotelRoomDto> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.hotel_rooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber).FirstOrDefaultAsync();
            var hrDto = await GetHotelRoom(hotelId, roomNumber);
            _context.hotel_rooms.Remove(hotelRoom);
             await _context.SaveChangesAsync();
            return hrDto;

        }
        /// <summary>
        /// Retrieves a hotel room based on the provided hotel and room numbers.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <param name="roomNumber">The number of the room.</param>
        /// <returns>A DTO representation of the retrieved hotel room.</returns>
        public async Task<HotelRoomDto> GetHotelRoom(int hotelId, int roomNumber)
        {

            var hotelRoom = await _context.hotel_rooms
                                                   .Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber).FirstOrDefaultAsync();
                                                  
            HotelRoomDto dto = new HotelRoomDto()
            {
                HotelId = hotelRoom.HotelId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly,
                RoomId = hotelRoom.RoomId,
               // Room = await _rooms.GetRoomById(hotelRoom.RoomId)
            };
            return dto;

            //return hotelRoom;
        }
        /// <summary>
        /// Retrieves a list of all hotel rooms along with their details.
        /// </summary>
        /// <returns>A list of DTO representations of hotel rooms.</returns>
        public async Task<List<HotelRoomDto>> GetHotelRooms()
        {
            var hotelRooms = await _context.hotel_rooms.Include(x=>x.hotel)
                                                       .Include(x => x.room)
                                                       .ThenInclude(x=>x.roomAmen)
                                                       .ThenInclude(k=>k.amenities)
                                                       .Select(a=>new HotelRoomDto
                                                       {
                                                           HotelId=a.HotelId,
                                                           Rate = a.Rate,
                                                           PetFriendly = a.PetFriendly,
                                                           RoomId = a.RoomId,
                                                           RoomNumber=a.RoomNumber,
                                                           Room =new RoomDto
                                                           {
                                                               Id=a.room.ID,
                                                               Name=a.room.Name,
                                                               Amenities=a.room.roomAmen.Select(roomA=>new AmenitiesDto
                                                               {
                                                                   Id=roomA.amenities.Id,
                                                                   Name=roomA.amenities.Name,
                                                               }).ToList(),
                                                           },
                                                       })
                                                       .ToListAsync();

            //var HR = new List<HotelRoomDto>();
            //foreach (var hotelRoom in hotelRooms)
            //{
            //    HR.Add(await GetHotelRoom(hotelRoom.HotelId, hotelRoom.RoomNumber));
            //}
            return hotelRooms;


        }
        
        /// <summary>
        /// Updates the details of a hotel room based on the provided hotel and room numbers.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <param name="roomNumber">The number of the room.</param>
        /// <param name="hotelRoom">The updated hotel room details.</param>
        /// <returns>A DTO representation of the updated hotel room.</returns>
        public async Task<HotelRoomDto> UpdateRoom(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            var hotRooms = await _context.hotel_rooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber).FirstOrDefaultAsync();
            //var hotelRoom = await _context.room_amenities.FindAsync(hotelId, roomNumber);
            var hRoom = await GetHotelRoom(hotelId, roomNumber);
            hRoom.RoomNumber = hotelRoom.RoomNumber;
            hRoom.Rate = hotelRoom.Rate;
            hRoom.PetFriendly = hotelRoom.PetFriendly;
            
            hotRooms.HotelId=hotelRoom.HotelId;
            hotRooms.RoomNumber = hotelRoom.RoomNumber;
            hotRooms.Rate = hotelRoom.Rate;
            hotRooms.PetFriendly = hotelRoom.PetFriendly;
            hotRooms.RoomId = hotelRoom.RoomId;

            _context.Entry(hRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hRoom;
        }
    }
}

