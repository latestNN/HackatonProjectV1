using HackatonProjectV1.Context;
using HackatonProjectV1.Entities;
using HackatonProjectV1.Entities.MainPageElements;
using HackatonProjectV1.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HackatonProjectV1.Controllers
{
    public class MainController : Controller
    {
        private readonly HtContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MainController(HtContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UseFilter(LabelSelectionViewModel  model)
        {
            var selected = model.SelectedLabels;
            
            if (selected != null)
            {
                var label = selected.FirstOrDefault();
                return RedirectToAction("FiltredList", new { selectedLabels = label });
            }
            else
            {
                return View("Sıkıntı");
            }
            
        }

        public IActionResult FiltredList(Entities.MainPageElements.ContentLabel selectedLabels)
        {
            var values = _context.contents.Include(x => x.User).Where(x => x.Label == selectedLabels).ToList();
            return View(values);
        }

        public IActionResult ContentDetails(int id)
        {
            var values = _context.contents.Include(x => x.User).FirstOrDefault(x => x.Id == id);
            
            ViewBag.commentCount = _context.comments.Where(x => x.contentId == id).Count();
            TempData["ContentId"] = id;
            return View(values);
        }


        public IActionResult AddCommit(string comment)
        {
            var contenttId = Convert.ToInt32(TempData["ContentId"]);
            var userId = _userManager.GetUserId(User);
            _context.comments.Add(new Entities.MainPageElements.Comments
            {
                UserId = userId,
                Content = comment,
                contentId = contenttId,
                CreateTime = DateTime.Now
                
                
            });
            _context.SaveChanges();
            return RedirectToAction("ContentDetails", new { id = contenttId});
        }

        public IActionResult UnivercityList()
        {
            var values = _context.universities.ToList();
            return View(values);
        }

        public IActionResult UnivercityForum(int id)
        {
            var values = _context.universities.Find(id);
            return View(values);
        }

        public IActionResult AddContent()
        {
            ViewBag.Labels = Enum.GetValues(typeof(ContentLabel))
        .Cast<ContentLabel>()
        .Select(x => new SelectListItem
        {
            Text = x.ToString(),
            Value = ((int)x).ToString()
        }).ToList();
            var model = new CreateContent();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContent model, string contentLevel)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await _userManager.GetUserAsync(User);
            var entity = new Content
            {

                Title = model.Title,
                Description = model.Description,
                Label = model.Label,   // <— Enum seçimi buraya direkt gelir
                UserId = user.Id
            };

            if (contentLevel == "genel") 
            {
                var univercity = user.UniversityId;
                entity.UniversityId = univercity;
            }
            else if(contentLevel == "fakulte")
            {
                var univercity = user.UniversityId;
                var faculty = user.facultyId;
                entity.UniversityId = univercity;
                entity.FacultyId = faculty;
            }
            else if(contentLevel == "bolum")
            {
                var univercity = user.UniversityId;
                var faculty = user.facultyId;
                var depeartment = user.departmentId;
                entity.UniversityId = univercity;
                entity.FacultyId = faculty;
                entity.departmentId = depeartment;
            }


            _context.contents.Add(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.FullName = user.NameAndLastname;
            ViewBag.Email = user.Email;
            ViewBag.University = _context.universities.Find(user.UniversityId)?.Name;
            ViewBag.Faculty = _context.faculties.Find(user.facultyId)?.Name;
            ViewBag.Department = _context.departments.Find(user.departmentId)?.Name;
            ViewBag.AccountStatus = user.IsApproved;


            return View();
        }
    }
}
