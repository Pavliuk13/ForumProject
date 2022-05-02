﻿using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ForumProject.Models;
using ForumProject.Models.AppDBContext;
using ForumProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _context;
        
        public HomeController(ILogger<HomeController> logger, AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new HomeVM()
            {
                Posts = _context.Posts.Include(el => el.Category), 
                Categories = _context.Categories
            });
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            DetailsVM detailsVm = new DetailsVM()
            {
                Post = _context.Posts.Include(el => el.Category).FirstOrDefault(el => el.Id == id),
                ExistInReadingBook = false
            };
            if (detailsVm.Post == null)
                return NotFound("Post is not found. Probably, it deleted");

            return View(detailsVm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}