using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ForumProject.Models.AppDBContext
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Theme { get; set; }

        [Required]
        public string Description { get; set; }

        public string Image { get; set; }

        [Display(Name = "Category type")]
        public int CategoryId { get; set; }

        [Display(Name = "Author")]
        public string UserId { get; set; }

        public DateTime DateTime { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        
        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
    }
}