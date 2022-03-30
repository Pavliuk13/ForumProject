using System.Collections.Generic;
using ForumProject.Models.AppDBContext;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ForumProject.Models.ViewModels
{
    public class PostVM
    {
        public Post Post { get; set; }
        
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    }
}