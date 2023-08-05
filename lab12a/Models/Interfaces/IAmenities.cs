using lab12a.Models;
using lab12a.Models.DTO;

namespace lab12a.Models.Interfaces
{
    public interface IAmenities
    {
        /// <summary>
        /// Creates a new amenity and adds it to the data store.
        /// </summary>
        /// <param name="amenDto">The DTO containing the details of the amenity to be created.</param>
        /// <returns>A DTO representation of the created amenity.</returns>
        Task<AmenitiesDto> CreateAmenities(AmenitiesDto amenDto);

        /// <summary>
        /// Retrieves a list of all amenities along with their details.
        /// </summary>
        /// <returns>A list of DTO representations of amenities.</returns>
        Task<List<AmenitiesDto>> GetAmenities();
        /// <summary>
        /// Retrieves an amenity by its ID along with its details.
        /// </summary>
        /// <param name="amenID">The ID of the amenity to retrieve.</param>
        /// <returns>A DTO representation of the retrieved amenity.</returns>
        Task<AmenitiesDto> GetAmenityById(int amenID);

        /// <summary>
        /// Updates the details of an existing amenity in the data store.
        /// </summary>
        /// <param name="amen">The DTO containing the updated details of the amenity.</param>
        Task UpdateAmenities(AmenitiesDto amen);

        /// <summary>
        /// Deletes an amenity based on its ID from the data store.
        /// </summary>
        /// <param name="id">The ID of the amenity to delete.</param>
        Task Delete(int id);
    }
}
