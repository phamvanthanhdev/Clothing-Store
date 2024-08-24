using ASPBanHangOnline.Data;
using ASPBanHangOnline.Models;
using ASPBanHangOnline.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int pageSize = 9;
        public class Data
        {
            public Product product { get; set; }
            public List<Size> sizes { get; set; }
            public List<Color> colors { get; set; }

        }

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            Data data = new Data();
            data.product = product;
            data.sizes = await _context.Sizes.ToListAsync();
            data.colors = await _context.Colors.ToListAsync();
            if (product == null)
            {
                return NotFound();
            }

            return View(data);
        }

        /*public async Task<IActionResult> Shop()
        {
            var products = await _context.Products.OrderByDescending(p => p.ProductId).ToListAsync();

            return View(products);
        }*/

        public async Task<IActionResult> Shop(int page=1)
        {
            ProductsListViewModels data = new ProductsListViewModels();
            data.paging.pageCurrent = page;
            data.paging.pageSize = pageSize;
            data.paging.pageCount = _context.Products.Count();
            data.products = _context.Products.Skip((page-1)*pageSize).Take(pageSize).ToList();

            return View(data);
        }

        public async Task<IActionResult> ProductByCategory(int? categoryId)
        {
            var products = await _context.Products.Where(p=>p.CategoryId==categoryId)
                            .OrderByDescending(p => p.ProductId).ToListAsync();
            ProductsListViewModels data = new ProductsListViewModels();
            data.products = products;

            return View("Shop",data);
        }

        public async Task<IActionResult> Search(string keywords)
        {
            ProductsListViewModels data = new ProductsListViewModels();
            data.products = _context.Products.Where(p=>p.ProductName.Contains(keywords)).ToList();

            return View("Shop", data);
        }
    }
}
