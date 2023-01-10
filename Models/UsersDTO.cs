namespace Cube4solo.Models
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = "";
        public string Lastname { get; set; } = "";
        public string Email { get; set; } = "";

        public string Cellphone { get; set; } = "";
        public string LandlinePhone { get; set; } = "";
        public Services Services { get; set; }
        public Sites Sites { get; set; }
        public bool IsAdmin { get; set; }
    }
}