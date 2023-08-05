using lab12a.Models.DTO;

namespace lab12a.Models.Interfaces
{
    public interface IRoom
    {
        /// <summary>
        /// Creates a new room and adds it to the data store.
        /// </summary>
        /// <param name="room">The room to be created.</param>
        /// <returns>A DTO representation of the created room.</returns>
        Task<RoomDto> CreateRoom(Room room);
        /// <summary>
        /// Retrieves a list of all rooms along with their details.
        /// </summary>
        /// <returns>A list of DTO representations of rooms.</returns>
        Task<List<RoomDto>> GetRoomAsync();
        /// <summary>
        /// Retrieves a room by its ID along with its details.
        /// </summary>
        /// <param name="roomId">The ID of the room to retrieve.</param>
        /// <returns>A DTO representation of the retrieved room.</returns>
        Task<RoomDto> GetRoomById(int roomId);
        /// <summary>
        /// Updates the details of an existing room in the data store.
        /// </summary>
        /// <param name="room">The updated room details.</param>
        /// <param name="id">The ID of the room to update.</param>
        /// <returns>A DTO representation of the updated room.</returns>
        Task<RoomDto>UpdateRoom(Room room,int id);

        /// <summary>
        /// Deletes a room based on its ID from the data store.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>
        /// <returns>A DTO representation of the deleted room.</returns>
        Task<RoomDto> Delete(int id);
        /// <summary>
        /// Associates an amenity with a room and adds the association to the data store.
        /// </summary>
        /// <param name="roomId">The ID of the room to which the amenity will be added.</param>
        /// <param name="amenitiesID">The ID of the amenity to add to the room.</param>
        /// <returns>The newly created RoomAmenities object representing the association.</returns>
        Task<RoomAmenities> AddAmenityToRoom(int roomId, int amenitiesID);
        /// <summary>
        /// Removes an amenity association from a room and deletes it from the data store.
        /// </summary>
        /// <param name="roomId">The ID of the room from which the amenity association will be removed.</param>
        /// <param name="amenitiesID">The ID of the amenity to remove from the room.</param>
        /// <returns>The removed RoomAmenities object representing the removed association.</returns>
        Task<RoomAmenities> RemoveAmentityFromRoom(int roomId, int amenitiesID);
    }
}
