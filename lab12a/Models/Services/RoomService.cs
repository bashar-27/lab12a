using lab12a.Data;
using lab12a.Models.DTO;
using lab12a.Models.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace lab12a.Models.Services
{
    public class RoomService : IRoom
    {
        private readonly AsyncInnContext _context;
        private readonly IAmenities _amenities;

        public RoomService(AsyncInnContext context, object @object)
        {
            _context = context;
            //_amenities = amenities;
        }

        public RoomService(AsyncInnContext context)
        {
            _context = context;
            //_amenities = amenities;
        }
        /// <summary>
        /// Creates a new room and adds it to the database.
        /// </summary>
        /// <param name="room">The room to be created.</param>
        /// <returns>A DTO representation of the created room.</returns>
        public async Task<RoomDto> CreateRoom(Room room)
        {
            var room1 = new RoomDto()
            {
                Name = room.Name,
                Layout = room.Layout,
                Amenities = _context.room_amenities.Select(x => new AmenitiesDto
                {
                    Id = x.amenities.Id,
                    Name = x.amenities.Name
                }).ToList()

            };

            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return room1;
        }
        /// <summary>
        /// Retrieves a list of all rooms along with their details and associated amenities.
        /// </summary>
        /// <returns>A list of DTO representations of rooms.</returns>
        public async Task<List<RoomDto>> GetRoomAsync()
        {

            return await _context.rooms.Include(x => x.roomAmen)
                                       .ThenInclude(x => x.amenities)
                                       .Select(x => new RoomDto
                                       {
                                           Id = x.ID,
                                           Name = x.Name,
                                           Layout = x.Layout,
                                           Amenities = x.roomAmen.Select(y => new AmenitiesDto
                                           {
                                               Id = y.amenities.Id,
                                               Name = y.amenities.Name
                                           }).ToList()

                                       }).ToListAsync();

        }

        /// <summary>
        /// Retrieves a room by its ID along with its details and associated amenities.
        /// </summary>
        /// <param name="roomId">The ID of the room to retrieve.</param>
        /// <returns>A DTO representation of the retrieved room.</returns>
        public async Task<RoomDto> GetRoomById(int roomId)
        {
            var room = await _context.rooms.Where(x => x.ID == roomId).FirstOrDefaultAsync();
            var Dto = await _context.rooms.Select(g => new RoomDto
            {
                Id = roomId,
                Name = room.Name,
                Layout = room.Layout,
                Amenities = g.roomAmen.Select(a => new AmenitiesDto
                {
                    Id = a.amenities.Id,
                    Name = a.amenities.Name
                }).ToList(),
            }).FirstOrDefaultAsync();
            return Dto;
        }

        /// <summary>
        /// Updates the details of a room in the database.
        /// </summary>
        /// <param name="room">The updated room details.</param>
        /// <param name="roomid">The ID of the room to update.</param>
        /// <returns>A DTO representation of the updated room.</returns>
        public async Task<RoomDto> UpdateRoom(Room room,int roomid)
        {
            RoomDto roomDto = await GetRoomById(roomid);

            Room existingRoom = await _context.rooms.Where(x=>x.ID==roomid).FirstOrDefaultAsync();


            existingRoom.Name = room.Name;
            existingRoom.Layout = room.Layout;

            _context.Entry(existingRoom).State = EntityState.Modified;
            var newRoom = await GetRoomById(existingRoom.ID);
            await _context.SaveChangesAsync();
            return roomDto;


        }
        /// <summary>
        /// Deletes a room based on its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>
        /// <returns>A DTO representation of the deleted room.</returns>
        public async Task<RoomDto> Delete(int id)
        {
            RoomDto roomDTO = await GetRoomById(id);
            Room room =await _context.rooms.Where(x=>x.ID ==id).FirstOrDefaultAsync();
            _context.rooms.Remove(room);
            await _context.SaveChangesAsync();
            return roomDTO;
        }


        /// <summary>
        /// Associates an amenity with a room and adds the association to the database.
        /// </summary>
        /// <param name="roomId">The ID of the room to which the amenity will be added.</param>
        /// <param name="amenitiesID">The ID of the amenity to add to the room.</param>
        /// <returns>The newly created RoomAmenities object representing the association.</returns>
        public async Task<RoomAmenities> AddAmenityToRoom(int roomId, int amenitiesID)
        {
            RoomAmenities roomAmenities = new RoomAmenities()
            {
                RoomID = roomId,
                AmenitiesID = amenitiesID,
                room = await _context.rooms.Where(x=>x.ID==roomId).FirstOrDefaultAsync(),
                amenities=await _context.amenities.Where(x=>x.Id ==amenitiesID).FirstOrDefaultAsync(),
            };

            _context.Entry(roomAmenities).State = EntityState.Added;
            await _context.SaveChangesAsync();
         
            return roomAmenities;
            //maybe without <Room>
            //comment
        }


        /// <summary>
        /// Removes an amenity association from a room and deletes it from the database.
        /// </summary>
        /// <param name="roomId">The ID of the room from which the amenity association will be removed.</param>
        /// <param name="amenitiesID">The ID of the amenity to remove from the room.</param>
        /// <returns>The removed RoomAmenities object representing the removed association.</returns>
        public async Task<RoomAmenities> RemoveAmentityFromRoom(int roomId, int amenitiesID)
        {
            var removeRes = await _context.room_amenities.Where(x => x.AmenitiesID == amenitiesID && x.RoomID ==roomId).FirstAsync();

            _context.Entry(removeRes).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return removeRes;
        }
    }
}
