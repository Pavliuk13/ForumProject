using System.ComponentModel.DataAnnotations;

namespace ForumProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [Display(Name = "Confirm password*")]
        [Compare("Password", ErrorMessage = "Passwords didn't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}