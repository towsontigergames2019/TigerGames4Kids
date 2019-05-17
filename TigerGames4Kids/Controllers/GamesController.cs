﻿using System.Web.Mvc;
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
            var collection = _dbConnection._database.GetCollection<GameType>("Games");

            var filter = new BsonDocument();

            var games = collection.FindSync<GameType>(filter).ToList();
            return View(games);
        }

        // GET: Games/Show/:title
        public ActionResult Show(string title)
        {

            var collection = _dbConnection._database.GetCollection<GameType>("Games");

            var filter = new BsonDocument("Title", title);

            var games = collection.FindSync<GameType>(filter).ToList();
            return View(games[0]);
        }

        // GET: Games/Add
        public ActionResult Add()
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"] == "Admin")
                { 
                    return View();
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

        // POST: Games/AddGame
        public ActionResult AddGame(GameType game)
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"] == "Admin")
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

        // GET: Games/Edit/:title
        public ActionResult Edit(string title)
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"] == "Admin")
                {
                    var collection = _dbConnection._database.GetCollection<UserType>("Games");
                    var filter = new BsonDocument("Title", title);
                    var games = collection.FindSync<UserType>(filter).ToList();
                    return View(games[0]);
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

        // POST: Games/EditGame
        public ActionResult EditGame(GameType game)
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"] == "Admin")
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
                if (Session["Role"] == "Admin")
                {
                    var collection = _dbConnection._database.GetCollection<UserType>("Games");
                    var filter = new BsonDocument("Title", title);
                    var games = collection.FindSync<UserType>(filter).ToList();
                    return View(games[0]);
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

        // POST: Games/DeleteGame
        public ActionResult DeleteGame(GameType game)
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"] == "Admin")
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