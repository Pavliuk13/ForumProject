using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using ForumProject.Models.AppDBContext;
using ForumProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class PostController : Controller
    {
        private readonly AppDBContext _db;
        private readonly IWebHostEnvironment _environment;

        public PostController(AppDBContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Post> posts = _db.Posts.Include(el => el.Category);
            return View(posts);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            PostVM postVm = new PostVM()
            {
                Post = new Post(),
                CategorySelectList = _db.Categories.Select(item => new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                })
            };
            
            if (id == null)
            {
                //create post
                return View(postVm);
            }

            postVm.Post = _db.Posts.Find(id);
            if (postVm.Post == null)
                return NotFound();

            return View(postVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PostVM postVm)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                string upload, fileName, extention;
                
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _environment.WebRootPath;

                if (postVm.Post.Id == 0)
                {
                    //create new post
                    upload = webRootPath + WebConst.ImagePath;
                    fileName = Guid.NewGuid().ToString();
                    extention = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    postVm.Post.Image = fileName + extention;
                    postVm.Post.DateTime = DateTime.Now;
                    postVm.Post.UserId = _db.IdentityUsers.FirstOrDefault(u => u.Id == claim.Value).Id;

                    _db.Posts.Add(postVm.Post);
                }
                else
                {
                    //update post
                    var objFromDb = _db.Posts.AsNoTracking().FirstOrDefault(el => el.Id == postVm.Post.Id);
                    if (files.Count > 0)
                    {
                        upload = webRootPath + WebConst.ImagePath;
                        fileName = Guid.NewGuid().ToString();
                        extention = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream =
                               new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        postVm.Post.Image = fileName + extention;
                    }
                    else
                        postVm.Post.Image = objFromDb.Image;
                    
                    _db.Posts.Update(postVm.Post);
                }
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            postVm.CategorySelectList = _db.Categories.Select(item => new SelectListItem()
            {
                Text = item.Name,
                Value = item.Id.ToString()
            });
            
            return View(postVm);
        }
        
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            
            var obj = _db.Posts.Find(id);
            if (obj == null)
                return NotFound();

            obj.Category = _db.Categories.FirstOrDefault(el => el.Id == obj.CategoryId);

            return View(obj);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var objFromDb = _db.Posts.Find(id);
            string path = _environment.WebRootPath + WebConst.ImagePath;

            var oldFile = Path.Combine(path, objFromDb.Image);
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Posts.Remove(objFromDb);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}