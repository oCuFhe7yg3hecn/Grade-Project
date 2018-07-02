using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GradeProject.MVCWeb.Infrastruct
{
    public class WebServicesCallService
    {
        private readonly HttpClient _httpClient;

        public WebServicesCallService()
        {
            _httpClient = new HttpClient();
        }

        public async List<Games> GetTopGames(int number)
        {
            var requestUri = API.GamesService.GetGames("", number);
            return await _httpClient.GetStringAsync(requestUri)
        }
    }
}
