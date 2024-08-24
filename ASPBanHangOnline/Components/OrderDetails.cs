using ASPBanHangOnline.Data;
using ASPBanHangOnline.Infrastructure;
using ASPBanHangOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPBanHangOnline.Components
{
    public class OrderDetails : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public OrderDetails(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            return View(cart);
        }
    }
}
