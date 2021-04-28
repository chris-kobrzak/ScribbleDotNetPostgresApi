using System.ComponentModel.DataAnnotations;

namespace Oss.Api
{
    public class LoginModel
    {
        [Required(ErrorMessage = "The email field is required")]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
