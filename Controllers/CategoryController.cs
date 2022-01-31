using System.Linq;
using ForumProject.Models.AppDBContext;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDBContext _db;

        public CategoryController(AppDBContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View(_db.Categories.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj != null)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}