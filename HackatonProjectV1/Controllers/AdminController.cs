using HackatonProjectV1.Context;
using HackatonProjectV1.Entities;
using HackatonProjectV1.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HackatonProjectV1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly HtContext _context;

        public AdminController(UserManager<AppUser> userManager, HtContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ApproveUsers()
        {
            var universities = _context.universities.ToList();
            ViewBag.UniversityList = new SelectList(universities, "Id", "Name");

            var faculties = _context.faculties.ToList();
            ViewBag.FacultyList = new SelectList(faculties, "Id", "Name");
            
            var departments = _context.departments.ToList();
            ViewBag.DepartmentList = new SelectList(departments, "Id", "Name");
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
                return View("Index");
            }

            user.IsApproved = true;
            user.UniversityId = model.UniversityId;
            user.facultyId = model.facultyId;
            user.departmentId = model.departmentId;
            user.Class = model.Class;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("ApproveUsers");
        }
    }
}
