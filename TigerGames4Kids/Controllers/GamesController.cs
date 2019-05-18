using System;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TigerGames4Kids.App_Start;
using TigerGames4Kids.Models;


namespace TigerGames4Kids.Controllers
{
    public class GamesController : Controller
    {

        MongoDriver _dbConnection;

        public GamesController()
        {
            _dbConnection = new MongoDriver();
        }

        // GET: Games/AllGames
        public ActionResult AllGames()
        {

            if (Session["Username"] != null)
            {
                var collection = _dbConnection._database.GetCollection<GameType>("Games");

                var filter = new BsonDocument();

                var games = collection.FindSync<GameType>(filter).ToList();
                return View(games);
            }
            else
            {
                return Redirect("/Users/Login");
            }

        }

        // GET: Games/Show/:title
        public ActionResult Show(string title)
        {
        
            var collection = _dbConnection._database.GetCollection<GameType>("Games");

            var filter = new BsonDocument("Title", title);

            var games = collection.FindSync<GameType>(filter).ToList();

            var record = new RecordType();
            record.GameTitle = games[0].Title;
            record.UserId = (MongoDB.Bson.ObjectId)Session["Id"];
            record.Timestamp = DateTime.Now;
            var recordsCollection = _dbConnection._database.GetCollection<RecordType>("Records");
            recordsCollection.InsertOne(record);

            return View(games[0]);
        }

        // GET: Games/Add
        public ActionResult Add()
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                { 
                    return View();
                }
                else
                {
                    return Redirect("/Users/ViewUser");
                }
            }
            else
            {
                return Redirect("/Users/Login");
            }
        }

        // POST: Games/AddGame
        [HttpPost]
        public ActionResult AddGame(GameType game)
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    var collection = _dbConnection._database.GetCollection<GameType>("Games");
                    collection.InsertOne(game);
                    return Redirect("/Users/ViewUser");
                }
                else
                {   
                    return Redirect("/Users/ViewUser");
                }
            }
            else
            {
                return Redirect("/Users/Login");
            }
        }

        // GET: Games/Edit/:title
        public ActionResult Edit(string title)
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    var collection = _dbConnection._database.GetCollection<UserType>("Games");
                    var filter = new BsonDocument("Title", title);
                    var games = collection.FindSync<UserType>(filter).ToList();
                    return View(games[0]);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: Games/EditGame
        [HttpPost]
        public ActionResult EditGame(GameType game)
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    var collection = _dbConnection._database.GetCollection<GameType>("Games");
                    collection.InsertOne(game);
                    return RedirectToAction("ViewUser");
                }
                else
                {
                    return RedirectToAction("ViewUser");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: Games/Delete/:title
        public ActionResult Delete(string title)
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    var collection = _dbConnection._database.GetCollection<UserType>("Games");
                    var filter = new BsonDocument("Title", title);
                    var games = collection.FindSync<UserType>(filter).ToList();
                    return View(games[0]);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("AllGames");
            }
        }

        // POST: Games/DeleteGame
        [HttpPost]
        public ActionResult DeleteGame(GameType game)
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    var collection = _dbConnection._database.GetCollection<GameType>("Games");
                    var filter = new BsonDocument("Title", game.Title);
                    collection.DeleteOne(filter);
                    return RedirectToAction("ViewUser");
                }
                else
                {
                    return RedirectToAction("ViewUser");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}