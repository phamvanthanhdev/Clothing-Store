using ASPBanHangOnline.Data;
using Microsoft.AspNetCore.Mvc;
namespace ASPBanHangOnline.Components
{
    public class Categories : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Categories(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories.OrderByDescending(c=>c.CategoryId).Take(6).ToList();
            return View(categories);
        }
    }
}
