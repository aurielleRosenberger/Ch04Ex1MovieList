using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieList.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieList.Controllers
{
    public class MovieController : Controller
    {
        private MovieContext context { get; set; }
        public MovieController(MovieContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Genres = context.Genres.OrderBy(g => g.Name).ToList();
            return View("Edit", new Movie()); // Send empty object
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Genres = context.Genres.OrderBy(g => g.Name).ToList();
            var movie = context.Movies.Find(id); // Read data for the id
            return View("Edit", movie); // Send movie object to view
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (movie.MovieId == 0) // Add the movie
                {
                    context.Movies.Add(movie);
                }
                else // Update the movie
                {
                    context.Movies.Update(movie);
                }
                context.SaveChanges(); // Saves to database
                return RedirectToAction("Index", "Home"); // Reloads data
            }
            else
            {
                ViewBag.Action = (movie.MovieId == 0) ? "Add" : "Edit";
                ViewBag.Genres = context.Genres.OrderBy(g => g.Name).ToList(); // Get genre data
                return View(movie); // Returns movie object to view
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = context.Movies.Find(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            context.Movies.Remove(movie);
            context.SaveChanges(); // Saves to database
            return RedirectToAction("Index", "Home"); // Reloads data
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
