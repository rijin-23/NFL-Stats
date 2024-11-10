using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Project.Models;
using Newtonsoft.Json;

namespace Project.Controllers
{
    public class FlagsController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string BASE_KEY = "https://tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com/";
        private readonly string ENDPOINT = "getNFLTeams?";
        private readonly string sortBy = "standings";
        private readonly string rosters = "false";
        private readonly string topPerformers = "false";
        private readonly string schedules = "false";
        private readonly string teamStats = "false";
        private readonly string teamStatsSeason = "2023";
        private readonly string API_KEY = "26012f15bcmshe074a643cf402e1p1643d1jsn27194aa6ea0a";

        public async Task<IActionResult> TeamFlagView()
        {
            string url = $"{BASE_KEY}{ENDPOINT}sortBy={sortBy}&rosters={rosters}&schedules={schedules}&topPerformers={topPerformers}&teamStats={teamStats}&teamStatsSeason={teamStatsSeason}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("x-rapidapi-host", "tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com"); 
            request.Headers.Add("x-rapidapi-key", API_KEY);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                FlagUrls urls =  JsonConvert.DeserializeObject<FlagUrls>(content);
                List<TeamFlags> teamFlags = urls.body;
                return View(teamFlags);
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                return Content($"Request failed with status code: {response.StatusCode}");
            }
        }
    }
}
