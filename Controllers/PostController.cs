using System.Linq;
using ForumProject.Models.AppDBContext;
using ForumProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ForumProject.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDBContext _db;

        public PostController(AppDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var objList = _db.Posts;
            foreach (var obj in objList)
                obj.Category = _db.Categories.FirstOrDefault(el => el.Id == obj.Category.Id);
            
            return View(objList);
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
        public IActionResult Upsert()
        {
            return View();
        }
    }
}