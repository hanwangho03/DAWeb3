using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAWeb3.Models;
using System.Drawing;

namespace DAWeb3.Controllers
{
    public class KhoiController : Controller
    {
        private readonly WebTracNghiemContext _context;

        public KhoiController(WebTracNghiemContext context)
        {
            _context = context;
        }

        // GET: Khoi
        public IActionResult Index()
        {
            return View();
        }

       public JsonResult DsKhoi()
        {
            try
            {
                var dskhoi = (from l in _context.Khois.Where(x => x.DaXoa != 1)
                              select new
                              {
                                  Id = l.IdKhoi,
                                  TenKhoi = l.TenKhoi,
                                  Meta = l.Meta,
                              }).ToList();
                return Json(new { code = 200, dskhoi = dskhoi, msg = "Lấy danh sách khối thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách khối thất bại: " + ex.Message });
            }
        }
        public JsonResult AddLop(string tenkhoi, string meta)
        {
            try
            {
                var l = new Khoi();
                l.TenKhoi = tenkhoi;
                l.Meta = meta;
                _context.Khois.Add(l);
                _context.SaveChanges();
                return Json(new { code = 200, msg = "Them moi khoi thanh cong " });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Them moi khối thất bại: " + ex.Message });
            }
        }
        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            try
            {

                var khoi = _context.Khois.SingleOrDefault(k => k.IdKhoi == id);

                if (khoi != null)
                {
                    // Trả về thông tin chi tiết của khối dưới dạng JSON
                    return Json(new { code = 200, K = khoi, msg = "Lấy thông tin chi tiết của khối thành công." });
                }
                else
                {
                    return Json(new { code = 404, msg = "Không tìm thấy khối." });
                }
            }
            catch (Exception ex)
            {
                
                return Json(new { code = 500, msg = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        public JsonResult Delete(int id)
        {
            try
            {

                var khoi = _context.Khois.SingleOrDefault(k => k.IdKhoi == id);

                khoi.DaXoa = 1;
                _context.SaveChanges();
                // Trả về thông tin chi tiết của khối dưới dạng JSON
                return Json(new { code = 200,  msg = "Xoa khoi thanh cong" });
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        public JsonResult Update(int id,string tenkhoi,string meta)
        {
            try
            {

                var khoi = _context.Khois.SingleOrDefault(k => k.IdKhoi == id);

                khoi.TenKhoi = tenkhoi;
                khoi.Meta = meta;
                _context.SaveChanges();
                // Trả về thông tin chi tiết của khối dưới dạng JSON
                return Json(new { code = 200, msg = "Cap nhat khoi thanh cong" });
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
    }

    }

