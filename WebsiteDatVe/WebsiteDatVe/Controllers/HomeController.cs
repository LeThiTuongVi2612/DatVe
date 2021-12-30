using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDatVe.Models;

namespace WebsiteDatVe.Controllers
{
    public class HomeController : Controller
    {
        private DatVeDB db = new DatVeDB();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(long diemdi, long diemden, int nguoilon, int treem, int embe, DateTime ngaydi, string hangghe)
        {
            ViewBag.DiemDi = db.SanBays.Where(x => x.MaSanBay.Equals(diemdi)).Select(x => x.TenSanBay).SingleOrDefault();
            ViewBag.DiemDen = db.SanBays.Where(x => x.MaSanBay.Equals(diemden)).Select(x => x.TenSanBay).SingleOrDefault();
            ViewBag.SoLuong = nguoilon + treem + embe;

            ThongTinDatVe thongTinDatVe = new ThongTinDatVe();
            thongTinDatVe.DiemDen = diemden;
            thongTinDatVe.DiemDi = diemdi;
            thongTinDatVe.NguoiLon = nguoilon;
            thongTinDatVe.TreEm = treem;
            thongTinDatVe.EmBe = embe;
            thongTinDatVe.NgayDi = ngaydi;
            thongTinDatVe.HangGhe = hangghe;
            Session["ThongTinDatVe"] = thongTinDatVe;
            //Tìm chuyến bay có ngày phù hợp
            List<ChuyenBay> flights = (from c
                          in db.ChuyenBays
                           where c.DiemDi == diemdi && c.DiemDen == diemden
                           && EntityFunctions.TruncateTime(c.ThoiGianDi) == EntityFunctions.TruncateTime(ngaydi) && c.ThoiGianDi > DateTime.Now
                           select c).ToList();
            //Kiểm tra chuyến bay còn vé
            //Tạo list mới lưu số chuyến bay còn vé
            List<ChuyenBay> chuyenbays = new List<ChuyenBay>();

            foreach (var item in flights)
            {

                //Lấy ra số lượng ghế ban đầu của hạng ghế đang tìm
                int socho = (int) item.MayBay.SoGhePhoThong; 

                if (hangghe == "Phổ thông đặc biệt")
                {
                    socho = (int)item.MayBay.SoGhePhoThongDacBiet;
                }
                if (hangghe == "Thương gia")
                {
                    socho = (int)item.MayBay.SoGheThuongGia;
                }
                if (hangghe == "Hạng nhất")
                {
                    socho = (int)item.MayBay.SoGheHangNhat;
                }

                //Lấy ra số vé đã được đặt của hạng ghế đang tìm
                int slve = (from v in db.Ves where v.MaChuyenBay == item.MaChuyenBay && v.HangVe == hangghe && v.TinhTrang != "Canceled" select v).Count();

                //Kiểm tra có đủ chỗ nếu đặt thêm không

                if(socho > (slve+nguoilon+treem))
                {
                    chuyenbays.Add(item);
                }
                
            }


            return View(chuyenbays);
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
            catch(Exception e)
            {
                return Json(new { code = 500 , msg = e.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        
    }
}