using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class GameOverviewController : Controller
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _baseKey = "https://tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com/";
        private readonly string _endpoint = "getNFLScoresOnly?";
        private readonly string _topPerformersParam = "true";
        private readonly string _apiKey;
        private readonly string _flagsEndpoint = "getNFLTeams?";
        private readonly string _flagParams = "sortBy=standings&rosters=false&schedules=false&topPerformers=false&teamStats=false&teamStatsSeason=2023";

        public GameOverviewController(IConfiguration configuration)
    {
        _apiKey = configuration["APISettings:RapidAPIKey"];
    }
    
        [HttpGet]
        public async Task<IActionResult> GameOverview([FromQuery] int gameWeek = 1, [FromQuery] int season = 2024)
        {
            // Debug line to check received parameters
            Console.WriteLine($"Received parameters - Week: {gameWeek}, Season: {season}");

            // Validate parameters
            if (gameWeek < 1 || gameWeek > 18)
            {
                gameWeek = 1; // Set to default if invalid
            }

            if (season < 2022)
            {
                season = 2024; // Set to default if invalid
            }

            try
            {
                // Get game scores with week and season parameters
                string scoresUrl = $"{_baseKey}{_endpoint}topPerformers={_topPerformersParam}&gameWeek={gameWeek}&season={season}";
                var scoresRequest = new HttpRequestMessage(HttpMethod.Get, scoresUrl);
                scoresRequest.Headers.Add("x-rapidapi-host", "tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com");
                scoresRequest.Headers.Add("x-rapidapi-key", _apiKey);

                // Get team flags
                string flagsUrl = $"{_baseKey}{_flagsEndpoint}{_flagParams}";
                var flagsRequest = new HttpRequestMessage(HttpMethod.Get, flagsUrl);
                flagsRequest.Headers.Add("x-rapidapi-host", "tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com");
                flagsRequest.Headers.Add("x-rapidapi-key", _apiKey);

                // Debug line to check API URL
                Console.WriteLine($"API URL: {scoresUrl}");

                var scoresResponse = await _client.SendAsync(scoresRequest);
                var flagsResponse = await _client.SendAsync(flagsRequest);

                if (scoresResponse.IsSuccessStatusCode && flagsResponse.IsSuccessStatusCode)
                {
                    string scoresContent = await scoresResponse.Content.ReadAsStringAsync();
                    string flagsContent = await flagsResponse.Content.ReadAsStringAsync();

                    // Debug line to check API response
                    Console.WriteLine($"API Response: {scoresContent}");

                    GameDetailsList urls = JsonConvert.DeserializeObject<GameDetailsList>(scoresContent);
                    FlagUrls flagUrls = JsonConvert.DeserializeObject<FlagUrls>(flagsContent);

                    List<GameDetails> games = urls.body?.Values.ToList() ?? new List<GameDetails>();
                    
                    // Store flags and current parameters in ViewBag
                    ViewBag.TeamFlags = flagUrls.body;
                    ViewBag.CurrentGameWeek = gameWeek;
                    ViewBag.CurrentSeason = season;

                    return View(games);
                }
                else
                {
                    // Debug line for API failure
                    Console.WriteLine($"API request failed. Status code: {scoresResponse.StatusCode}");
                    return View(new List<GameDetails>());
                }
            }
            catch (Exception ex)
            {
                // Debug line for exceptions
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return View(new List<GameDetails>());
            }
        }
    }
}