using AgriStreamIoT.Models;
using AgriStreamIoT.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgriStreamIoT.Controllers
{
    public class UserController : Controller
    {
        private readonly AgriStreamRepository _agriStreamRepository;

        // The Dependency Injection container automatically provides the concrete instance here
        public UserController(AgriStreamRepository agriStreamRepository)
        {
            _agriStreamRepository = agriStreamRepository;
        }

        // Static list to act as a simple in-memory data store for the demo
        private static List<Users> _users = new List<Users>
        {
            new Users { UserId = "1", Username = "User123", Password = "1234Password", Role = "Farmer" },
            new Users { UserId = "2", Username = "User321", Password = "1234Password", Role = "Farmer" },
        };

        // GET: /User/Index
        public IActionResult Index()
        {
            return View(_users);
        }

        // GET: /User/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        [HttpPost]
        public async Task<IActionResult> Create(Users newUser)
        {
            if (ModelState.IsValid)
            {
                // Fixed ID incrementation (assuming string IDs representing numbers)
                if (_users.Any())
                {
                    int maxId = _users.Max(i => int.Parse(i.UserId));
                    newUser.UserId = (maxId + 1).ToString();
                }
                else
                {
                    newUser.UserId = "1";
                }

                // Await the async save operation
                await _agriStreamRepository.SaveAsync(newUser);
                return RedirectToAction("Index");
            }

            // If validation fails, return to the view with the current data to show errors
            return View(newUser);
        }


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
