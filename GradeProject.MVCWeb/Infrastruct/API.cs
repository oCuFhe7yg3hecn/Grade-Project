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
            public static string GetGames(string baseUri, int top = 5) => $"{baseUri}/odata/Games?$top={top}";
        }                                                                                           
    }
}
