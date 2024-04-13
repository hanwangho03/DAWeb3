using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAWeb3.Models;

namespace DAWeb3.Controllers
{
    public class HocSinhController : Controller
    {
        private readonly WebTracNghiemContext _context;

        public HocSinhController(WebTracNghiemContext context)
        {
            _context = context;
        }

        // GET: HocSinh
        public async Task<IActionResult> Index()
        {
            var webTracNghiemContext = _context.HocSinhs.Include(h => h.MaKhoiNavigation);
            return View(await webTracNghiemContext.ToListAsync());
        }

        // GET: HocSinh/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocSinh = await _context.HocSinhs
                .Include(h => h.MaKhoiNavigation)
                .FirstOrDefaultAsync(m => m.MaThanhVien == id);
            if (hocSinh == null)
            {
                return NotFound();
            }

            return View(hocSinh);
        }

        // GET: HocSinh/Create
        public IActionResult Create()
        {
            ViewData["MaKhoi"] = new SelectList(_context.Khois, "IdKhoi", "TenKhoi");
            return View();
        }

        // POST: HocSinh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaThanhVien,MatKhau,HoTen,Email,DienThoai,DiaChi,IdNhom,MaKhoi,DaXoa")] HocSinh hocSinh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hocSinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhoi"] = new SelectList(_context.Khois, "IdKhoi", "TenKhoi", hocSinh.MaKhoi);
            return View(hocSinh);
        }

        // GET: HocSinh/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocSinh = await _context.HocSinhs.FindAsync(id);
            if (hocSinh == null)
            {
                return NotFound();
            }
            ViewData["MaKhoi"] = new SelectList(_context.Khois, "IdKhoi", "TenKhoi", hocSinh.MaKhoi);
            return View(hocSinh);
        }

        // POST: HocSinh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaThanhVien,MatKhau,HoTen,Email,DienThoai,DiaChi,IdNhom,MaKhoi,DaXoa")] HocSinh hocSinh)
        {
            if (id != hocSinh.MaThanhVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocSinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocSinhExists(hocSinh.MaThanhVien))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhoi"] = new SelectList(_context.Khois, "IdKhoi", "TenKhoi", hocSinh.MaKhoi);
            return View(hocSinh);
        }

        // GET: HocSinh/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocSinh = await _context.HocSinhs
                .Include(h => h.MaKhoiNavigation)
                .FirstOrDefaultAsync(m => m.MaThanhVien == id);
            if (hocSinh == null)
            {
                return NotFound();
            }

            return View(hocSinh);
        }

        // POST: HocSinh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hocSinh = await _context.HocSinhs.FindAsync(id);
            if (hocSinh != null)
            {
                _context.HocSinhs.Remove(hocSinh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HocSinhExists(string id)
        {
            return _context.HocSinhs.Any(e => e.MaThanhVien == id);
        }
    }
}
