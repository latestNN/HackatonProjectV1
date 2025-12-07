using HackatonProjectV1.Context;
using HackatonProjectV1.Entities;
using HackatonProjectV1.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    }
}
