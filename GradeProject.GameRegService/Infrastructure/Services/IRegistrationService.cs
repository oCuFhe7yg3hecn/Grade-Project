using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Infrstructure
{
    public interface IRegistrationService
    {
        Task<bool> RegisterGame(string gameUrl);
    }
}
