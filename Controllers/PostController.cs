﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ForumProject.Models.AppDBContext;
using ForumProject.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Controllers
{
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
            IEnumerable<Post> posts = _db.Posts.ToList();
            foreach (var obj in posts)
                obj.Category = _db.Categories.FirstOrDefault(el => el.Id == obj.CategoryId);
            
            return View(posts);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            // IEnumerable<SelectListItem> listItems = _db.Categories.Select(item => new SelectListItem()
            // {
            //     Text = item.Name,
            //     Value = item.Id.ToString()
            // });
            //
            // ViewBag.listItems = listItems;
            //
            // Post post = new Post();

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
            return View();
        }
    }
}