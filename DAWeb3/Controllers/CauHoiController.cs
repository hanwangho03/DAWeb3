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
    public class CauHoiController : Controller
    {
        private readonly WebTracNghiemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CauHoiController(IHttpContextAccessor httpContextAccessor, WebTracNghiemContext context)
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
        // GET: CauHoi
        public async Task<IActionResult> Index()
        {
            
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            var webTracNghiemContext = _context.CauHois.Include(c => c.IdBaiNavigation).Include(c => c.MaDapAnNavigation).Include(c => c.MaMucDoNavigation).Include(c => c.NguoitaoNavigation);
            return View(await webTracNghiemContext.ToListAsync());
        }

        // GET: CauHoi/Details/5
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

            var cauHoi = await _context.CauHois
                .Include(c => c.IdBaiNavigation)
                .Include(c => c.MaDapAnNavigation)
                .Include(c => c.MaMucDoNavigation)
                .Include(c => c.NguoitaoNavigation)
                .FirstOrDefaultAsync(m => m.IdCauhoi == id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            return View(cauHoi);
        }

        // GET: CauHoi/Create
        public async Task<IActionResult> Create()
        {
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            ViewData["IdBai"] = new SelectList(_context.Bais, "Idbai", "TenBai");
            ViewData["MaDapAn"] = new SelectList(_context.DapAns, "Id", "DapAn1");
            ViewData["MaMucDo"] = new SelectList(_context.MucDoKhos, "IdDoKho", "TenMucDo");
            ViewData["Nguoitao"] = new SelectList(_context.GiaoViens, "IdGiaovien", "Hoten");
            
            return View();
        }

        // POST: CauHoi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCauhoi,CauHoi1,DapAnA,DapAnB,DapAnC,DapAnD,MaDapAn,GhiChu,Dapheduyet,IdBai,MaMucDo,Nguoitao,NgayTao,DaXoa")] CauHoi cauHoi)
        {
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (ModelState.IsValid)
            {
                cauHoi.NgayTao = DateTime.Now;
              
                _context.Add(cauHoi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBai"] = new SelectList(_context.Bais, "Idbai", "TenBai", cauHoi.IdBai);
            ViewData["MaDapAn"] = new SelectList(_context.DapAns, "Id", "DapAn1", cauHoi.MaDapAn);
            ViewData["MaMucDo"] = new SelectList(_context.MucDoKhos, "IdDoKho", "TenMucDo", cauHoi.MaMucDo);
            ViewData["Nguoitao"] = new SelectList(_context.GiaoViens, "IdGiaovien", "Hoten", cauHoi.Nguoitao);
            return View(cauHoi);
        }

        // GET: CauHoi/Edit/5
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

            var cauHoi = await _context.CauHois.FindAsync(id);
            if (cauHoi == null)
            {
                return NotFound();
            }
            ViewData["IdBai"] = new SelectList(_context.Bais, "Idbai", "TenBai", cauHoi.IdBai);
            ViewData["MaDapAn"] = new SelectList(_context.DapAns, "Id", "DapAn1", cauHoi.MaDapAn);
            ViewData["MaMucDo"] = new SelectList(_context.MucDoKhos, "IdDoKho", "TenMucDo", cauHoi.MaMucDo);
            ViewData["Nguoitao"] = new SelectList(_context.GiaoViens, "IdGiaovien", "Hoten", cauHoi.Nguoitao);
            return View(cauHoi);
        }

        // POST: CauHoi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdCauhoi,CauHoi1,DapAnA,DapAnB,DapAnC,DapAnD,MaDapAn,GhiChu,Dapheduyet,IdBai,MaMucDo,Nguoitao,NgayTao,DaXoa")] CauHoi cauHoi)
        {
            if (await IsHocSinh())
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (id != cauHoi.IdCauhoi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cauHoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauHoiExists(cauHoi.IdCauhoi))
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
            ViewData["IdBai"] = new SelectList(_context.Bais, "Idbai", "TenBai", cauHoi.IdBai);
            ViewData["MaDapAn"] = new SelectList(_context.DapAns, "Id", "DapAn1", cauHoi.MaDapAn);
            ViewData["MaMucDo"] = new SelectList(_context.MucDoKhos, "IdDoKho", "TenMucDo", cauHoi.MaMucDo);
            ViewData["Nguoitao"] = new SelectList(_context.GiaoViens, "IdGiaovien", "Hoten", cauHoi.Nguoitao);
            return View(cauHoi);
        }

        // GET: CauHoi/Delete/5
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

            var cauHoi = await _context.CauHois
                .Include(c => c.IdBaiNavigation)
                .Include(c => c.MaDapAnNavigation)
                .Include(c => c.MaMucDoNavigation)
                .Include(c => c.NguoitaoNavigation)
                .FirstOrDefaultAsync(m => m.IdCauhoi == id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            return View(cauHoi);
        }

        // POST: CauHoi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cauHoi = await _context.CauHois.FindAsync(id);
            if (cauHoi != null)
            {
                _context.CauHois.Remove(cauHoi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CauHoiExists(long id)
        {
            return _context.CauHois.Any(e => e.IdCauhoi == id);
        }
        
    }
}
