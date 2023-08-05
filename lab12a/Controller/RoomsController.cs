using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab12a.Data;
using lab12a.Models;
using lab12a.Models.Interfaces;
using lab12a.Models.DTO;

namespace lab12a.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        public RoomsController(IRoom room)
        {
            _room = room;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> Getrooms()
        {
            return await _room.GetRoomAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoom(int id)
        {
            var roomDto = await _room.GetRoomById(id);
            return roomDto;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(Room room,int id)
        {
            if (id != room.ID)
            {
                return BadRequest();
            }

             await _room.UpdateRoom(room,id);
            return Ok();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Route("{roomId}/{amenitiesID}")]
        public async Task<ActionResult<RoomDto>> PostRoom(Room room)
        {
            
            return await _room.CreateRoom(room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
      
        public async Task<ActionResult<RoomDto>> DeleteRoom(int roomId)
        {
           

            await _room.Delete(roomId);

            return Ok();

        }
        [HttpPost]
        [Route("{roomId}/Amenities/{amenityId}")]
        public async Task<RoomAmenities> PostRoomaded(int roomId, int amenityId)
        {
            return await _room.AddAmenityToRoom(roomId, amenityId);
        }

        [HttpDelete]
        [Route("{roomId}/Amenities/{amenityId}")]
        public async Task<RoomAmenities> DeleteRoomaded(int roomId, int amenityId)
        {
            return await _room.RemoveAmentityFromRoom(roomId, amenityId);
        }

    }
}
