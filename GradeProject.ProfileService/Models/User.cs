using GradeProject.ProfileService.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Models
{
    public enum UserStatus
    {
        Online,
        Busy,
        Away,
        Offline
    }

    [CollectionName("Users")]
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            ImageURL = "";
            Rank = "Newcommer";
            Status = UserStatus.Online;
            Gender = "male";

            FavouriteGenres = new List<string>();
            Games = new Dictionary<string, double>();
            Friends = new List<Guid>();

        }

        //TODO :Change Type
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        public string Gender { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public string Rank { get; set; }

        public string Slogan { get; set; }

        public string CoverImage { get; set; }

        [Required]
        public UserStatus Status { get; set; }
        public string CurrentAction { get; set; }
        public double TotalScore { get; set; }

        [MaxLength(5)]
        public List<string> FavouriteGenres { get; set; }
        public Dictionary<string, double> Games { get; set; }
        public List<Guid> Friends { get; set; }
    }
}
