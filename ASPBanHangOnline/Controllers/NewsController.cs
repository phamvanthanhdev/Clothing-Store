using ASPBanHangOnline.Data;
using ASPBanHangOnline.Infrastructure;
using ASPBanHangOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPBanHangOnline.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private NewsViewModel newsViewModel { get; set; } = new NewsViewModel();
        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.News.Include(n => n.Admin);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? NewsId)
        {
            int UserId = HttpContext.Session.GetJson<int>("UserId");
            if (UserId <= 0) return Redirect("/Account/Login");
            newsViewModel.UserId = UserId;
            News news = _context.News.Include(n => n.Admin).Where(n=>n.NewsId == NewsId).FirstOrDefault();
            if(news == null) return NotFound();
            newsViewModel.News = news;
            newsViewModel.Likes = _context.NewsLikes.Include(n => n.User).Include(n=>n.News).Where(n => n.NewsId == NewsId).ToList();
            newsViewModel.Comments = _context.NewsComments.Include(n => n.User).Include(n => n.News).Where(n => n.NewsId == NewsId).ToList();

            Console.WriteLine("/////");
            Console.WriteLine(newsViewModel.UserId);
            Console.WriteLine(newsViewModel.News.NewsId);
            
            Console.WriteLine("/////");

            return View(newsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComments(int UserId, int NewsId, string Comments)
        {
            Console.WriteLine(UserId);
            Console.WriteLine(NewsId);
            Console.WriteLine(Comments);
            if (ModelState.IsValid)
            {
                NewsComment comments = new NewsComment
                {
                    UserId = UserId,
                    NewsId = NewsId,
                    Comments = Comments
                };
                _context.Add(comments);
                await _context.SaveChangesAsync();
                return Redirect("/News/Details/?NewsId=" + NewsId);
            }
            return NotFound();
        }

        public async Task<IActionResult> Like(int NewsId)
        {
            int UserId = HttpContext.Session.GetJson<int>("UserId");
            if (UserId <= 0) return Redirect("/Account/Login");

            NewsLike newsLike = _context.NewsLikes.Include(n => n.User).Include(n => n.News).Where(n => n.UserId == UserId).FirstOrDefault();
            if (newsLike == null)
            {
                NewsLike like = new NewsLike
                {
                    UserId = UserId,
                    NewsId = NewsId
                };
                _context.Add(like);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Remove(newsLike);
                await _context.SaveChangesAsync();
            }

            return Redirect("/News/Details/?NewsId=" + NewsId);
        }

        public async Task<IActionResult> RemoveComment(int CommentId)
        {

            NewsComment newsComment = _context.NewsComments.Include(n => n.User).Include(n => n.News).Where(n => n.CommentId == CommentId).FirstOrDefault();
            int NewsId = 0;
            if (newsComment == null)
            {
                return NotFound();
            }
            else
            {
                NewsId = newsComment.NewsId;
                _context.Remove(newsComment);
                await _context.SaveChangesAsync();
            }

            return Redirect("/News/Details/?NewsId=" + NewsId);
        }
    }

    public class NewsViewModel
    {
        public News? News { get; set; } = new News();
        public List<NewsComment>? Comments { get; set; } = new List<NewsComment>();
        public List<NewsLike>? Likes { get; set; } = new List<NewsLike>();
        public int UserId { get; set; }
    }
}
