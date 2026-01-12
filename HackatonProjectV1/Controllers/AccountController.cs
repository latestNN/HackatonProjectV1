using HackatonProjectV1.Entities;
using HackatonProjectV1.Services;
using HackatonProjectV1.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HackatonProjectV1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EDevletService _eDevletService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> singInManager, EDevletService eDevletService)
        {
            _userManager = userManager;
            _signInManager = singInManager;
            _eDevletService = eDevletService;
        }


        public IActionResult Index()
        {
            return View();
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
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,

                    
                    NameAndLastname = model.NameAndLastname,
                    TcNo = model.TcNo,
                    StudentBarcode = model.StudentBarcode,
                    IsApproved = false 
                };

                
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    
                    await _signInManager.SignInAsync(user, isPersistent: false);
                                        
                    return RedirectToAction("Index", "Main");
                }

                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            
            return View(model);
        }

        

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {

                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        
                        return RedirectToAction("Index", "Main");
                    }
                }

                
                ModelState.AddModelError("", "Email veya şifre hatalı.");
            }

            return View(model);
            
        }


    }
}   

