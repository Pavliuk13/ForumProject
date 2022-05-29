using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
        public IActionResult Index(string? keyWords)
        {
            if (keyWords != null)
                return SearchIndex(keyWords);
            
            return View(new HomeVM()
            {
                Posts = _context.Posts.Include(el => el.Category).Include(el => el.User),
                Categories = _context.Categories
            });
        }
        
        [ActionName("Index")]
        public IActionResult SearchIndex(string keyWords)
        {
            var words = keyWords.Split(' ');
            List<Post> posts = new List<Post>();
            foreach (var word in words)
            {
                var queryable = _context.Posts.Where(el => el.Theme.Contains(word)).Include(el => el.Category).Include(el => el.User);
                if (queryable.Any())
                {
                    posts.AddRange(queryable.Where(el => !posts.Contains(el)));
                }
            }
            
            return View(new HomeVM()
            {
                Posts = posts,
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
                Post = _context.Posts.Include(el => el.Category).Include(el => el.User).FirstOrDefault(el => el.Id == id),
                ExistInReadingBook = false,
                Likes = _context.Likes.Where(el => el.PostId == id).ToList(),
                Dislikes = _context.Dislikes.Where(el => el.PostId == id).ToList(),
                PostComments = _context.Comments.Where(el => el.PostId == id).Include(el => el.User).Include(el => el.Post).OrderByDescending(el => el.Date).ToList()
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

        [HttpPost]
        public IActionResult Comments(int postId, int? parentId,string text)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            Comment comment = new Comment()
            {
                Date = DateTime.Now,
                ParentId = parentId,
                Text = text,
                PostId = postId,
                UserId = _context.IdentityUsers.FirstOrDefault(u => u.Id == claim.Value).Id
            };
            
            _context.Comments.Add(comment);
            _context.SaveChanges();
            
            return RedirectToAction(nameof(Details), new {id = postId});
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
        
        [HttpGet]
        public IActionResult Like(int postId)
        {
            var post = _context.Posts.Find(postId);
            if (post != null)
            {
                var claimsIdentity = (ClaimsIdentity) User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var userId = _context.IdentityUsers.FirstOrDefault(u => u.Id == claim.Value).Id;

                var obj = _context.Likes.Where(el => el.PostId == postId && el.UserId == userId).ToList();
                if (obj.Count == 0)
                {
                    var objDislikes = _context.Dislikes.Where(el => el.PostId == postId && el.UserId == userId).ToList();
                    if (objDislikes.Any())
                    {
                        _context.Dislikes.Remove(objDislikes.FirstOrDefault());
                    }
                    _context.Likes.Add(new Like()
                    {
                        PostId = postId,
                        UserId = userId
                    });
                    _context.SaveChanges();
                }
                else
                {
                    _context.Likes.Remove(obj.FirstOrDefault());
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Details), new {id = postId});
            }

            return NotFound();
        }
        
        [HttpGet]
        public IActionResult Dislike(int postId)
        {
            var post = _context.Posts.Find(postId);
            if (post != null)
            {
                var claimsIdentity = (ClaimsIdentity) User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var userId = _context.IdentityUsers.FirstOrDefault(u => u.Id == claim.Value).Id;

                var obj = _context.Dislikes.Where(el => el.PostId == postId && el.UserId == userId).ToList();
                if (obj.Count == 0)
                {
                    var objLikes = _context.Likes.Where(el => el.PostId == postId && el.UserId == userId).ToList();
                    if (objLikes.Any())
                    {
                        _context.Likes.Remove(objLikes.FirstOrDefault());
                    }
                    _context.Dislikes.Add(new Dislike()
                    {
                        PostId = postId,
                        UserId = userId
                    });
                    _context.SaveChanges();
                }
                else
                {
                    _context.Dislikes.Remove(obj.FirstOrDefault());
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Details), new {id = postId});
            }

            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}