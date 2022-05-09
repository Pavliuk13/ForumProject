using System.Collections.Generic;
using System.Linq;
using ForumProject.Models.AppDBContext;
using Microsoft.AspNetCore.Mvc;
using ForumProject.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Controllers
{
    [Authorize]
    public class ReadListController : Controller
    {
        private readonly AppDBContext _context;
        
        public ReadListController(AppDBContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            List<ReadList> readList = new List<ReadList>();
            if (HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList) != null 
                && HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList).Any())
            {
                readList = HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList).ToList();
            }

            List<int> idList = readList.Select(el => el.PostId).ToList();
            List<Post> posts = _context.Posts.Where(item => idList.Contains(item.Id)).Include(el => el.Category).ToList();

            return View(posts);
        }
        
        public IActionResult Remove(int id)
        {
            List<ReadList> readList = new List<ReadList>();
            if (HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList) != null 
                && HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList).Any())
            {
                readList = HttpContext.Session.Get<IEnumerable<ReadList>>(WebConst.SessionReadList).ToList();
            }
            
            readList.Remove(readList.FirstOrDefault(item => item.PostId == id));
            HttpContext.Session.Set(WebConst.SessionReadList, readList);

            return RedirectToAction(nameof(Index));
        }
    }
}