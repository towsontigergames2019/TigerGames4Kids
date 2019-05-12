using System.Linq;
using System.Web.Mvc;
using MongoDB.Bson;
using TigerGames4Kids.App_Start;
using TigerGames4Kids.Models;
using System.Text;

namespace TigerGames4Kids.Controllers
{
    public class UsersController : Controller
    {

        MongoDriver _dbConnection;

        public UsersController()
        {
            _dbConnection = new MongoDriver();
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
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

            var hash = CreateMD5(user.Email);

            user.ProfileImageURI = "https://www.gravatar.com/avatar/" + hash + "?s=200?r=pg&d=identicon";

            user.Password = CreateMD5(user.Password);

            collection.InsertOne(user);
     
            return RedirectToAction("Home");
        }
    }
}

