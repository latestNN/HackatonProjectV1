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
            user.TcNo = "Deleted";

            await _userManager.UpdateAsync(user);

            return RedirectToAction("ApproveUsers");
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users
                .Include(u => u.university)
                .Include(u => u.faculty)
                .Include(u => u.department)
                .ToListAsync();

            var userViewModels = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                NameAndLastname = u.NameAndLastname,
                TcNo = u.TcNo,
                StudentBarcode = u.StudentBarcode,
                Class = u.Class,
                IsApproved = u.IsApproved,
                UniversityId = u.UniversityId,
                FacultyId = u.facultyId,
                DepartmentId = u.departmentId,
                university = u.university,
                faculty = u.faculty,
                department = u.department
            }).ToList();

            return View(userViewModels);
        }

        public IActionResult CreateUser()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    NameAndLastname = model.NameAndLastname,
                    TcNo = model.TcNo,
                    StudentBarcode = model.StudentBarcode,
                    Class = model.Class,
                    IsApproved = model.IsApproved,
                    UniversityId = model.UniversityId,
                    facultyId = model.FacultyId,
                    departmentId = model.DepartmentId
                };

                var result = await _userManager.CreateAsync(user, model.Password ?? "Password123!"); // Default password if null
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Users");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            LoadDropdowns();
            return View(model);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                NameAndLastname = user.NameAndLastname,
                TcNo = user.TcNo,
                StudentBarcode = user.StudentBarcode,
                Class = user.Class,
                IsApproved = user.IsApproved,
                UniversityId = user.UniversityId,
                FacultyId = user.facultyId,
                DepartmentId = user.departmentId
            };
            LoadDropdowns();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.NameAndLastname = model.NameAndLastname;
                user.TcNo = model.TcNo;
                user.StudentBarcode = model.StudentBarcode;
                user.Class = model.Class;
                user.IsApproved = model.IsApproved;
                user.UniversityId = model.UniversityId;
                user.facultyId = model.FacultyId;
                user.departmentId = model.DepartmentId;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                     // Password update if provided
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.Password);
                         if (!passwordResult.Succeeded)
                        {
                             foreach (var error in passwordResult.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                             LoadDropdowns();
                             return View(model);
                        }
                    }
                    return RedirectToAction("Users");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            LoadDropdowns();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Users");
        }

        private void LoadDropdowns()
        {
            ViewBag.UniversityList = new SelectList(_context.universities.ToList(), "Id", "Name");
            ViewBag.FacultyList = new SelectList(_context.faculties.ToList(), "Id", "Name");
            ViewBag.DepartmentList = new SelectList(_context.departments.ToList(), "Id", "Name");
        }
    }
}
