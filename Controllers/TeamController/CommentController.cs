using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Project.Controllers
{
    public class CommentController : Controller
    {
        private static Dictionary<string, List<Comment>> gameComments = new Dictionary<string, List<Comment>>();

        public IActionResult AddComment(string gameId)
        {
            if (string.IsNullOrEmpty(gameId))
            {
                return RedirectToAction("GameOverview", "GameOverview");
            }
            ViewBag.GameId = gameId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitComment(string gameId, string userName, string userComment)
        {
            // Validate gameId
            if (string.IsNullOrEmpty(gameId))
            {
                return RedirectToAction("GameOverview", "GameOverview");
            }

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userComment))
            {
                var comment = new Comment 
                { 
                    UserName = userName,
                    UserComment = userComment
                };

                // Initialize list for game if it doesn't exist
                if (!gameComments.ContainsKey(gameId))
                {
                    gameComments[gameId] = new List<Comment>();
                }

                // Add comment to the specific game's list
                gameComments[gameId].Add(comment);

                // Store comments for this specific game in TempData
                TempData["Comments"] = JsonConvert.SerializeObject(gameComments[gameId]);

                // Redirect back to game details with the gameId
                return RedirectToAction("GameInDetail", "GameInDepth", new { gameId });
            }

            // If we get here, something went wrong. Return to form with the gameId
            ViewBag.GameId = gameId;
            return View("AddComment");
        }
    }
}