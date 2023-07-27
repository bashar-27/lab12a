using lab12a.Data;
using lab12a.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lab12a.Models.Services
{
    public class RoomService : IRoom
    {
        private readonly AsyncInnContext _context;
        private readonly IAmenities _amenities;

        public RoomService(AsyncInnContext context,IAmenities amenities)
        {
            _context = context;
            _amenities = amenities;
        }
        public async Task<Room> CreateRoom(Room room)
        {

            _context.rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }
        public async Task<List<Room>> GetRoomAsync()
        {
            var allRoom = await _context.rooms.Include(x => x.roomAmen).ToListAsync();
            return allRoom;
        }

        public async Task Delete(int id)
        {
            Room room = await GetRoomById(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }


        public async Task<Room> UpdateRoom(Room room, int id)
        {

            var existingRoom = await _context.rooms.FindAsync(id);

            if (existingRoom == null)
            {


                throw new("Room is not found");
            }
            existingRoom.Name = room.Name;
            existingRoom.Layout = room.Layout;

            await _context.SaveChangesAsync();

            return existingRoom;
        }

        public async Task<Room> AddAmenityToRoom(int roomId, int amenitiesID)
        {
            RoomAmenities roomAmenities = new RoomAmenities();
            roomAmenities.RoomID = roomId;
            roomAmenities.AmenitiesID = amenitiesID;
            _context.Entry(roomAmenities).State = EntityState.Added;
            await _context.SaveChangesAsync();
            var room = await GetRoomById(roomId);
            return room;
            //maybe without <Room>
            //comment
        }

        public async Task<Room> GetRoomById(int roomId)
        {
            Room room = await _context.rooms.Where(x => x.ID == roomId).Include(x => x.roomAmen).FirstOrDefaultAsync();
            return room;
        }

        public async Task<RoomAmenities> RemoveAmentityFromRoom(int roomId, int amenitiesID)
        {
            var removeRes = await _context.room_amenities.Where(x => x.RoomID == roomId && x.AmenitiesID == amenitiesID).FirstAsync();

            _context.Entry(removeRes).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return removeRes;
        }
    }
}
