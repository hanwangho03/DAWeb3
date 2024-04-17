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
    public class GiaoVienController : Controller
    {
        private readonly WebTracNghiemContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public GiaoVienController(IHttpContextAccessor httpContextAccessor, WebTracNghiemContext context)
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

        // GET: GiaoVien
        public async Task<IActionResult> Index()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var username = session.GetString("user");
            var isAdmin = _context.Admins.Any(a => a.TaiKhoan == username);
            if (!isAdmin)
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            return View(await _context.GiaoViens.ToListAsync());
        }

        // GET: GiaoVien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var username = session.GetString("user");
            var isAdmin = _context.Admins.Any(a => a.TaiKhoan == username);
            if (!isAdmin)
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (id == null)
            {
                return NotFound();
            }

            var giaoVien = await _context.GiaoViens
                .FirstOrDefaultAsync(m => m.IdGiaovien == id);
            if (giaoVien == null)
            {
                return NotFound();
            }

            return View(giaoVien);
        }

        // GET: GiaoVien/Create
        public async Task<IActionResult> Create()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var username = session.GetString("user");
            var isAdmin = _context.Admins.Any(a => a.TaiKhoan == username);
            if (!isAdmin)
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            return View();
        }

        // POST: GiaoVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Magiaovien,IdGiaovien,Matkhau,Hoten,Sdt,Email,DaXoa,LaTruongBm")] GiaoVien giaoVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giaoVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(giaoVien);
        }

        // GET: GiaoVien/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var username = session.GetString("user");
            var isAdmin = _context.Admins.Any(a => a.TaiKhoan == username);
            if (!isAdmin)
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (id == null)
            {
                return NotFound();
            }

            var giaoVien = await _context.GiaoViens.FindAsync(id);
            if (giaoVien == null)
            {
                return NotFound();
            }
            return View(giaoVien);
        }

        // POST: GiaoVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Magiaovien,IdGiaovien,Matkhau,Hoten,Sdt,Email,DaXoa,LaTruongBm")] GiaoVien giaoVien)
        {
            if (id != giaoVien.IdGiaovien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giaoVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiaoVienExists(giaoVien.IdGiaovien))
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
            return View(giaoVien);
        }

        // GET: GiaoVien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var username = session.GetString("user");
            var isAdmin = _context.Admins.Any(a => a.TaiKhoan == username);
            if (!isAdmin)
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (id == null)
            {
                return NotFound();
            }

            var giaoVien = await _context.GiaoViens
                .FirstOrDefaultAsync(m => m.IdGiaovien == id);
            if (giaoVien == null)
            {
                return NotFound();
            }

            return View(giaoVien);
        }

        // POST: GiaoVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giaoVien = await _context.GiaoViens.FindAsync(id);
            if (giaoVien != null)
            {
                _context.GiaoViens.Remove(giaoVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiaoVienExists(int id)
        {
            return _context.GiaoViens.Any(e => e.IdGiaovien == id);
        }
    }
}
