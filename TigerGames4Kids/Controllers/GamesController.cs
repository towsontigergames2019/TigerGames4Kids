using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TigerGames4Kids.Models;


namespace TigerGames4Kids.Controllers
{
    public class GamesController : Controller
    {
        // GET: Games/Random
        public ActionResult Random()
        {
            var game = new GameType() { Title = "Super Mario" };
            return View(game);
        }
    }
}