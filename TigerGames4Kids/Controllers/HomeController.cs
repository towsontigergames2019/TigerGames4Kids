using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TigerGames4Kids.App_Start;
using TigerGames4Kids.Models;

namespace TigerGames4Kids.Controllers
{
    public class HomeController : Controller
    {
        MongoDriver _dbConnection;

        public HomeController()
        {
            _dbConnection = new MongoDriver();
        }


        public ActionResult Index()
        {
            var collection = _dbConnection._database.GetCollection<RecordType>("Records");
            var filter = new BsonDocument();
            var records = collection.FindSync<RecordType>(filter).ToList();

            var recordGroup = records.GroupBy(i => i.GameTitle);
            var enumerator = recordGroup.GetEnumerator();

            List<GameType> topGames = new GameType[] { }.ToList();
            
            
            for(int i=0; i< 3; i++)
            {
                var gameCollection = _dbConnection._database.GetCollection<GameType>("Games");
                var gameFilter = new BsonDocument("Title", recordGroup.ToArray()[i].Key);
                var games = gameCollection.FindSync<GameType>(gameFilter).ToList();

                topGames.Insert(i, games[0]);
            }

            return View(topGames);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Donation()
        {
            ViewBag.Message = "Your Donation Page.";

            return View();
        }

        //after donation, redirect
        public ActionResult Success()
        {
            ViewBag.Message = "Your Donation Was Successful!";
            return View();
        }

        public ActionResult Canceled()
        {
            ViewBag.Message = "Your Donation Failed";
            return View();
        }
    }
}