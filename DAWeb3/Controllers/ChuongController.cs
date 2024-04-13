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
    public class ChuongController : Controller
    {
        private readonly WebTracNghiemContext _context;

        public ChuongController(WebTracNghiemContext context)
        {
            _context = context;
        }

        // GET: Chuong
        public async Task<IActionResult> Index()
        {
            var webTracNghiemContext = _context.Chuongs.Include(c => c.IdMonHocNavigation);
            return View(await webTracNghiemContext.ToListAsync());
        }

        // GET: Chuong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuong = await _context.Chuongs
                .Include(c => c.IdMonHocNavigation)
                .FirstOrDefaultAsync(m => m.IdChuong == id);
            if (chuong == null)
            {
                return NotFound();
            }

            return View(chuong);
        }

        // GET: Chuong/Create
        public IActionResult Create()
        {
            ViewData["IdMonHoc"] = new SelectList(_context.MonHocs, "IdMonHoc", "TenMonHoc");
            return View();
        }

        // POST: Chuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdChuong,Tenchuong,Meta,IdMonHoc,DaXoa")] Chuong chuong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMonHoc"] = new SelectList(_context.MonHocs, "IdMonHoc", "TenMonHoc", chuong.IdMonHoc);
            return View(chuong);
        }

        // GET: Chuong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuong = await _context.Chuongs.FindAsync(id);
            if (chuong == null)
            {
                return NotFound();
            }
            ViewData["IdMonHoc"] = new SelectList(_context.MonHocs, "IdMonHoc", "TenMonHoc", chuong.IdMonHoc);
            return View(chuong);
        }

        // POST: Chuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdChuong,Tenchuong,Meta,IdMonHoc,DaXoa")] Chuong chuong)
        {
            if (id != chuong.IdChuong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chuong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChuongExists(chuong.IdChuong))
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
            ViewData["IdMonHoc"] = new SelectList(_context.MonHocs, "IdMonHoc", "TenMonHoc", chuong.IdMonHoc);
            return View(chuong);
        }

        // GET: Chuong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuong = await _context.Chuongs
                .Include(c => c.IdMonHocNavigation)
                .FirstOrDefaultAsync(m => m.IdChuong == id);
            if (chuong == null)
            {
                return NotFound();
            }

            return View(chuong);
        }

        // POST: Chuong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chuong = await _context.Chuongs.FindAsync(id);
            if (chuong != null)
            {
                _context.Chuongs.Remove(chuong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChuongExists(int id)
        {
            return _context.Chuongs.Any(e => e.IdChuong == id);
        }
    }
}
