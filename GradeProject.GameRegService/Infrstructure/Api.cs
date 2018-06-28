using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Infrstructure
{
    public static class Api
    {
        public static class GameCatalog
        {
            private static string _baseRoute;
            private static string _opts;

            public static void Configure(string opts) => _opts = opts;

            public static string AddGame() => $"{_baseRoute}/api/games";
        }
    }
}
