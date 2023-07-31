namespace lab12a.Models.Interfaces
{
    public interface IHotelRoom
    {

        Task UpdateRoom(int hotelId, int roomNumber, HotelRoom hotelRoom);
        Task<HotelRoom> CreateHotelRoom(HotelRoom hotelRoom);
        Task DeleteHotelRoom(int hotelId, int roomNumber);
        Task<List<HotelRoom>> GetHotelRooms(int hotelId);
        Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber);
        // Task UpdateHotelRoom(HotelRoom dto);
    }
}
