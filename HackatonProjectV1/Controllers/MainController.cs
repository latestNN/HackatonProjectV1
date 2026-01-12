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
            var user = _userManager.GetUserAsync(User).Result;
            var univercityId = user.UniversityId;
            return RedirectToAction("UnivercityPage", new { Id = univercityId });
        }


        public IActionResult UnivercityPage(int Id)
        {
            
            var univercity = _context.universities.Where(x => x.Id == Id).FirstOrDefault();
            ViewBag.univercityId = Id;
            //ViewBag.univercityName = univercity.Name;
            //ViewBag.univercityCity = univercity.City;
            //ViewBag.userDepartmentId = user.departmentId;
            ViewBag.studentCount = _userManager.Users.Where(x => x.UniversityId == Id).Count();
            var departments = _context.departments.Where(x => x.UniversityId == Id).ToList();

            return View(departments);
        }

        public IActionResult Problems(int univercityId)
        {

            ViewBag.SelectedFilter = "all"; // Varsayılan olarak "Tümü" seçili gelsin
            TempData["UnivecityId"] = univercityId;
            
            
                var problems = _context.complaints.Include(z => z.User).Include(y => y.comments).Where(x => x.UniversityId == 18).ToList();
                return View(problems);
            
            
        }

        public IActionResult ProblemDetail(int Id)
        {
            
            var problem = _context.complaints.Include(z => z.User).Where(x => x.Id == Id).FirstOrDefault();
            if(problem != null)
            {
                TempData["ComplaintId"] = Id;
                ViewBag.commentCount = _context.comments.Where(x => x.ComplaintId == Id).Count();
                return View(problem);
            }
            else
            {
                return RedirectToAction("Problems");    
            }
                
        }

        [HttpPost]
        public IActionResult FilterComplaint(string filter)

        {
            int univercityId = Convert.ToInt32(TempData["UnivecityId"]);
            TempData.Keep("UnivecityId"); // TempData okunduktan sonra silinmemesi için Keep ediyoruz.

            List<Complaint> complaints = new List<Complaint>();
            
            if (filter == "all")
            {
                complaints = _context.complaints.Include(z => z.User).Include(y => y.comments).Where(x => x.UniversityId == univercityId).ToList();
                ViewBag.SelectedFilter = filter;
                return View("Problems", complaints);
            }
            else if (filter == "mostSupported")
            {
                complaints = _context.complaints.Include(z => z.User).Include(y => y.comments).Where(x => x.UniversityId == univercityId).OrderByDescending(x => x.Support).ToList();
                ViewBag.SelectedFilter = filter;
                return View("Problems", complaints);
            }
            else if (filter == "solved")
            {
                complaints = _context.complaints.Include(z => z.User).Include(y => y.comments).Where(x => x.UniversityId == univercityId).Where(x => x.Status == true).ToList();
                ViewBag.SelectedFilter = filter;
                return View("Problems", complaints);
            }
            else if (filter == "newest")
            {
                complaints = _context.complaints.Include(z => z.User).Include(y => y.comments).Where(x => x.UniversityId == univercityId).OrderByDescending(x => x.CreateTime).ToList();
                ViewBag.SelectedFilter = filter;
                return View("Problems", complaints);
            }

            // Eğer yukarıdaki if'lere girmezse (beklenmeyen durum) varsayılan döndür:
            ViewBag.SelectedFilter = filter;
            return View("Problems", complaints);
        }

        public IActionResult AddComplaintCommit(string comment)
        {
            int complaintId = Convert.ToInt32(TempData["ComplaintId"]);
            var userId = _userManager.GetUserId(User);
            _context.comments.Add(new Entities.MainPageElements.Comments
            {
                UserId = userId,
                Content = comment,
                ComplaintId = complaintId,
                CreateTime = DateTime.Now


            });
            _context.SaveChanges();
            return RedirectToAction("ProblemDetail", new { Id = complaintId });
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
