using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.MVCWeb.Infrastruct
{
    public static class API
    {
        public static class GamesService
        {
            public static string GetGames(string baseUri, int take = 5, int page = 1) => $"{baseUri}/games/?PageSize={take}&Page={page}";
        }                                                                                           
    }
}
