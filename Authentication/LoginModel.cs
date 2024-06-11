using System.ComponentModel.DataAnnotations;

namespace MovieMania.Authentication
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
