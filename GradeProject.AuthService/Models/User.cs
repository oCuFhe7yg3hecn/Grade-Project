using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Models
{
    public class User
    {
        public User()
        {
            SubjectId = Guid.NewGuid();
            RegisteredAt = DateTime.Now;
        }

        //
        // Summary:
        //     Gets or sets the subject identifier.
        [Key]
        public Guid SubjectId { get; set; }
        //
        // Summary:
        //     Gets or sets the username.
        [Required]
        public string Username { get; set; }
        //
        // Summary:
        //     Gets or sets the password.
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //
        // Summary :
        //     Gets or sets the provider name.
        //
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        //
        // Summary:
        //     Gets or sets if the Registration Date.
        [Required]
        public DateTime RegisteredAt { get; set; }
        //
        // Summary:
        //     Gets or sets if the user is active.
        [Required]
        public bool IsActive { get; set; }
        //
        // Summary:
        //     Gets or sets if the user is developer.
        [Required]
        public bool IsDeveloper { get; set; }

        public List<UserClient> Clients { get; set; }

        //
        // Summary:
        //     Gets or sets the provider name.
        //public string ProviderName { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the provider subject identifier.
        //public string ProviderSubjectId { get; set; }
        //
        // Summary:
        //     Gets or sets the claims.
        //public ICollection<Claim> Claims { get; set; }
    }
}
