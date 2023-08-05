using lab12a.Models.DTO;

namespace lab12a.Models.Interfaces
{
   
    public interface IHotel
    {
        /// <summary>
        /// Creates a new hotel and adds it to the data store.
        /// </summary>
        /// <param name="hotel">The hotel to be created.</param>
        /// <returns>A DTO representation of the created hotel.</returns>
        Task<HotelDto> CreateHotel(Hotel hotel);

        /// <summary>
        /// Retrieves a list of all hotels along with their details.
        /// </summary>
        /// <returns>A list of DTO representations of hotels.</returns>
        Task<List<HotelDto>> GetHotelAsync();
        /// <summary>
        /// Retrieves a hotel by its ID along with its details.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to retrieve.</param>
        /// <returns>A DTO representation of the retrieved hotel.</returns>
        Task<HotelDto> GetHotelById(int hotelId);
        /// <summary>
        /// Updates the details of an existing hotel in the data store.
        /// </summary>
        /// <param name="hotel">The updated hotel details.</param>
        /// <param name="id">The ID of the hotel to update.</param>
        /// <returns>A DTO representation of the updated hotel.</returns>
        Task<HotelDto> UpdateHotel(Hotel hotel, int id);

        /// <summary>
        /// Deletes a hotel based on its ID from the data store.
        /// </summary>
        /// <param name="id">The ID of the hotel to delete.</param>
        /// <returns>A DTO representation of the deleted hotel.</returns>
        Task<HotelDto> Delete(int id);


    }
}
