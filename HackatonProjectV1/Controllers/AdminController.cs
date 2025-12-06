using HackatonProjectV1.Entities;
using HackatonProjectV1.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonProjectV1.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ApproveUsers()
        {
            var users = await _userManager.Users.Where(x => x.IsApproved == false).ToListAsync();
            var vm = new UserUpdateViewModel
            {
                UnaprrovedUsers = users
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveStudent(UserUpdateViewModel model)
        {
            
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return View("Yarrak");
            }

            user.IsApproved = true;
            user.University = model.University;
            user.Faculty = model.Faculty;
            user.Department = model.Department;
            user.Class = model.Class;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("ApproveUsers");
        }
    }
}
