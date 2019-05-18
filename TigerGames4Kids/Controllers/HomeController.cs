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

            return View();
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

    }
}