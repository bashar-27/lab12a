using lab12a.Models.DTO;

namespace lab12a.Models.Interfaces
{
    public interface IRoom
    {
        //Create
        Task<RoomDto> CreateRoom(RoomDto room);

        //Get All Room
        Task<List<RoomDto>> GetRoomAsync();
        //Get Room By ID
        Task<RoomDto> GetRoomById(int roomId);

        //Update
        Task UpdateRoom(RoomDto room);

        //Delete
        Task Delete(int id);

        Task <RoomDto> AddAmenityToRoom(int roomId, int amenitiesID);
        Task<RoomAmenities> RemoveAmentityFromRoom(int roomId, int amenitiesID);
    }
}
