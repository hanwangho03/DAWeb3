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
    public class DeThiController : Controller
    {
        private readonly WebTracNghiemContext _context;

        public DeThiController(WebTracNghiemContext context)
        {
            _context = context;
        }

        // GET: DeThi
        public async Task<IActionResult> Index()
        {
            var webTracNghiemContext = _context.DeThis.Include(d => d.NguoiTaoNavigation);
            return View(await webTracNghiemContext.ToListAsync());
        }

        // GET: DeThi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deThi = await _context.DeThis
                .Include(d => d.NguoiTaoNavigation)
                .FirstOrDefaultAsync(m => m.IdDeThi == id);
            if (deThi == null)
            {
                return NotFound();
            }

            return View(deThi);
        }

        // GET: DeThi/Create
        public IActionResult Create()
        {
            ViewData["NguoiTao"] = new SelectList(_context.GiaoViens, "IdGiaovien", "Hoten");
            return View();
        }

        // POST: DeThi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDeThi,NgayThi,ThoiGianThi,SoLuongCauHoi,TenDeThi,DaXoa,NguoiTao")] DeThi deThi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deThi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NguoiTao"] = new SelectList(_context.GiaoViens, "IdGiaovien", "Hoten", deThi.NguoiTao);
            return View(deThi);
        }

        // GET: DeThi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deThi = await _context.DeThis.FindAsync(id);
            if (deThi == null)
            {
                return NotFound();
            }
            ViewData["NguoiTao"] = new SelectList(_context.GiaoViens, "IdGiaovien", "Hoten", deThi.NguoiTao);
            return View(deThi);
        }

        // POST: DeThi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDeThi,NgayThi,ThoiGianThi,SoLuongCauHoi,TenDeThi,DaXoa,NguoiTao")] DeThi deThi)
        {
            if (id != deThi.IdDeThi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deThi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeThiExists(deThi.IdDeThi))
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
            ViewData["NguoiTao"] = new SelectList(_context.GiaoViens, "IdGiaovien", "Hoten", deThi.NguoiTao);
            return View(deThi);
        }

        // GET: DeThi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deThi = await _context.DeThis
                .Include(d => d.NguoiTaoNavigation)
                .FirstOrDefaultAsync(m => m.IdDeThi == id);
            if (deThi == null)
            {
                return NotFound();
            }

            return View(deThi);
        }

        // POST: DeThi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deThi = await _context.DeThis.FindAsync(id);
            if (deThi != null)
            {
                _context.DeThis.Remove(deThi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeThiExists(int id)
        {
            return _context.DeThis.Any(e => e.IdDeThi == id);
        }
    }
}
