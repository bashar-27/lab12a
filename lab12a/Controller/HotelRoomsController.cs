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
using lab12a.Models.Services;

namespace lab12a.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _Hroom;

        public HotelRoomsController(IHotelRoom context)
        {
            _Hroom = context;
        }

        // GET: api/HotelRooms
        [HttpGet]
        [Route("{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms(int hotelId)
        {
            return await _Hroom.GetHotelRooms(hotelId);
        }


        // GET: api/HotelRooms/5
        [HttpGet]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _Hroom.GetHotelRoom(hotelId, roomNumber);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            try
            {
                await _Hroom.UpdateRoom(hotelId, roomNumber, hotelRoom.RoomId, hotelRoom.Rate, hotelRoom.PetFriendly);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, "An error occurred while updating the hotel room.");
            }
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(int hotelId,HotelRoom hotelRoom)
        {
            {
                var createdRoom = await _Hroom.CreateHotelRoom(hotelId, hotelRoom.RoomNumber, hotelRoom.RoomId, hotelRoom.Rate, hotelRoom.PetFriendly);
                return CreatedAtAction(nameof(GetHotelRoom), new { hotelId, roomNumber = createdRoom.RoomNumber }, createdRoom);

            }
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _Hroom.GetHotelRoom(hotelId, roomNumber);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            await _Hroom.DeleteHotelRoom(hotelId, roomNumber);

            return (IActionResult)hotelRoom;
        }

        private bool HotelRoomExists(int hotelId)
        {
            return _Hroom.GetHotelRooms(hotelId).Result.Any(e => e.HotelId == hotelId);
        }
    }
}
