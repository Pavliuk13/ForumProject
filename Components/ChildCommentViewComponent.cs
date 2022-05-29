using System.Collections.Generic;
using System.Linq;
using ForumProject.Models.AppDBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Components
{
    public class ChildCommentViewComponent : ViewComponent
    {
        private readonly AppDBContext _context;

        public ChildCommentViewComponent(AppDBContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id, int postId, string userId)
        {
            KeyValuePair<(int, string), List<Comment>> pair = new KeyValuePair<(int, string), List<Comment>>((postId, userId), _context.Comments.Where(el => el.ParentId == id).Include(el => el.User).Include(el => el.Post).OrderByDescending(el => el.Date).ToList());
            return View(pair);
        }
    }
}