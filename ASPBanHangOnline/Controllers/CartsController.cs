using ASPBanHangOnline.Data;
using ASPBanHangOnline.Infrastructure;
using ASPBanHangOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPBanHangOnline.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public Cart? Cart { set; get; }

        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            return View(Cart);
        }

        public IActionResult Create(int? productId, int quantity=1)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart")??new Cart();
                Cart.AddItem(product, quantity);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ReduceQuantity(int? productId)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, -1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int? productId)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.RemoveItem(product);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
