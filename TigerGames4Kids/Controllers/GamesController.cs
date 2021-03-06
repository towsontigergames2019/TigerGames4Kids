﻿using System;
using System.Collections.Generic;
using System.Linq;
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

                var viewModel = new UserGameViewModel();
                viewModel.Games = games;
                viewModel.User = Session;
                return View(viewModel);
            }
            else
            {
                return Redirect("/Users/Login");
            }

        }

        // GET: Games/OurGames
        public ActionResult OurGames()
        {

            if (Session["Username"] != null)
            {

                return View();
            }
            else
            {
                return Redirect("/Users/Login");
            }

        }

        // GET: Games/Show/:title
        public ActionResult Show(string title)
        {
            if (Session["Username"] != null)
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
            else
            {
                return Redirect("/Users/Login");
            }
        }

        //show Anas games
        public ActionResult ShowAnasGame(string title)
        {
            if (Session["Username"] != null)
            {
                var username = Session["Username"];
                return View(username);
            }
            else
            {
                return Redirect("/Users/Login");
            }
        }

        //show Jens games
        public ActionResult ShowJensGame(string title)
        {
            if (Session["Username"] != null)
            {
                var username = Session["Username"];
                return View(username);
            }
            else
            {
                return Redirect("/Users/Login");
            }
        }

        //show MArias games
        public ActionResult ShowMariasGame(string title)
        {
            if (Session["Username"] != null)
            {
                var username = Session["Username"];
                return View(username);
            }
            else
            {
                return Redirect("/Users/Login");
            }
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
                    return View();
                }
            }
            else
            {
                return Redirect("/Games/AllGames");
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
                    return Redirect("/Games/AllGames");
                }
                else
                {   
                    return Redirect("/Games/AllGames");
                }
            }
            else
            {
                return Redirect("/Games/AllGames");
            }
        }

        // GET: Games/Edit/:title
        public ActionResult Edit(string title)
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    var collection = _dbConnection._database.GetCollection<GameType>("Games");
                    var filter = new BsonDocument("Title", title);
                    var games = collection.FindSync<GameType>(filter).ToList();
                    return View(games[0]);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return Redirect("/Games/AllGames");
            }
        }

        public ActionResult HighScores(String gameTitle)
        {
            var collection = _dbConnection._database.GetCollection<ScoreType>("Scores");
            var filter = new BsonDocument("GameTitle", gameTitle);
            List<ScoreType> scores = new List<ScoreType>();
            scores = collection.FindSync<ScoreType>(filter).ToList();
            List<ScoreType> orderedScores = scores.OrderByDescending(e => Int32.Parse(e.Score)).ToList();
            return View(orderedScores);
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
                    var filter = new BsonDocument("Title", game.Title);
                    collection.DeleteOne(filter);
                    collection.InsertOne(game);
                    return Redirect("/Games/AllGames");
                }
                else
                {
                    return Redirect("/Users/Login");
                }
            }
            else
            {
                return Redirect("/Users/Login");
            }
        }

        // GET: Games/Delete/:title
        public ActionResult Delete(string title)
        {
            if (Session["Username"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    var collection = _dbConnection._database.GetCollection<GameType>("Games");
                    var filter = new BsonDocument("Title", title);
                    var games = collection.FindSync<GameType>(filter).ToList();
                    return View(games[0]);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return Redirect("/Users/Login");
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
                    return Redirect("/Games/AllGames");
                }
                else
                {
                    return Redirect("/Users/Login");
                }
            }
            else
            {
                return Redirect("/Users/Login");
            }
        }
    }
}