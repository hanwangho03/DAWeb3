using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using DAWeb3.Models;

namespace DAWeb3.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly WebTracNghiemContext _context;

        public AdminController(IHttpContextAccessor httpContextAccessor, WebTracNghiemContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public IActionResult Index()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            if (session.GetString("user") == null)
            {
                return RedirectToAction("Login", "Default");
            }

            var username = session.GetString("user");

            // Kiểm tra xem tài khoản đăng nhập có trong bảng admin không
            var isAdmin = _context.Admins.Any(a => a.TaiKhoan == username);
            if (!isAdmin)
            {
                return RedirectToAction("AccessDenied", "Admin");
            }

            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        // Sửa lại tên action thành "MenuPartial" để phản ánh chức năng của nó
        public IActionResult MenuPartial()
        {
            return PartialView("_Navbar");
        }
    }
}
