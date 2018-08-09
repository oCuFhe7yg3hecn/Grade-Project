using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Models.Account.Register
{
    public class PlayerRegisteModel
    {
        public PlayerRegisteModel()
        {

        }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MinLength(5, ErrorMessage = "User name must be at least 5 to 25 symbols.")]
        [MaxLength(25, ErrorMessage = "User name must be at least 5 to 25 symbols.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(7, ErrorMessage = "Password must be at least 7 to 15 symbols.")]
        [MaxLength(15, ErrorMessage = "Password must be at least 7 to 15 symbols.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required]
        public bool IsDeveloper { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        public string Gender { get; set; }

        [Required]
        public IFormFile AvatarImage { get; set; }

        public IFormFile CoverImage { get; set; }

        public string Slogan { get; set; }
    }
}
