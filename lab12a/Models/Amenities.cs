namespace lab12a.Models
{
    public class Amenities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Amenities> amenities { get; set; }
    }
}
