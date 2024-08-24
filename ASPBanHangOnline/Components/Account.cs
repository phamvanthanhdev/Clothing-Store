using ASPBanHangOnline.Data;
using ASPBanHangOnline.Infrastructure;
using Microsoft.AspNetCore.Mvc;
namespace ASPBanHangOnline.Components
{
    public class Account : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Account(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            int UserId = HttpContext.Session.GetJson<int>("UserId");
            var user = _context.Users.Where(u => u.UserId==UserId).FirstOrDefault();

            return View(user);
        }
    }
}
