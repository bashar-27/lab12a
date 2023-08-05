using lab12a.Models.DTO;

namespace lab12a.Models.Interfaces
{
    public interface IHotelRoom
    {
        /// <summary>
        /// Creates a new hotel room and adds it to the data store.
        /// </summary>
        /// <param name="hotelRoom">The DTO containing the details of the hotel room to be created.</param>
        /// <returns>A DTO representation of the created hotel room.</returns>
        Task<HotelRoomDto> CreateHotelRoom(HotelRoom hotelRoom);

        /// <summary>
        /// Retrieves a list of all hotel rooms along with their details.
        /// </summary>
        /// <returns>A list of DTO representations of hotel rooms.</returns>
        Task<List<HotelRoomDto>> GetHotelRooms();
        /// <summary>
        /// Retrieves a hotel room by its hotel ID and room number along with its details.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <param name="roomNumber">The number of the room.</param>
        /// <returns>A DTO representation of the retrieved hotel room.</returns>
        Task<HotelRoomDto> GetHotelRoom(int hotelId, int roomNumber);
        /// <summary>
        /// Updates the details of a hotel room based on the provided hotel ID, room number, and updated hotel room details.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <param name="roomNumber">The number of the room.</param>
        /// <param name="hotelRoom">The updated hotel room details.</param>
        /// <returns>A DTO representation of the updated hotel room.</returns>
        Task<HotelRoomDto> UpdateRoom(int hotelId, int roomNumber, HotelRoom hotelRoom);
        /// <summary>
        /// Deletes a hotel room based on the provided hotel ID and room number.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <param name="roomNumber">The number of the room.</param>
        /// <returns>A DTO representation of the deleted hotel room.</returns>
        Task<HotelRoomDto> DeleteHotelRoom(int hotelId, int roomNumber);
    }
}
