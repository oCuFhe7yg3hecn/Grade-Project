using System.Collections.Generic;
using System.Threading.Tasks;
using GradeProject.ScoreService.Domain;

namespace GradeProject.ScoreService.Controllers
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
    }
}