using GradeProject.ProfileService.Extensions;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Infrastructure
{
    public class DefaultAvatarsFactory
    {
        private readonly List<string> _maleAvatars;
        private readonly List<string> _femaleAvatars;

        public DefaultAvatarsFactory()
        {
            _maleAvatars = Directory.GetFiles("wwwroot/images/Avatars/Male", "*.svg")
                       .Select(x => Path.Combine("https://localhost:44383//Images//Avatars//Male", Path.GetFileName(x)))
                       .ToList();

            _femaleAvatars = Directory.GetFiles("wwwroot/images/Avatars/Female", "*.svg")
                       .Select(x => Path.Combine("https://localhost:44383//Images//Avatars//Female", Path.GetFileName(x)))
                       .ToList();
        }

        public string GetRandomAvatar(string gender) =>
            gender == "male" ? _maleAvatars.GetRandom() : _femaleAvatars.GetRandom();
    }
}
