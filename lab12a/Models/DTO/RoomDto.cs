namespace lab12a.Models.DTO
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Layout { get; set; }
        public List<AmenitiesDto>? Amenities { get; set; }
    }
}
