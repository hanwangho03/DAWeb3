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
    public class BaiController : Controller
    {
        private readonly WebTracNghiemContext _context;

        public BaiController(WebTracNghiemContext context)
        {
            _context = context;
        }

        // GET: Bai
        public async Task<IActionResult> Index()
        {
            var webTracNghiemContext = _context.Bais.Include(b => b.IdchuongNavigation);
            return View(await webTracNghiemContext.ToListAsync());
        }

        // GET: Bai/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bai = await _context.Bais
                .Include(b => b.IdchuongNavigation)
                .FirstOrDefaultAsync(m => m.Idbai == id);
            if (bai == null)
            {
                return NotFound();
            }

            return View(bai);
        }

        // GET: Bai/Create
        public IActionResult Create()
        {
            ViewData["Idchuong"] = new SelectList(_context.Chuongs, "IdChuong", "Tenchuong");
            return View();
        }

        // POST: Bai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create([Bind("Idbai,TenBai,Meta,Idchuong,DaXoa")] Bai bai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idchuong"] = new SelectList(_context.Chuongs, "IdChuong", "Tenchuong", bai.Idchuong);
            return View(bai);
        }

        // GET: Bai/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bai = await _context.Bais.FindAsync(id);
            if (bai == null)
            {
                return NotFound();
            }
            ViewData["Idchuong"] = new SelectList(_context.Chuongs, "IdChuong", "Tenchuong", bai.Idchuong);
            return View(bai);
        }

        // POST: Bai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idbai,TenBai,Meta,Idchuong,DaXoa")] Bai bai)
        {
            if (id != bai.Idbai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaiExists(bai.Idbai))
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
            ViewData["Idchuong"] = new SelectList(_context.Chuongs, "IdChuong", "Tenchuong", bai.Idchuong);
            return View(bai);
        }

        // GET: Bai/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bai = await _context.Bais
                .Include(b => b.IdchuongNavigation)
                .FirstOrDefaultAsync(m => m.Idbai == id);
            if (bai == null)
            {
                return NotFound();
            }

            return View(bai);
        }

        // POST: Bai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bai = await _context.Bais.FindAsync(id);
            if (bai != null)
            {
                _context.Bais.Remove(bai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiExists(int id)
        {
            return _context.Bais.Any(e => e.Idbai == id);
        }
    }
}
