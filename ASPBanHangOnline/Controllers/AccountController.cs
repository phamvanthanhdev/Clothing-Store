using ASPBanHangOnline.Data;
using ASPBanHangOnline.Infrastructure;
using ASPBanHangOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPBanHangOnline.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private DataLogin data { get; set; } = new DataLogin();
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult SignUp()
        {
            return View(new User());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,UserEmail,Passwords,Birthday,Sex,Phone")] User user, string? RememberPass)
        {
            if (ModelState.IsValid)
            {
                var emailExist = _context.Users.Where(u => u.UserEmail == user.UserEmail).FirstOrDefault();
                if (user.Passwords == RememberPass && emailExist==null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    return View("SignUp", user);
                }
  
            }
            return View("SignUp",user);
        }

        public IActionResult Login()
        {
            return View(new DataLogin());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserEmail,Passwords")] DataLogin data)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(u => u.UserEmail == data.UserEmail && u.Passwords == data.Passwords).FirstOrDefault();
                if (user != null)
                {
                    //Gửi dữ liệu lên session
                    //UserId
                    HttpContext.Session.SetJson("UserId", user.UserId);
                    //LastName
                    HttpContext.Session.SetJson("LastName", user.LastName);

                    return Redirect("/");
                }
                else
                {
                    data.Message = "Email or Password don't match !";
                    return View(data);
                }

            }
            data.Message = "Email and Password can't be empty !";
            return View(data);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("LastName");

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Info()
        {
            int id = HttpContext.Session.GetJson<int>("UserId");
            var user = _context.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("UserId,FirstName,LastName,UserEmail,Passwords, Birthday,Sex,Phone")] User user)
        {
            //if (ModelState.IsValid)
            //{
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Redirect("/");
            //}
            
            return View("Info", user);
        }
    }

    public class DataLogin
    {
        public string? UserEmail { get; set; }
        public string? Passwords { get; set; }
        public string? Message { get; set; }
    }
}
