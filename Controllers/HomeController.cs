using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ForumProject.Models;
using ForumProject.Models.AppDBContext;
using ForumProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ForumProject.Utility;

namespace ForumProject.Controllers
{
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
        [Authorize]
        public IActionResult Details(int id)
        {
            DetailsVM detailsVm = new DetailsVM()
            {
                Post = _context.Posts.Include(el => el.Category).FirstOrDefault(el => el.Id == id),
                ExistInReadingBook = false
            };
            
            List<ReadList> readList = new List<ReadList>();
            if (HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList) != null && 
                HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList).Any())
            {
                readList = HttpContext.Session.Get<List<ReadList>>(WebConst.SessionReadList);
            }
            
            if (readList.Where(item => item.PostId == id).Any())
            {
                detailsVm.ExistInReadingBook = true;
            }
            
            if (detailsVm.Post == null)
                return NotFound("Post is not found. Probably, it deleted");

            return View(detailsVm);
        }
        
        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ReadList> readList = new List<ReadList>();
            if (HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList) != null && 
                HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList).Any())
            {
                readList = HttpContext.Session.Get<List<ReadList>>(WebConst.SessionReadList);
            }

            if (!readList.Where(item => item.PostId == id).Any())
            {
                readList.Add(new ReadList(){ PostId = id });
                HttpContext.Session.Set(WebConst.SessionReadList, readList);
            }

            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult RemoveFromCart(int id)
        {
            List<ReadList> readList = new List<ReadList>();
            if (HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList) != null && 
                HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList).Any())
            {
                readList = HttpContext.Session.Get<List<ReadList>>(WebConst.SessionReadList);
            }

            var obj = readList.FirstOrDefault(item => item.PostId == id);
            
            if (obj != null)
            {
                readList.Remove(obj);
                HttpContext.Session.Set(WebConst.SessionReadList, readList);
            }

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}