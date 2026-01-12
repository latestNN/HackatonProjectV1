using HackatonProjectV1.Context;
using HackatonProjectV1.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonProjectV1.ViewComponents
{
    public class DetailCommentViewComponent : ViewComponent
    {
        private readonly HtContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DetailCommentViewComponent(HtContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(int id , string ContentType)
        {
            if (ContentType == "content")
            {
                ViewBag.contentComments = _context.comments.Include(x => x.User).Where(x => x.contentId == id).ToList();
                ViewBag.commentType = ContentType;

            }
            else if (ContentType == "complaint")
            {
                var values = _context.comments.Include(x => x.User).Where(x => x.ComplaintId == id).ToList();
                if (values != null)
                {
                    ViewBag.complaintComments = values;
                    ViewBag.commentType = ContentType;
                }
                


            }

            

            return View();
        }
    }
}
