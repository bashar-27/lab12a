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
using lab12a.Models.DTO;

namespace lab12a.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _Hroom;

        public HotelRoomsController(IHotelRoom _context)
        {
            _Hroom = _context;
        }

        // GET: api/HotelRooms
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<HotelRoomDto>>> GetHotelRooms()
        {
            return await _Hroom.GetHotelRooms();
        }


        // GET: api/HotelRooms/5
        [HttpGet]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDto>> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _Hroom.GetHotelRoom(hotelId, roomNumber);

            return hotelRoom;
        }

        // PUT: api/HotelRooms/5
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {

            await _Hroom.UpdateRoom(hotelId, roomNumber, hotelRoom);
            return Ok();

        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
     
        public async Task<ActionResult<HotelRoomDto>> PostHotelRoom(HotelRoom hotelRoom)
        {
            await _Hroom.CreateHotelRoom(hotelRoom);
            return Ok(hotelRoom);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDto>> DeleteHotelRoom(int hotelId, int roomNumber)
        {


            await _Hroom.DeleteHotelRoom(hotelId, roomNumber);

            return NoContent();
        }
    }
}