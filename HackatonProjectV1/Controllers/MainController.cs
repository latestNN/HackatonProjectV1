using HackatonProjectV1.Context;
using HackatonProjectV1.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonProjectV1.Controllers
{
    public class MainController : Controller
    {
        private readonly HtContext _context;

        public MainController(HtContext context)
        {
            _context = context;
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
    }
}
