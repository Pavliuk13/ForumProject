using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ForumProject.Models.AppDBContext
{
    public class Dislike
    {
        public int PostId { get; set; }

        public string UserId { get; set; }
        
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        
        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
    }
}