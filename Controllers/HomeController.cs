using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DockerMVC.Models;
using DockerMVC.Data;

namespace DockerMVC.Controllers
{
public class HomeController : Controller

    {

        private readonly ILogger<HomeController> _logger;

        private IConfiguration _configuration;

        private BookDbContext _db;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, BookDbContext db)

        {

            _logger = logger;

            _configuration = configuration;

            _db = db;

        }

        public IActionResult Index()

        {

            var books = _db.Books.ToList();

            return View(books);

        }

        public IActionResult Create()

        {

            return View();

        }

        [HttpPost]

        public async Task<IActionResult> Create(Book book)

        {

            if (!ModelState.IsValid)

            {

                return View(book);

            }

            else

            {

                _db.Books.Add(book);

                await _db.SaveChangesAsync();

                return RedirectToAction("Index");

            }

        }

        public IActionResult Privacy()

        {

            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()

        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }

    }
}
