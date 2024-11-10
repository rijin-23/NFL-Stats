using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers{
    public class TeamNameController : Controller
    {
        public IActionResult TeamDetails()
        {
            var name = new TeamName();
            name.teamName = "Tampa Bay Buccaneers";
            return View(name);
        }
    }
}