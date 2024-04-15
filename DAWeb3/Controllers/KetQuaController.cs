﻿using DAWeb3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAWeb3.Controllers
{
    public class KetQuaController : Controller
    {
        private readonly WebTracNghiemContext _context;
        public KetQuaController(WebTracNghiemContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var webTracNghiemContext = _context.DeThis.Include(d => d.NguoiTaoNavigation);
            return View(await webTracNghiemContext.ToListAsync());
        }
        public async Task<IActionResult> XemKetQua(int id)
        {
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
