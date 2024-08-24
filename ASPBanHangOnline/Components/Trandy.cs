using ASPBanHangOnline.Data;
using Microsoft.AspNetCore.Mvc;

namespace ASPBanHangOnline.Components
{
    public class Trandy : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Trandy(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var trandys = _context.Products.Where(p => p.IsTrandy == true).ToList();
            return View(trandys);
        }
    }
}
