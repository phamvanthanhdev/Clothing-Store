using ASPBanHangOnline.Data;
using ASPBanHangOnline.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ASPBanHangOnline.Areas.Admin.Components
{
    public class AdminInfo : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AdminInfo(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            int AdminId = HttpContext.Session.GetJson<int>("AdminId");
            var admin = _context.Admins.Where(u => u.AdminId == AdminId).FirstOrDefault();
            if(admin == null)
            {
                admin = new ASPBanHangOnline.Models.Admin();
            }
            return View(admin);
        }
    }
}
