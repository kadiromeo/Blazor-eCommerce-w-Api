using System.ComponentModel.DataAnnotations;

namespace Blazor_eCommerce_Project.Models
{
    public class SingInDTO
    {
        [Required(ErrorMessage ="Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
    }
}
