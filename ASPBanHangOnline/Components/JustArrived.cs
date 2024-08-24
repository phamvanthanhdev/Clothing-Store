using ASPBanHangOnline.Data;
using Microsoft.AspNetCore.Mvc;

namespace ASPBanHangOnline.Components
{
    public class JustArrived : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public JustArrived(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var arrived = _context.Products.Where(p => p.IsArrived == true).ToList();
            return View(arrived);
        }
    }
}
