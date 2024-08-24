using ASPBanHangOnline.Areas.Admin.Controllers;
using ASPBanHangOnline.Data;
using ASPBanHangOnline.Infrastructure;
using ASPBanHangOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPBanHangOnline.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserOrder userOrder { get; set; } = new UserOrder();
        private DetailInOrder detailInOrder { get; set; } = new DetailInOrder();
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            int id = HttpContext.Session.GetJson<int>("UserId");
            var user = _context.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user != null)
            {
                userOrder.User = user;
            }
            else
            {
                return Redirect("Account/Login");
            }

            return View(userOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,UserId,Phone,Address")] Order order)
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            if (cart.CartLines.Count() > 0) {
                decimal shipping = 10;
                order.TotalPrice = cart.ComputeTotalPrice() + shipping;


                if (ModelState.IsValid)
                {
                    /*Console.WriteLine(order.UserId);
                    Console.WriteLine(order.Phone);
                    Console.WriteLine(order.Address);
                    Console.WriteLine(order.TotalPrice);*/

                    //Insert Order
                    _context.Add(order);
                    await _context.SaveChangesAsync();


                    // Insert OrderDetails
                    foreach (var item in cart.CartLines)
                    {
                        OrderDetail detail = new OrderDetail();
                        detail.ProductId = item.Product.ProductId;
                        detail.OrderId = order.OrderId;
                        detail.Quantity = item.Quantity;

                        _context.Add(detail);
                        await _context.SaveChangesAsync();
                    }

                    //Clear All Line In Cart
                    cart.ClearAll();
                    HttpContext.Session.SetJson("cart", cart);


                    int id = HttpContext.Session.GetJson<int>("UserId");
                    var user = _context.Users.Where(u => u.UserId == id).FirstOrDefault();
                    userOrder.User = user;

                    userOrder.Message = "";
                    userOrder.SuccessMessage = "Successful order!";

                    return View("Index", userOrder);
                }
                else
                {
                    int id = HttpContext.Session.GetJson<int>("UserId");
                    var user = _context.Users.Where(u => u.UserId == id).FirstOrDefault();
                    userOrder.User = user;

                    userOrder.SuccessMessage = "";
                    userOrder.Message = "Phone and Address can't be empty!";
                    return View("Index", userOrder);
                }
            }
            else
            {
                int id = HttpContext.Session.GetJson<int>("UserId");
                var user = _context.Users.Where(u => u.UserId == id).FirstOrDefault();

                userOrder.SuccessMessage = "";
                userOrder.Message = "Products in your cart is empty!";
                return View("Index", userOrder);
            }
        }
        public IActionResult PurchasedOrders()
        {
            int id = HttpContext.Session.GetJson<int>("UserId");
            //var user = _context.Users.Where(u => u.UserId == id).FirstOrDefault();
            if(id <= 0)
            {
                return Redirect("/Account/Login");
            }
            List<Order> orders = _context.Orders.Where(o=>o.UserId == id).OrderByDescending(o=>o.OrderId).ToList();
            if(orders.Count != 0)
            {
                return View(orders);
            }
            return NotFound();
        }

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }
            else
            {
                List<OrderDetail> details = _context.OrderDetails.Where(o => o.OrderId == order.OrderId).ToList();

                foreach (var detail in details)
                {
                    detail.Product = _context.Products.Where(o => o.ProductId == detail.ProductId).FirstOrDefault();
                }

                detailInOrder.Order = order;
                detailInOrder.OrderDetails = details;
            }

            return View(detailInOrder);

        }
    }
    public class UserOrder
    {
        public User User {set;get;} = new User();
        public Order Order {set;get;} = new Order();
        public string? Message {set;get;}
        public string? SuccessMessage { set;get;}
    }
}
