using System.Collections.Generic;
using ForumProject.Models.AppDBContext;

namespace ForumProject.Models.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            Post = new Post();
            Likes = new List<Like>();
            Dislikes = new List<Dislike>();
        }
        
        public Post Post { get; set; }

        public List<Like> Likes { get; set; }
        
        public List<Dislike> Dislikes { get; set; }

        public bool ExistInReadingBook { get; set; }
    }
}