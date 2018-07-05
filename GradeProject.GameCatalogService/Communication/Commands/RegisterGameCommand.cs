using GradeProject.GameCatalogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Communication.Commands
{
    public class RegisterGameCommand : ICommand
    {
        public GameInfo GameInfo { get; set; }

        public RegisterGameCommand(GameInfo gameInfo)
        {
            GameInfo = gameInfo;
        }
    }
}
