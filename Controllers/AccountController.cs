using System.Linq;
using System.Threading.Tasks;
using ForumProject.Models.AppDBContext;
using ForumProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        
        public AccountController(AppDBContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var registeredUser = await _userManager.FindByEmailAsync(user.Email);
                    if (!_context.Users.Any())
                    {
                        await _userManager.AddToRoleAsync(registeredUser, "SuperAdmin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(registeredUser, "User");
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                ModelState.AddModelError(string.Empty, "Invalid registration attempt");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }

            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}