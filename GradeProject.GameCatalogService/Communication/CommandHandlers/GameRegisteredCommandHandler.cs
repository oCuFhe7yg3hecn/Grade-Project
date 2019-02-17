using GradeProject.CommandBusInterfaces;
using GradeProject.GameCatalogService.Communication.Commands;
using GradeProject.GameCatalogService.Infrastructure.Services;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Communication.CommandHandlers
{
    public class GameRegisteredCommandHandler : ICommandHandler<RegisterGameCommand>
    {
        private readonly ICatalogService _catalogSvc;

        public GameRegisteredCommandHandler(ICatalogService catalogSvc)
        {
            _catalogSvc = catalogSvc;
        }

        public async Task ExecuteAsync(RegisterGameCommand command) => await Task.FromResult(0);
    }
}
