using DAWeb3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAWeb3.Controllers
{
    public class KetQuaController : Controller
    {
        private readonly WebTracNghiemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public KetQuaController(IHttpContextAccessor httpContextAccessor, WebTracNghiemContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        private async Task<bool> IsHocSinh()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var username = session.GetString("user");
            return await _context.HocSinhs.AnyAsync(h => h.MaThanhVien == username);
        }
        public async Task<IActionResult> Index()
        {
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            var webTracNghiemContext = _context.DeThis.Include(d => d.NguoiTaoNavigation);
            return View(await webTracNghiemContext.ToListAsync());
        }
        public async Task<IActionResult> XemKetQua(int id)
        {
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            // Lấy danh sách các chi tiết kết quả dựa trên ID đề thi
            var ketQuas = await _context.KetQuas
    .Include(kq => kq.IdThanhVienNavigation)
        .ThenInclude(hs => hs.MaKhoiNavigation)
    .Where(kq => kq.IdDethi == id)
    .ToListAsync();

            if (ketQuas == null)
            {
                return NotFound();
            }

            return View(ketQuas);
        }

    }

}
