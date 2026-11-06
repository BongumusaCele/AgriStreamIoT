using AgriStreamIoT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgriStreamIoT.Controllers
{
    public class UserController : Controller
    {
        // Static list to act as a simple in-memory data store for the demo
        private static List<Users> _users = new List<Users>
        {
            new Users { UserId = "1", Username = "User123", Password = "1234Password", Role = "Farmer" },
            new Users { UserId = "2", Username = "User321", Password = "1234Password", Role = "Farmer" },
        };

        // GET: /Home/Index
        public IActionResult Index()
        {
            return View(_users);
        }

        // POST: /Home/Create
        [HttpPost]
        public IActionResult Create(Users newUser)
        {
            if (ModelState.IsValid)
            {
                newUser.UserId = _users.Any() ? _users.Max(i => i.UserId) + "1" : "1";
                _users.Add(newUser);
            }
            return RedirectToAction("Index");
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Users { UserId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
