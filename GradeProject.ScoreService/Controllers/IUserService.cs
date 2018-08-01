using System.Collections.Generic;
using System.Threading.Tasks;
using GradeProject.ScoreService.Models;

namespace GradeProject.ScoreService.Controllers
{
    public interface IUserService
    {
        Task<List<UserScores>> GetUsers();
    }
}