using DAWeb3.Models;
using Microsoft.AspNetCore.Http;
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
            var webTracNghiemContext = _context.DeThis
                                           .Where(d => d.DaXoa == null || d.DaXoa == 0) // Lọc các đề thi chưa bị đánh dấu xóa
                                          .Include(d => d.NguoiTaoNavigation);
            var maDangNhap = HttpContext.Session.GetString("user");
            ViewBag.MaDangNhap = maDangNhap;
            return View(await webTracNghiemContext.ToListAsync());
        }

        public async Task<IActionResult> Start(int id)
        {
            var maDangNhap = HttpContext.Session.GetString("user");
            var ketQua = new KetQua();
            var ketQuaDaNop = await _context.KetQuas
                            .FirstOrDefaultAsync(kq => kq.IdThanhVien == maDangNhap && kq.IdDethi == id && kq.DaNop == 1);

            if (ketQuaDaNop != null)
            {
                var idKetQuaDaNop = ketQuaDaNop.IdKetQua;
                ViewBag.AlertMessage = "Bạn đã hoàn thành bài thi này rồi.";
                return RedirectToAction("XemChiTiet", new { idKetQua = idKetQuaDaNop });
            }


            // Đặt các thuộc tính cho đối tượng KetQua
            // Giả sử bạn muốn lưu IdDeThi khi người dùng bắt đầu thi
            ketQua.IdThanhVien = maDangNhap;
            
            ketQua.IdDethi = id;
            // Thêm đối tượng KetQua vào context
            _context.KetQuas.Add(ketQua);
            // Tìm đề thi theo id
            var deThi = await _context.DeThis.FindAsync(id);
            if (deThi == null)
            {
                return NotFound();
            }
            if (DateTime.Now < deThi.NgayThi)
            {
                
                return RedirectToAction("ThoiGianThi", "StudenAction");
            }
            // Truy vấn cơ sở dữ liệu để lấy ra danh sách câu hỏi thuộc đề thi đó
            var cauHoiList = await _context.DeThisChiTiets
                                            .Where(ch => ch.IdDeThi == id)
                                            .Select(ch => ch.IdCauHoiNavigation) // Chọn thuộc tính IdCauHoiNavigation để lấy thông tin chi tiết về câu hỏi
                                            .ToListAsync();

            // Trả về view chứa danh sách các câu hỏi để học sinh thực hiện bài thi
            await _context.SaveChangesAsync();
            var idKetQua = (int)ketQua.IdKetQua;
            
            // Lưu ID kết quả vào session
            HttpContext.Session.SetInt32("idKetQua", idKetQua);
            ViewBag.IdKetQua = idKetQua;

            ViewBag.DeThiId = id;
            return View(cauHoiList);
        }
        public IActionResult ThoiGianThi()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveAnswer(int idKetQua, int cauHoiId,byte dapandachon,byte dapandung)
        {
            // Tạo một đối tượng ChiTietKetQua mới và thêm vào context
            var chiTietKetQua = new ChiTietKetQua { Idketqua = idKetQua, IdCauhoiDeThi = cauHoiId ,IdDapAnDaChon=dapandachon,IdDapAnDung= dapandung };
            _context.ChiTietKetQuas.Add(chiTietKetQua);
            await _context.SaveChangesAsync();

            // Trả về kết quả hoặc JSON tùy thuộc vào yêu cầu của ứng dụng
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> SubmitAnswers(int idKetQua, float tongDiem)
        {
            // Tìm kết quả trong cơ sở dữ liệu và cập nhật trạng thái đã nộp và tổng điểm
            var ketQua = await _context.KetQuas.FirstOrDefaultAsync(kq => kq.IdKetQua == idKetQua);

            if (ketQua != null)
            {
                ketQua.DaNop = 1;
                ketQua.TongDiem = tongDiem;

                _context.KetQuas.Update(ketQua); // Cập nhật trạng thái của đối tượng

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (DbUpdateException)
                {
                    // Xử lý lỗi khi lưu dữ liệu
                    return StatusCode(500, "Đã xảy ra lỗi khi lưu dữ liệu.");
                }
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Studenaction");
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
        public IActionResult XemChiTiet(int idKetQua)
        {
            var danhSachCauHoiIds = _context.ChiTietKetQuas
        .Where(ctkq => ctkq.Idketqua == idKetQua)
        .Select(ctkq => ctkq.IdCauhoiDeThi)
        .ToList();

            var danhSachCauHoi = _context.CauHois
                .Where(cauHoi => danhSachCauHoiIds.Contains(cauHoi.IdCauhoi))
                .ToList();

            var danhSachDapAnDaChon = new List<byte?>();
            var danhSachIdDapAnDung = new List<byte?>();
            foreach (var cauHoiId in danhSachCauHoiIds)
            {
                var ketQua = _context.ChiTietKetQuas
            .FirstOrDefault(ctkq => ctkq.IdCauhoiDeThi == cauHoiId && ctkq.Idketqua == idKetQua);

                danhSachDapAnDaChon.Add(ketQua?.IdDapAnDaChon);
                danhSachIdDapAnDung.Add(ketQua?.IdDapAnDung);
            }
            var totalScore = _context.KetQuas
    .Where(kq => kq.IdKetQua == idKetQua)
    .Sum(kq => kq.TongDiem);
            ViewBag.TotalScore = totalScore;
            ViewBag.DanhSachDapAnDaChon = danhSachDapAnDaChon;
            ViewBag.DanhSachIdDapAnDung = danhSachIdDapAnDung;
            ViewBag.IdKetQua = idKetQua;
            ViewBag.DanhSachIdCauHoi = danhSachCauHoiIds;
            ViewBag.DanhSachCauHoi = danhSachCauHoi;
            
            return View();
        }
    }
}
