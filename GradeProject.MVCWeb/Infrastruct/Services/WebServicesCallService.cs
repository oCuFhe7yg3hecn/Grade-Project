using GradeProject.MVCWeb.Models;
using GradeProject.MVCWeb.Models.Services;
using Newtonsoft.Json;
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

        public async Task<List<Games.GameDTO>> GetTopGames(int number)
        {
            var requestUri = API.GamesService.GetGames("http://localhost:54554", number);
            var response = await _httpClient.GetStringAsync(requestUri);
            var oData = JsonConvert.DeserializeObject<ODataResponse<Games.GameDTO>>(response);
            return oData.Value.ToList();
        }
    }
}
