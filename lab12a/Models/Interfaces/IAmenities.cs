using lab12a.Models;
using lab12a.Models.DTO;

namespace lab12a.Models.Interfaces
{
    public interface IAmenities
    {
        //Create
        Task<AmenitiesDto> CreateAmenities(AmenitiesDto amenDto);

        //Get All Amenities
        Task<List<AmenitiesDto>> GetAmenities();
        //Get Amenities By ID
        Task<AmenitiesDto> GetAmenityById(int amenID);

        //Update
        Task UpdateAmenities(AmenitiesDto amen);

        //Delete
        Task Delete(int id);
    }
}
