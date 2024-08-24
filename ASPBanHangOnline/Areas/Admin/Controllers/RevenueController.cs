using ASPBanHangOnline.Data;
using Microsoft.AspNetCore.Mvc;

namespace ASPBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RevenueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RevenueController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Monthly()
        {
            int yearNow = DateTime.Now.Year;
            int monthNow = DateTime.Now.Month;

            

            int[] monthly = new int[monthNow];
            decimal[] totalPrice = new decimal[monthNow];

            for (int i = 0;i< monthNow; i++)
            {
                monthly[i] = i + 1;
                var orders = _context.Orders.Where(o => o.CreatedDate.Year == yearNow && o.CreatedDate.Month == i).ToList();
                var sum = orders.Sum(o => o.TotalPrice);
                totalPrice[i]=sum;
            }

            RevenueViewModel data = new RevenueViewModel()
            {
                label = monthly,
                price = totalPrice
            };

            return View(data);
        }
    }

    public class RevenueViewModel
    {
        public int[] label { get; set; }
        public decimal[] price { get; set; }
    }
}
