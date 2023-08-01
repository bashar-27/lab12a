namespace lab12a.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int? Layout { get; set; }

        public List<RoomAmenities>? roomAmen { get; set; }
        public IList<HotelRoom>? Rooms { get; set; }
    }

    }

