using ASPBanHangOnline.Data;
using Microsoft.AspNetCore.Mvc;

namespace ASPBanHangOnline.Components
{
    public class Navbar: ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Navbar(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories.ToList();
            return View("Index", categories);
        }
    }
}
