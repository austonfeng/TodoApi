using System.ComponentModel.DataAnnotations;

namespace DemoProject.Service.Models
{
    public class ValidateUserReq
    {
        [Required(ErrorMessage = "User Name is required")]
        [MaxLength(50)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(50)]
        public string? Password { get; set; }
    }
}
