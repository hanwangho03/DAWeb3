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
    public class DeThiCauHoiController : Controller
    {
        
        private readonly WebTracNghiemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeThiCauHoiController(IHttpContextAccessor httpContextAccessor, WebTracNghiemContext context)
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
        // GET: DeThiCauHoi
        public async Task<IActionResult> Index()
        {
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            var webTracNghiemContext = _context.DeThisChiTiets.Include(d => d.IdCauHoiNavigation).Include(d => d.IdDeThiNavigation).OrderBy(d => d.IdDeThiNavigation.TenDeThi); ;
            return View(await webTracNghiemContext.ToListAsync());
        }

        // GET: DeThiCauHoi/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (id == null)
            {
                return NotFound();
            }

            var deThisChiTiet = await _context.DeThisChiTiets
                .Include(d => d.IdCauHoiNavigation)
                .Include(d => d.IdDeThiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deThisChiTiet == null)
            {
                return NotFound();
            }

            return View(deThisChiTiet);
        }

        // GET: DeThiCauHoi/Create
        public async Task<IActionResult> Create()
        {
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            ViewData["IdCauHoi"] = new SelectList(_context.CauHois, "IdCauhoi", "CauHoi1");
            ViewData["IdDeThi"] = new SelectList(_context.DeThis, "IdDeThi", "TenDeThi");
            return View();
        }

        // POST: DeThiCauHoi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdDeThi,IdCauHoi")] DeThisChiTiet deThisChiTiet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deThisChiTiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCauHoi"] = new SelectList(_context.CauHois, "IdCauhoi", "CauHoi1", deThisChiTiet.IdCauHoi);
            ViewData["IdDeThi"] = new SelectList(_context.DeThis, "IdDeThi", "TenDeThi", deThisChiTiet.IdDeThi);
            return View(deThisChiTiet);
        }

        // GET: DeThiCauHoi/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (id == null)
            {
                return NotFound();
            }

            var deThisChiTiet = await _context.DeThisChiTiets.FindAsync(id);
            if (deThisChiTiet == null)
            {
                return NotFound();
            }
            ViewData["IdCauHoi"] = new SelectList(_context.CauHois, "IdCauhoi", "CauHoi1", deThisChiTiet.IdCauHoi);
            ViewData["IdDeThi"] = new SelectList(_context.DeThis, "IdDeThi", "TenDeThi", deThisChiTiet.IdDeThi);
            return View(deThisChiTiet);
        }

        // POST: DeThiCauHoi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IdDeThi,IdCauHoi")] DeThisChiTiet deThisChiTiet)
        {
            if (id != deThisChiTiet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deThisChiTiet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeThisChiTietExists(deThisChiTiet.Id))
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
            ViewData["IdCauHoi"] = new SelectList(_context.CauHois, "IdCauhoi", "CauHoi1", deThisChiTiet.IdCauHoi);
            ViewData["IdDeThi"] = new SelectList(_context.DeThis, "IdDeThi", "TenDeThi", deThisChiTiet.IdDeThi);
            return View(deThisChiTiet);
        }

        // GET: DeThiCauHoi/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (id == null)
            {
                return NotFound();
            }

            var deThisChiTiet = await _context.DeThisChiTiets
                .Include(d => d.IdCauHoiNavigation)
                .Include(d => d.IdDeThiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deThisChiTiet == null)
            {
                return NotFound();
            }

            return View(deThisChiTiet);
        }

        // POST: DeThiCauHoi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var deThisChiTiet = await _context.DeThisChiTiets.FindAsync(id);
            if (deThisChiTiet != null)
            {
                _context.DeThisChiTiets.Remove(deThisChiTiet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeThisChiTietExists(long id)
        {
            return _context.DeThisChiTiets.Any(e => e.Id == id);
        }
    }
}
