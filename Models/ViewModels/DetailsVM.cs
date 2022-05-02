using ForumProject.Models.AppDBContext;

namespace ForumProject.Models.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            Post = new Post();
        }
        
        public Post Post { get; set; }

        public bool ExistInReadingBook { get; set; }
    }
}