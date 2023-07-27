namespace lab12a.Models.Interfaces
{
   
    public interface IHotel
    {
        //Create
        Task<Hotel> CreateHotel(Hotel hotel);

        //Get All Hotel
        Task<List<Hotel>> GetHotelAsync();
        //Get Hotel By ID
        Task<Hotel> GetHotelById(int hotelId);

        //Update
        Task<Hotel> UpdateHotel(Hotel hotel, int id);

        //Delete
        Task<Hotel> Delete(int id);


    }
}
