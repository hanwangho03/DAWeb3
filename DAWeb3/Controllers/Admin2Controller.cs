﻿using DAWeb3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAWeb3.Controllers
{
    public class Admin2Controller : Controller
    {
        private readonly WebTracNghiemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Admin2Controller(IHttpContextAccessor httpContextAccessor, WebTracNghiemContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        // GET: Admin2
        public async Task<IActionResult> Index()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var username = session.GetString("user");

            // Kiểm tra xem tài khoản đăng nhập có trong bảng admin không
            var isAdmin = _context.Admins.Any(a => a.TaiKhoan == username);
            if (!isAdmin)
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            
            return View(await _context.Admins.ToListAsync());
        }

        // GET: Admin2/Create
        public IActionResult Create()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var username = session.GetString("user");

            // Kiểm tra xem tài khoản đăng nhập có trong bảng admin không
            var isAdmin = _context.Admins.Any(a => a.TaiKhoan == username);
            if (!isAdmin)
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            return View();
        }

        // POST: Admin2/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaiKhoan,MatKhau,DaXoa")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admin2/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var username = session.GetString("user");

            // Kiểm tra xem tài khoản đăng nhập có trong bảng admin không
            var isAdmin = _context.Admins.Any(a => a.TaiKhoan == username);
            if (!isAdmin)
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admin2/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TaiKhoan,MatKhau,DaXoa")] Admin admin)
        {
            if (id != admin.TaiKhoan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.TaiKhoan))
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
            return View(admin);
        }

        // GET: Admin2/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var username = session.GetString("user");

            // Kiểm tra xem tài khoản đăng nhập có trong bảng admin không
            var isAdmin = _context.Admins.Any(a => a.TaiKhoan == username);
            if (!isAdmin)
            {
                return RedirectToAction("AccessDenied", "Admin");
            }
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.TaiKhoan == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admin2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var admin = await _context.Admins.FindAsync(id);
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.TaiKhoan == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }
        private bool AdminExists(string id)
        {
            return _context.Admins.Any(e => e.TaiKhoan == id);
        }
    }
}
