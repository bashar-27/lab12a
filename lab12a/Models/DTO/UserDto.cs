namespace lab12a.Models.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string userName { get; set; }
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
    }
}
