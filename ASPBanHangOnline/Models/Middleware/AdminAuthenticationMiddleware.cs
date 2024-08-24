using ASPBanHangOnline.Data;
using ASPBanHangOnline.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ASPBanHangOnline.Models.Middleware
{
    public class AdminAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        

        public AdminAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            int AdminId = context.Session.GetJson<int>("AdminId");
            
            if (context.Request.Path.StartsWithSegments("/Admin") && !context.User.Identity.IsAuthenticated && AdminId <= 0)
            {
                // Nếu người dùng chưa đăng nhập và đang truy cập đường dẫn bắt đầu bằng "/Admin"
                // thì điều hướng đến trang đăng nhập
                context.Response.Redirect("/Admin/Admins/Login"); // Thay đổi "/Login" bằng đường dẫn đến trang đăng nhập của bạn
                return;
            }

            await _next(context);
        }
    }
}
