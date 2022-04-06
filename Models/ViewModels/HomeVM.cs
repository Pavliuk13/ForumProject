using System.Collections.Generic;
using ForumProject.Models.AppDBContext;

namespace ForumProject.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Post> Posts { get; set; }
        
        public IEnumerable<Category> Categories { get; set; }
    }
}