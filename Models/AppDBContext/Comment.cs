using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ForumProject.Models.AppDBContext
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Required]
        public string Text { get; set; }

        public int PostId { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }
        
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        
        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
        
        [ForeignKey("ParentId")]
        public virtual Comment Parent { get; set; }
        
        public virtual ICollection<Comment> Children { get; set; }
    }
}