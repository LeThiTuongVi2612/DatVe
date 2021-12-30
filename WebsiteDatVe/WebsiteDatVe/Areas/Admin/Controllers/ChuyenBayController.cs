using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDatVe.Models;

namespace WebsiteDatVe.Areas.Admin.Controllers
{
    public class ChuyenBayController : Controller
    {
        DatVeDB db = new DatVeDB();
        // GET: Admin/ChuyenBay
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            var listChuyenBay = db.ChuyenBays.ToList();
            var listSanBay = db.SanBays.ToList();

            var data = (from c in listChuyenBay
                        join a in listSanBay on c.DiemDi equals a.MaSanBay
                        join b in listSanBay on c.DiemDen equals b.MaSanBay
                        select new
                        {
                            MaChuyenBay = c.MaChuyenBay,
                            DiemDi = a.TenSanBay,
                            DiemDen = b.TenSanBay,
                            ThoiGianDi = c.ThoiGianDi.ToString(),
                            ThoiGianDen = c.ThoiGianDen.ToString(),
                            MaMayBay = c.MaMayBay,
                            Gia = c.Gia
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(ChuyenBay chuyenBay)
        {
            db.ChuyenBays.Add(chuyenBay);
            db.SaveChanges();
            return Json(new { result = 1, log = chuyenBay.Gia }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getById(long Id)
        {
            var chuyenBay = db.ChuyenBays.Where(c => c.MaChuyenBay == Id).FirstOrDefault();
            return View(chuyenBay);
        }


        public JsonResult Update(ChuyenBay chuyenBay)
        {
            var preData = db.ChuyenBays.Where(c => c.MaChuyenBay == chuyenBay.MaChuyenBay).FirstOrDefault();
            preData.DiemDi = chuyenBay.DiemDi;
            preData.DiemDen = chuyenBay.DiemDen;
            preData.ThoiGianDi = chuyenBay.ThoiGianDi;
            preData.ThoiGianDen = chuyenBay.ThoiGianDen;
            preData.MaMayBay = chuyenBay.MaMayBay;
            preData.Gia = chuyenBay.Gia;

            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(long Id)
        {
            var chuyenBay = db.ChuyenBays.Where(c => c.MaChuyenBay == Id).FirstOrDefault();
            db.ChuyenBays.Remove(chuyenBay);

            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult getDiaDiem()
        {
            try
            {
                var lstDiaDiem = (from d in db.SanBays
                                  select new
                                  {
                                      MaSanBay = d.MaSanBay,
                                      NoiDung = d.TenSanBay + " (" + d.DiaChi + ")"
                                  }).ToList();

                return Json(new { code = 200, lstDiaDiem = lstDiaDiem }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getMaMayBay()
        {
            try
            {
                var lstMayBay = (from d in db.MayBays
                                 join h in db.HangBays on d.MahangBay equals h.MaHangBay
                                 select new
                                 {
                                     MaMayBay = d.MaMayBay,
                                     NoiDung = d.MaMayBay + " (" + h.TenHangBay + ")"
                                 }).ToList();

                return Json(new { code = 200, lstMayBay = lstMayBay }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SearchByMaChuyenBay(long? searchKey)
        {
            var resultSearch = db.ChuyenBays.Where(c => c.MaChuyenBay == searchKey || searchKey == null).ToList();
            var data = (from c in resultSearch
                        join a in db.SanBays on c.DiemDi equals a.MaSanBay
                        join b in db.SanBays on c.DiemDen equals b.MaSanBay
                        select new
                        {
                            MaChuyenBay = c.MaChuyenBay,
                            DiemDi = a.TenSanBay,
                            DiemDen = b.TenSanBay,
                            ThoiGianDi = c.ThoiGianDi.ToString(),
                            ThoiGianDen = c.ThoiGianDen.ToString(),
                            MaMayBay = c.MaMayBay,
                            Gia = c.Gia
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchByDiemDi(string searchKey)
        {
            var listSanBay = db.SanBays.Where(s => s.TenSanBay.StartsWith(searchKey) || searchKey == null).ToList();
            var data = (from s1 in listSanBay
                        join c in db.ChuyenBays on s1.MaSanBay equals c.DiemDi
                        join b in db.SanBays on c.DiemDen equals b.MaSanBay
                        select new
                        {
                            MaChuyenBay = c.MaChuyenBay,
                            DiemDi = s1.TenSanBay,
                            DiemDen = b.TenSanBay,
                            ThoiGianDi = c.ThoiGianDi.ToString(),
                            ThoiGianDen = c.ThoiGianDen.ToString(),
                            MaMayBay = c.MaMayBay,
                            Gia = c.Gia
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchByDiemDen(string searchKey)
        {
            var listSanBay = db.SanBays.Where(s => s.TenSanBay.StartsWith(searchKey) || searchKey == null).ToList();
            var data = (from s1 in listSanBay
                        join c in db.ChuyenBays on s1.MaSanBay equals c.DiemDen
                        join b in db.SanBays on c.DiemDi equals b.MaSanBay
                        select new
                        {
                            MaChuyenBay = c.MaChuyenBay,
                            DiemDi = b.TenSanBay,
                            DiemDen = s1.TenSanBay,
                            ThoiGianDi = c.ThoiGianDi.ToString(),
                            ThoiGianDen = c.ThoiGianDen.ToString(),
                            MaMayBay = c.MaMayBay,
                            Gia = c.Gia
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchByThoiGianDi(string searchKey)
        {
            DateTime dateToSearch = DateTime.Parse(searchKey);
            var resultSearch = db.ChuyenBays.Where(c => (c.ThoiGianDi.Value.Day == dateToSearch.Day)
            && (c.ThoiGianDi.Value.Month == dateToSearch.Month)
            && (c.ThoiGianDi.Value.Year == dateToSearch.Year)
            || (searchKey == null)).ToList();
            var data = (from c in resultSearch
                        join a in db.SanBays on c.DiemDi equals a.MaSanBay
                        join b in db.SanBays on c.DiemDen equals b.MaSanBay
                        select new
                        {
                            MaChuyenBay = c.MaChuyenBay,
                            DiemDi = a.TenSanBay,
                            DiemDen = b.TenSanBay,
                            ThoiGianDi = c.ThoiGianDi.ToString(),
                            ThoiGianDen = c.ThoiGianDen.ToString(),
                            MaMayBay = c.MaMayBay,
                            Gia = c.Gia
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchByThoiGianDen(string searchKey)
        {
            DateTime dateToSearch = DateTime.Parse(searchKey);
            var resultSearch = db.ChuyenBays.Where(c => (c.ThoiGianDen.Value.Day == dateToSearch.Day)
            && (c.ThoiGianDen.Value.Month == dateToSearch.Month)
            && (c.ThoiGianDen.Value.Year == dateToSearch.Year)
            || (searchKey == null)).ToList();
            var data = (from c in resultSearch
                        join a in db.SanBays on c.DiemDi equals a.MaSanBay
                        join b in db.SanBays on c.DiemDen equals b.MaSanBay
                        select new
                        {
                            MaChuyenBay = c.MaChuyenBay,
                            DiemDi = a.TenSanBay,
                            DiemDen = b.TenSanBay,
                            ThoiGianDi = c.ThoiGianDi.ToString(),
                            ThoiGianDen = c.ThoiGianDen.ToString(),
                            MaMayBay = c.MaMayBay,
                            Gia = c.Gia
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchByMaMayBay(string searchKey)
        {
            var resultSearch = db.ChuyenBays.Where(c => c.MaMayBay == searchKey || searchKey == null).ToList();
            var data = (from c in resultSearch
                        join a in db.SanBays on c.DiemDi equals a.MaSanBay
                        join b in db.SanBays on c.DiemDen equals b.MaSanBay
                        select new
                        {
                            MaChuyenBay = c.MaChuyenBay,
                            DiemDi = a.TenSanBay,
                            DiemDen = b.TenSanBay,
                            ThoiGianDi = c.ThoiGianDi.ToString(),
                            ThoiGianDen = c.ThoiGianDen.ToString(),
                            MaMayBay = c.MaMayBay,
                            Gia = c.Gia
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchByGiaVe(double? searchKey)
        {
            var resultSearch = db.ChuyenBays.Where(c => c.Gia == searchKey || searchKey == null).ToList();
            var data = (from c in resultSearch
                        join a in db.SanBays on c.DiemDi equals a.MaSanBay
                        join b in db.SanBays on c.DiemDen equals b.MaSanBay
                        select new
                        {
                            MaChuyenBay = c.MaChuyenBay,
                            DiemDi = a.TenSanBay,
                            DiemDen = b.TenSanBay,
                            ThoiGianDi = c.ThoiGianDi.ToString(),
                            ThoiGianDen = c.ThoiGianDen.ToString(),
                            MaMayBay = c.MaMayBay,
                            Gia = c.Gia
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}