using System.Linq;
using System.Web.Mvc;
using MongoDB.Bson;
using TigerGames4Kids.App_Start;
using TigerGames4Kids.Models;
using System.Text;
using MongoDB.Driver;
using System;

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

        public static bool verifyMd5Hash(string input, string hash)
        {
            return true;
        }


        // GET: Users/Register
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

            user.Role = "User";

            collection.InsertOne(user);

            return RedirectToAction("Home");
        }

        // GET: Users/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Users/Token
        [HttpPost]
        public ActionResult Token(UserType user)
        {
            var collection = _dbConnection._database.GetCollection<UserType>("Users");

            var filter = new BsonDocument("Username", user.Username);

            var userInfo = collection.FindSync<UserType>(filter).ToList();

            Console.WriteLine(userInfo);

            if (verifyMd5Hash(user.Password, userInfo[0].Password))
            {
                Session["Username"] = userInfo[0].Username;
                Session["Name"] = userInfo[0].Name;
                Session["Email"] = userInfo[0].Email;
                Session["Age"] = userInfo[0].Age;
                Session["ProfileImageURI"] = userInfo[0].ProfileImageURI;
                Session["Role"] = userInfo[0].Role;
                return RedirectToAction("ViewUser");
            }
            else
            {
                return View("Login");
            }
        }

        // GET: Users/ViewUser
        public ActionResult ViewUser()
        {
            if (Session["Username"] != null)
            {
                var user = new UserType();
                user.Username = Session["Username"].ToString();
                user.Email = Session["Email"].ToString();
                user.Age = Int32.Parse(Session["Age"].ToString());
                user.ProfileImageURI = Session["ProfileImageURI"].ToString();
                user.Name = Session["Name"].ToString();
                return View(user);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: Users/Edit
        public ActionResult Edit()
        {
            if (Session["Username"] != null)
            {
                var user = new UserType();
                user.Username = Session["Username"].ToString();
                user.Email = Session["Email"].ToString();
                user.Age = Int32.Parse(Session["Age"].ToString());
                user.ProfileImageURI = Session["ProfileImageURI"].ToString();
                user.Name = Session["Name"].ToString();
                return View(user);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: Users/EditUser
        public ActionResult EditUser(UserType user)
        {
            var collection = _dbConnection._database.GetCollection<UserType>("Users");

            var filter = new BsonDocument("Username", user.Username);

            // collection.FindOneAndUpdate<UserType>();

            return RedirectToAction("ViewUser");
        }

        // GET: Users/Delete
        public ActionResult Delete()
        {
            if (Session["Username"] != null)
            {
                var user = new UserType();
                user.Username = Session["Username"].ToString();
                user.Email = Session["Email"].ToString();
                user.Age = Int32.Parse(Session["Age"].ToString());
                user.ProfileImageURI = Session["ProfileImageURI"].ToString();
                user.Name = Session["Name"].ToString();
                return View(user);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: Users/DeleteUser
        public ActionResult DeleteUser()
        {
            var collection = _dbConnection._database.GetCollection<UserType>("Users");

            // var filter = new BsonDocument("Username", user.Username);

            // collection.FindOneAndDelete<UserType>();

            return RedirectToAction("ViewUser");

        }
    }
}


