using System.Linq;
using System.Web.Mvc;
using MongoDB.Bson;
using TigerGames4Kids.App_Start;
using TigerGames4Kids.Models;

namespace TigerGames4Kids.Controllers
{
    public class UsersController : Controller
    {

        MongoDriver _dbConnection;

        public UsersController()
        {
            _dbConnection = new MongoDriver();
        }

        public ActionResult Register()
        {
            return View();
        }


        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(UserType user)
        {
            var collection = _dbConnection._database.GetCollection<UserType>("Users");

            collection.InsertOne(user);
     
            return RedirectToAction("Index");
        }
    }
}

