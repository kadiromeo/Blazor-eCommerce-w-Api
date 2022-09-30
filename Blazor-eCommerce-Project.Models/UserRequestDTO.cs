using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_eCommerce_Project.Models
{
    public class UserRequestDTO
    {
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and confirm password not matched")]
        public string ConfirmPassword { get; set; }
    }
}
