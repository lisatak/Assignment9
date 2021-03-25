using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment9.Models;
using Assignment9.Models.ViewModels;

namespace Assignment9.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //variables for repository and context to access database
        private IMoviesRepository _repository;
        private MoviesDbContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, IMoviesRepository repository, MoviesDbContext con)
        {
            _logger = logger;

            //receive database data
            _repository = repository;
            _context = con;
        }

        //Home page
        public IActionResult Index()
        {
            return View();
        }

        //Add Movie Page
        [HttpGet]
        public IActionResult MovieForm()
        {
            return View();
        }

        //Actually add the movie to the collection
        [HttpPost]
        public IActionResult MovieForm(Movies appResponse)
        {
            //check if data is valid, add movie to context, display confirmation page
            if (ModelState.IsValid)
            {
                _context.Movies.Add(appResponse);
                _context.SaveChanges();
                return View("Confirmation", appResponse);
            }
            //show same page again with validation comments if data is not valid
            return View();

        }

        //Run when edit button is clicked on collection page
        [HttpPost]
        public IActionResult EditMovie(int MovieId)
        {
            //pass data from the specific movie to form page
            var m = _repository.Movies
                .Where(m => m.MovieId == MovieId).FirstOrDefault();
            return View(m);
        }

        //Actually make changes for desired edits
        [HttpPost]
        public IActionResult SaveEdit(Movies appResponse)
        {
            //check if data is valid, delete old version and replace with new edited version
            if (ModelState.IsValid)
            {
                var movie = _repository.Movies
                    .Where(m => m.MovieId == appResponse.MovieId).FirstOrDefault();
                _context.Movies.Remove(movie);
                _context.Movies.Add(appResponse);
                _context.SaveChanges();
                //redirect to movie list
                return View("MovieCollection", _context.Movies);
            }
            //show same page again with validation comments if data is not valid
            return View("EditMovie", appResponse);
        }

        //Run when delete button is pressed on movie collection page
        [HttpPost]
        public IActionResult DeleteMovie(int movieId)
        {
            var movie = _repository.Movies
                    .Where(m => m.MovieId == movieId).FirstOrDefault();
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            //redirect to movie collection page (updated list)
            return View("MovieCollection", _context.Movies);
        }
        
        //display list of movies
        public IActionResult MovieCollection()
        {
            return View(_context.Movies);
        }

        //podcast page
        public IActionResult Podcast()
        {
            return View();
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
