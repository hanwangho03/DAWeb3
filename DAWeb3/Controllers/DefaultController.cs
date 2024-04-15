using DAWeb3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAWeb3.Controllers
{
    public class DefaultController : Controller
    {
        private readonly WebTracNghiemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DefaultController(WebTracNghiemContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {

            var session = _httpContextAccessor.HttpContext.Session;
            if (session.GetString("user") != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
            
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var usr = username;
            var pass = password;

            var acc = _context.Admins.SingleOrDefault(x => x.TaiKhoan == usr && x.MatKhau == pass);
            if (acc != null)
            {
                HttpContext.Session.SetString("user", username);
                TempData["AlertMessage"] = "Mã thành viên: " + acc.TaiKhoan; // Thêm mã thành viên vào TempData để hiển thị trong alert
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                var accteacher = _context.GiaoViens.SingleOrDefault(x => x.Magiaovien == usr && x.Matkhau == pass);
                if (accteacher != null)
                {
                    HttpContext.Session.SetString("user", username);
                    TempData["AlertMessage"] = "Mã thành viên: " + accteacher.Magiaovien; // Thêm mã thành viên vào TempData để hiển thị trong alert
                    return RedirectToAction("Index", "KetQua");
                }
                else
                {
                    var student = _context.HocSinhs.SingleOrDefault(x => x.MaThanhVien == usr && x.MatKhau == pass);
                    if (student != null)
                    {
                        HttpContext.Session.SetString("user", username);
                        TempData["AlertMessage"] =  student.MaThanhVien; // Thêm mã thành viên vào TempData để hiển thị trong alert
                        return RedirectToAction("Index", "StudenAction");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Đăng nhập thất bại!";
                        return RedirectToAction("Login");
                    }
                }
            }
        }
        [HttpGet]
        public ActionResult Logout()
        {
            // Xóa session khi đăng xuất
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
