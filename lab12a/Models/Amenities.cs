namespace lab12a.Models
{
    public class Amenities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Amenities> amenities { get; set; }
    }
}
