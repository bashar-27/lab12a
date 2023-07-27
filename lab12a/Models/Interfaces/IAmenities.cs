using lab12a.Models;

namespace lab12a.Models.Interfaces
{
    public interface IAmenities
    {
        //Create
        Task<Amenities> CreateAmenities(Amenities amen);

        //Get All Amenities
        Task<List<Amenities>> GetAmenities();
        //Get Amenities By ID
        Task<Amenities> GetAmenityById(int amenID);

        //Update
        Task<Amenities> UpdateAmenities(Amenities amen, int id);

        //Delete
        Task Delete(int id);
    }
}
