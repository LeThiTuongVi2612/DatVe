using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDatVe.Models;

namespace WebsiteDatVe.Areas.Admin.Controllers
{
    public class MayBayController : Controller
    {
        DatVeDB db = new DatVeDB();
        // GET: Admin/MayBay
        public ActionResult Index()
        {
            return View(db.MayBays);
        }

        //Tạo mới máy bay
        //GET: Admin/CreatePlane
        [HttpGet]
        public ActionResult CreatePhane()
        {
            ViewBag.MaHangBay = new SelectList(db.HangBays.OrderBy(c => c.TenHangBay), "MaHangBay", "TenHangBay");
            return View();
        }
        [HttpPost]
        public JsonResult AddPhane(string MaMayBay, int? GhePhoThong, int? GheThuongGia, int? GheHangNhat, int? GheDatBiet, long? MaHangBay)
        {
            try
            {
                MayBay maybay = new MayBay();
                maybay.MaMayBay = MaMayBay;
                maybay.SoGhePhoThong = GhePhoThong;
                maybay.SoGheThuongGia = GheThuongGia;
                maybay.SoGheHangNhat = GheHangNhat;
                maybay.SoGhePhoThongDacBiet = GheDatBiet;
                maybay.MahangBay = MaHangBay;
                db.MayBays.Add(maybay);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Đăng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Sửa máy bay
        [HttpGet]
        public ActionResult EditPlane(string id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View();
            }
            MayBay maybay = db.MayBays.SingleOrDefault(c => c.MaMayBay == id);
            if (maybay == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHangBay = new SelectList(db.HangBays.OrderBy(c => c.TenHangBay), "MaHangBay", "TenHangBay", maybay.MahangBay);
            return View(maybay);
        }
        [HttpPost]
        public JsonResult UpdatePhane(string MaMayBay, int? GhePhoThong, int? GheThuongGia, int? GheHangNhat, int? GheDatBiet, long? MaHangBay)
        {
            try
            {
                var maybay = (from c in db.MayBays where c.MaMayBay == MaMayBay select c).FirstOrDefault();
                maybay.MaMayBay = MaMayBay;
                maybay.SoGhePhoThong = GhePhoThong;
                maybay.SoGheThuongGia = GheThuongGia;
                maybay.SoGheHangNhat = GheHangNhat;
                maybay.SoGhePhoThongDacBiet = GheDatBiet;
                maybay.MahangBay = MaHangBay;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Đăng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Xóa máy bay
        public JsonResult DeletePlane(string mamaybay)
        {
            try
            {
                var maybay = (from c in db.MayBays where c.MaMayBay == mamaybay select c).FirstOrDefault();
                db.MayBays.Remove(maybay);
                db.SaveChanges();
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}