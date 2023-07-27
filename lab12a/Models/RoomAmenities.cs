using System.ComponentModel.DataAnnotations.Schema;
namespace lab12a.Models
{

    public class RoomAmenities
    {
        public int AmenitiesID { get; set; }

        public int RoomID { get; set; }

        public Room room { get; set; }
        public Amenities amenities { get; set; }

    }
}
