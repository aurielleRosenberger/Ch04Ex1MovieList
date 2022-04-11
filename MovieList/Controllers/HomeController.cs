using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieList.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieList.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext context { get; set; }           //New
        public HomeController(MovieContext ctx)         //New
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var movies = context.Movies.Include(m => m.Genre).OrderBy(m => m.Name).ToList();    //New var to hold movies
            return View(movies);                                          //Sends movies object to view
        }

    }
}
