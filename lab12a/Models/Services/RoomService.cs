using lab12a.Data;
using lab12a.Models.DTO;
using lab12a.Models.Interfaces;
using Microsoft.AspNetCore.Components;
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
        public async Task<RoomDto> CreateRoom(RoomDto room)
        {
            Room room1 = new Room()
            {
                Name = room.Name,
              
            };

            _context.Entry(room1).State = EntityState.Added;
            await _context.SaveChangesAsync();
            room.Id = room1.ID;
            return room;
        }
        public async Task<List<RoomDto>> GetRoomAsync()
        {
            var allRoom = await _context.rooms.Include(x => x.roomAmen)
                                              .ThenInclude(x=>x.amenities)
                                              .ToListAsync();
            List<RoomDto>dtos = new List<RoomDto>();
            foreach (var item in dtos)
            dtos.Add(await GetRoomById(item.Id));
            return dtos;

        }

        public async Task<RoomDto> GetRoomById(int roomId)
        {
            var room = await _context.rooms.Where(x => x.ID == roomId)
                                            .Include(x => x.roomAmen)
                                            .ThenInclude(x=>x.amenities)
                                            .Include(x=>x.Rooms)
                                            .FirstOrDefaultAsync();

            RoomDto roomDTO= new RoomDto()
            {
                Id = room.ID,
                Name = room.Name,
                Layout = room.Layout.ToString(),
                Amenities = await _amenities.GetAmenities()
            };
            return roomDTO;
        }

        public async Task UpdateRoom(RoomDto roomDTO)
        {

            var existingRoom = await _context.rooms.FindAsync(roomDTO.Id);

          
            existingRoom.Name = roomDTO.Name;
           // existingRoom.Layout =(Layout)Enum.Parse(typeof(Layout),roomDTO.Layout);

            _context.Entry(roomDTO).State = EntityState.Modified;
            await _context.SaveChangesAsync();

           
        }

        public async Task Delete(int id)
        {
            Room room = await _context.rooms.FindAsync(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }


        public async Task<RoomDto> AddAmenityToRoom(int roomId, int amenitiesID)
        {
            RoomAmenities roomAmenities = new RoomAmenities()
            {
                RoomID = roomId,
                AmenitiesID = amenitiesID
            };
          
            _context.Entry(roomAmenities).State = EntityState.Added;
            await _context.SaveChangesAsync();
            var roomDto = await GetRoomById(roomId);
            return roomDto;
            //maybe without <Room>
            //comment
        }

        public async Task<RoomAmenities> RemoveAmentityFromRoom(int roomId, int amenitiesID)
        {
            RoomAmenities removeRes = await _context.room_amenities.Where(x => x.AmenitiesID == amenitiesID).FirstAsync();

            _context.Entry(removeRes).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return removeRes;
        }
    }
}
