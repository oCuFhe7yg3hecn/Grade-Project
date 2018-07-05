using GradeProject.ProfileService.Models;
using GradeProject.ProfileService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Communication.Commands
{
    public class AddProfileCommand : ICommand
    {
        public UserInsertDTO UserInfo { get; set; }

        public AddProfileCommand(UserInsertDTO userInfo)
        {
            UserInfo = userInfo;
        }
    }
}
