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
    public class MonHocController : Controller
    {
        private readonly WebTracNghiemContext _context;

        public MonHocController(WebTracNghiemContext context)
        {
            _context = context;
        }

        // GET: MonHoc
        public async Task<IActionResult> Index()
        {
            var webTracNghiemContext = _context.MonHocs.Include(m => m.MaKhoiNavigation);
            return View(await webTracNghiemContext.ToListAsync());
        }

        // GET: MonHoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monHoc = await _context.MonHocs
                .Include(m => m.MaKhoiNavigation)
                .FirstOrDefaultAsync(m => m.IdMonHoc == id);
            if (monHoc == null)
            {
                return NotFound();
            }

            return View(monHoc);
        }

        // GET: MonHoc/Create
        public IActionResult Create()
        {
            ViewData["MaKhoi"] = new SelectList(_context.Khois, "IdKhoi", "TenKhoi");
            return View();
        }

        // POST: MonHoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMonHoc,TenMonHoc,Meta,MaKhoi,DaXoa")] MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhoi"] = new SelectList(_context.Khois, "IdKhoi", "TenKhoi", monHoc.MaKhoi);
            return View(monHoc);
        }

        // GET: MonHoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monHoc = await _context.MonHocs.FindAsync(id);
            if (monHoc == null)
            {
                return NotFound();
            }
            ViewData["MaKhoi"] = new SelectList(_context.Khois, "IdKhoi", "TenKhoi", monHoc.MaKhoi);
            return View(monHoc);
        }

        // POST: MonHoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMonHoc,TenMonHoc,Meta,MaKhoi,DaXoa")] MonHoc monHoc)
        {
            if (id != monHoc.IdMonHoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonHocExists(monHoc.IdMonHoc))
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
            ViewData["MaKhoi"] = new SelectList(_context.Khois, "IdKhoi", "TenKhoi", monHoc.TenMonHoc);
            return View(monHoc);
        }

        // GET: MonHoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monHoc = await _context.MonHocs
                .Include(m => m.MaKhoiNavigation)
                .FirstOrDefaultAsync(m => m.IdMonHoc == id);
            if (monHoc == null)
            {
                return NotFound();
            }

            return View(monHoc);
        }

        // POST: MonHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monHoc = await _context.MonHocs.FindAsync(id);
            if (monHoc != null)
            {
                _context.MonHocs.Remove(monHoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonHocExists(int id)
        {
            return _context.MonHocs.Any(e => e.IdMonHoc == id);
        }
    }
}
