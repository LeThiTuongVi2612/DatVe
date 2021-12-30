using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDatVe.Models;

namespace WebsiteDatVe.Areas.Admin.Controllers
{
    public class HangBayController : Controller
    {
        DatVeDB db = new DatVeDB();
        // GET: Admin/HangBay
        public ActionResult Index()
        {
            return View(db.HangBays.OrderByDescending(n => n.MaHangBay));
        }


        //Tạo mới hãng bay
        //GET: Admin/CreateAirline
        [HttpGet]
        public ActionResult CreateAirline()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddAirline(string TenHangBay, string Hinh)
        {
            try
            {
                HangBay hangBay = new HangBay();
                hangBay.TenHangBay = TenHangBay;
                if (Hinh != "NULL")
                {
                    hangBay.Logo = Hinh;
                }
                db.HangBays.Add(hangBay);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Đăng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Sửa hãng bay
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult EditAirline(long? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View();
            }
            HangBay hb = db.HangBays.SingleOrDefault(c => c.MaHangBay == id);
            if (hb == null)
            {
                return HttpNotFound();
            }
            return View(hb);
        }
        [HttpPost]
        public JsonResult EditAirline(int mahangbay, string TenHangBay, string Hinh)
        {
            try
            {
                var hangBay = (from c in db.HangBays where c.MaHangBay == mahangbay select c).FirstOrDefault();

                hangBay.TenHangBay = TenHangBay;

                hangBay.Logo = Hinh;

                db.SaveChanges();
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Xóa Hãng bay
        [HttpPost]
        public JsonResult DeleteAirline(int mahangbay)
        {
            try
            {
                var hb = (from c in db.HangBays where c.MaHangBay == mahangbay select c).FirstOrDefault();
                db.HangBays.Remove(hb);
                db.SaveChanges();

                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public string XuLyFile(HttpPostedFileBase file)
        {
            string path = Server.MapPath("~/Images/" + file.FileName);
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                file.SaveAs(path);
            }
            catch { }

            return "/Images/" + file.FileName;

        }
    }
}