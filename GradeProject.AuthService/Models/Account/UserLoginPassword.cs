using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Models.Account
{
    public class UserLoginPassword
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(7, ErrorMessage = "Password must be at least 7 to 15 symbols.")]
        [MaxLength(15, ErrorMessage = "Password must be at least 7 to 15 symbols.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
