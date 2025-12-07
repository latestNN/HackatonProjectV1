using HackatonProjectV1.Context;
using HackatonProjectV1.Entities.MainPageElements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace HackatonProjectV1.ViewComponents
{
    public class FilterViewComponent : ViewComponent
    {
        private readonly HtContext _context;

        public FilterViewComponent(HtContext context)
        {
            _context = context;
        }

        public  IViewComponentResult Invoke()
        {
            var labels = Enum.GetValues(typeof(ContentLabel))
                     .Cast<ContentLabel>()
                     .ToList();

            ViewBag.Labels = labels;

            return View(new HackatonProjectV1.ViewModel.LabelSelectionViewModel());
        }
    }
}
