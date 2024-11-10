using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class GameInDepthController : Controller
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _baseKey = "https://tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com/";
        private readonly string _endpoint = "getNFLBoxScore?";
        private readonly string _remainingParams = "playByPlay=true&fantasyPoints=true&twoPointConversions=2&passYards=.04&passAttempts=0&passTD=4&passCompletions=0&passInterceptions=-2&pointsPerReception=.5&carries=.2&rushYards=.1&rushTD=6&fumbles=-2&receivingYards=.1&receivingTD=6&targets=0&defTD=6&fgMade=3&fgMissed=-3&xpMade=1&xpMissed=-1";
        private readonly string _apiKey;

        public GameInDepthController(IConfiguration configuration)
    {
        _apiKey = configuration["APISettings:RapidAPIKey"];
    }

        // Add endpoint for team flags
        private readonly string _flagsEndpoint = "getNFLTeams?";
        private readonly string _flagParams = "sortBy=standings&rosters=false&schedules=false&topPerformers=false&teamStats=false&teamStatsSeason=2023";

        public async Task<IActionResult> GameInDetail(string gameId)
        {
            // Get game data with the passed gameId
            string apiUrl = $"{_baseKey}{_endpoint}gameID={gameId}&{_remainingParams}";
            var gameRequest = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            gameRequest.Headers.Add("x-rapidapi-host", "tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com");
            gameRequest.Headers.Add("x-rapidapi-key", _apiKey);

            // Get flags data
            string flagsUrl = $"{_baseKey}{_flagsEndpoint}{_flagParams}";
            var flagsRequest = new HttpRequestMessage(HttpMethod.Get, flagsUrl);
            flagsRequest.Headers.Add("x-rapidapi-host", "tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com");
            flagsRequest.Headers.Add("x-rapidapi-key", _apiKey);

            var gameResponse = await _client.SendAsync(gameRequest);
            var flagsResponse = await _client.SendAsync(flagsRequest);

            if (gameResponse.IsSuccessStatusCode && flagsResponse.IsSuccessStatusCode)
            {
                string gameContent = await gameResponse.Content.ReadAsStringAsync();
                string flagsContent = await flagsResponse.Content.ReadAsStringAsync();

                Root rootObject = JsonConvert.DeserializeObject<Root>(gameContent);
                FlagUrls flagUrls = JsonConvert.DeserializeObject<FlagUrls>(flagsContent);

                if (rootObject?.body != null)
                {
                    var gameData = new NFLGameData
                    {
                        gameStatus = rootObject.body.gameStatus,
                        teamStats = rootObject.body.teamStats,
                        gameDate = rootObject.body.gameDate,
                        teamIDHome = rootObject.body.teamIDHome,
                        homeResult = rootObject.body.homeResult,
                        away = rootObject.body.away,
                        lineScore = rootObject.body.lineScore,
                        home = rootObject.body.home,
                        homePts = rootObject.body.homePts,
                        awayResult = rootObject.body.awayResult,
                        teamIDAway = rootObject.body.teamIDAway,
                        awayPts = rootObject.body.awayPts,
                        gameID = rootObject.body.gameID
                    };

                    ViewBag.TeamFlags = flagUrls.body;
                    return View(new List<NFLGameData> { gameData });
                }
            }

            // Handle error case
            return View(new List<NFLGameData>());
        }
    }
}