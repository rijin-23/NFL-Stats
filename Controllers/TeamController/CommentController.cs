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
            ViewBag.GameId = gameId;
            return View();
        }

        [HttpPost]
        public IActionResult SubmitComment(string gameId, string userName, string userComment)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userComment))
            {
                var comment = new Comment 
                { 
                    Id = Guid.NewGuid().ToString(), // Add unique ID for each comment
                    UserName = userName,
                    UserComment = userComment,
                    CreatedAt = DateTime.Now
                };

                if (!gameComments.ContainsKey(gameId))
                {
                    gameComments[gameId] = new List<Comment>();
                }

                gameComments[gameId].Add(comment);
                TempData["Comments"] = JsonConvert.SerializeObject(gameComments[gameId]);

                return RedirectToAction("GameInDetail", "GameInDepth", new { gameId });
            }

            ViewBag.GameId = gameId;
            return View("AddComment");
        }

        [HttpPost]
        public IActionResult DeleteComment(string commentId, string gameId)
        {
            if (gameComments.ContainsKey(gameId))
            {
                var comment = gameComments[gameId].FirstOrDefault(c => c.Id == commentId);
                if (comment != null)
                {
                    gameComments[gameId].Remove(comment);
                    TempData["Comments"] = JsonConvert.SerializeObject(gameComments[gameId]);
                }
            }

            return RedirectToAction("GameInDetail", "GameInDepth", new { gameId });
        }

        [HttpGet]
        public IActionResult EditComment(string commentId, string gameId)
        {
            if (gameComments.ContainsKey(gameId))
            {
                var comment = gameComments[gameId].FirstOrDefault(c => c.Id == commentId);
                if (comment != null)
                {
                    ViewBag.GameId = gameId;
                    return View(comment);
                }
            }

            return RedirectToAction("GameInDetail", "GameInDepth", new { gameId });
        }

        [HttpPost]
        public IActionResult UpdateComment(string commentId, string gameId, string userName, string userComment)
        {
            if (gameComments.ContainsKey(gameId))
            {
                var comment = gameComments[gameId].FirstOrDefault(c => c.Id == commentId);
                if (comment != null)
                {
                    comment.UserName = userName;
                    comment.UserComment = userComment;
                    TempData["Comments"] = JsonConvert.SerializeObject(gameComments[gameId]);
                }
            }

            return RedirectToAction("GameInDetail", "GameInDepth", new { gameId });
        }
    }
}