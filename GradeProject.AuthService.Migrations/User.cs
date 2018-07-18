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

        }

        [Key]
        //
        // Summary:
        //     Gets or sets the subject identifier.
        public Guid SubjectId { get; set; }
        //
        // Summary:
        //     Gets or sets the username.
        public string Username { get; set; }
        //
        // Summary:
        //     Gets or sets the password.
        public string Password { get; set; }
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
        //     Gets or sets if the user is active.
        public bool IsActive { get; set; }
        //
        // Summary:
        //     Gets or sets the claims.
        //public ICollection<Claim> Claims { get; set; }
    }
}
