using DAWeb3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAWeb3.Controllers
{
    public class StudenAction : Controller
    {
        private readonly WebTracNghiemContext _context;
        public StudenAction(WebTracNghiemContext context)
        {
            _context = context;
        }

        // GET: DeThi
        public async Task<IActionResult> Index()
        {
            var webTracNghiemContext = _context.DeThis.Include(d => d.NguoiTaoNavigation);
            return View(await webTracNghiemContext.ToListAsync());
        }
        public async Task<IActionResult> Start(int id)
        {
            // Tìm đề thi theo id
            var deThi = await _context.DeThis.FindAsync(id);
            if (deThi == null)
            {
                return NotFound();
            }

            // Truy vấn cơ sở dữ liệu để lấy ra danh sách câu hỏi thuộc đề thi đó
            var cauHoiList = await _context.DeThisChiTiets
                                            .Where(ch => ch.IdDeThi == id)
                                            .Select(ch => ch.IdCauHoiNavigation) // Chọn thuộc tính IdCauHoiNavigation để lấy thông tin chi tiết về câu hỏi
                                            .ToListAsync();

            // Trả về view chứa danh sách các câu hỏi để học sinh thực hiện bài thi
            ViewBag.DeThiId = id;
            return View(cauHoiList);
        }
        public int? GetDapAnDung(int cauHoiId)
        {
            // Thực hiện truy vấn cơ sở dữ liệu để lấy giá trị dapAnDung dựa vào cauHoiId
            var cauHoi = _context.CauHois.FirstOrDefault(c => c.IdCauhoi == cauHoiId);
            if (cauHoi != null)
            {
                return cauHoi.MaDapAn;
            }
            else
            {
                return 0;
            }
        }
        public int? GetThoiGianThi(int maDeThi)
        {
            // Thực hiện truy vấn cơ sở dữ liệu để lấy giá trị dapAnDung dựa vào cauHoiId
            var deThi = _context.DeThis.FirstOrDefault(d => d.IdDeThi == maDeThi);
            if (deThi != null)
            {
                return deThi.ThoiGianThi;
            }
            else
            {
                return 0;
            }
        }
    }
}
