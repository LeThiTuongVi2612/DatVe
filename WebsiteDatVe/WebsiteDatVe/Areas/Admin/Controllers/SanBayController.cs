using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDatVe.Models;

namespace WebsiteDatVe.Areas.Admin.Controllers
{
    public class SanBayController : Controller
    {
        DatVeDB db = new DatVeDB();
        // GET: Admin/SanBay
        public ActionResult Index()
        {
            return View(db.SanBays.ToList());
        }

        public ActionResult ThemSanBay()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ThemSanBay(string Tensanbay, string diachi)
        {
            try
            {
                SanBay sanbay = new SanBay();
                sanbay.TenSanBay = Tensanbay;
                sanbay.DiaChi = diachi;

                db.SanBays.Add(sanbay);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Đăng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [ValidateInput(false)]
        [HttpGet]
        public ActionResult EditSanBay(int? id)
        {
            SanBay sb = db.SanBays.SingleOrDefault(c => c.MaSanBay == id);

            if (sb == null)
            {
                return HttpNotFound();
            }
            return View(sb);
        }
        [HttpPost]
        public JsonResult EditSanBay(int masb, string ten, string diachi)
        {

            try
            {
                var sanbay = (from c in db.SanBays where c.MaSanBay == masb select c).FirstOrDefault();

                sanbay.TenSanBay = ten;
                sanbay.DiaChi = diachi;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Đăng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }




        }
        [HttpPost]
        public JsonResult DeleteSanbay(int id)
        {

            try
            {
                var sanbay = (from c in db.SanBays where c.MaSanBay == id select c).FirstOrDefault();
                db.SanBays.Remove(sanbay);
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