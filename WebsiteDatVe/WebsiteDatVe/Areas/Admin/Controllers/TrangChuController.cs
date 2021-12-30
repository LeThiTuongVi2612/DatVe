using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDatVe.Models;

namespace WebsiteDatVe.Areas.Admin.Controllers
{
    public class TrangChuController : Controller
    {
        DatVeDB db = new DatVeDB();
        // GET: Admin/Home
        public ActionResult Index()
        {
            int nam = 2021;
            ViewBag.tonghangbay = slhangbay();
            ViewBag.tongchuyenbay = slchuyenbay();
            ViewBag.tongkhachhang = slkh();
            ViewBag.tongve = slve();
            ViewBag.tongsanbay = slsb();
            ViewBag.T1 = tongVE1(nam);
            ViewBag.T2 = tongVE2(nam);
            ViewBag.T3 = tongVE3(nam);
            ViewBag.T4 = tongVE4(nam);

            ViewBag.T5 = tongVE5(nam);
            ViewBag.T6 = tongVE6(nam);
            ViewBag.T7 = tongVE7(nam);
            ViewBag.T8 = tongVE8(nam);
            ViewBag.T9 = tongVE9(nam);
            ViewBag.T10 = tongVE10(nam);
            ViewBag.T11 = tongVE11(nam);
            ViewBag.T12 = tongVE12(nam);

            ViewBag.tongtien1 = tongdoanhthu1(nam);
            ViewBag.tongtien2 = tongdoanhthu2(nam);
            ViewBag.tongtien3 = tongdoanhthu3(nam);
            ViewBag.tongtien4 = tongdoanhthu4(nam);
            ViewBag.tongtien5 = tongdoanhthu5(nam);
            ViewBag.tongtien6 = tongdoanhthu6(nam);
            ViewBag.tongtien7 = tongdoanhthu7(nam);
            ViewBag.tongtien8 = tongdoanhthu8(nam);
            ViewBag.tongtien9 = tongdoanhthu9(nam);
            ViewBag.tongtien10 = tongdoanhthu10(nam);
            ViewBag.tongtien11 = tongdoanhthu11(nam);
            ViewBag.tongtien12 = tongdoanhthu12(nam);
            return View();
        }

        public int slhangbay()
        {
            int tonghang = db.HangBays.Count();
            ViewBag.tonghangbay = tonghang;
            return tonghang;

        }
        public int slchuyenbay()
        {
            int tongchuyen = db.ChuyenBays.Count();
            ViewBag.tongchuyenbay = tongchuyen;
            return tongchuyen;

        }
        public int slkh()
        {
            int tongkh = db.KhachHangs.Count();
            ViewBag.tongkhachhang = tongkh;
            return tongkh;

        }
        public int slve()
        {
            int tongve = db.Ves.Count();
            ViewBag.tongve = tongve;
            return tongve;

        }
        public int slsb()
        {
            int tongsb = db.SanBays.Count();
            ViewBag.tongsanbay = tongsb;
            return tongsb;

        }
        public int tongVE1(int nam)
        {

            int Thang1 = db.Ves.Where(n => n.NgayDat.Value.Month == 1 && n.NgayDat.Value.Year == nam).Count();


            return Thang1;
        }
        public int tongVE2(int nam)
        {

            int Thang2 = db.Ves.Where(n => n.NgayDat.Value.Month == 2 && n.NgayDat.Value.Year == nam).Count();


            return Thang2;
        }
        public int tongVE3(int nam)
        {

            int Thang3 = db.Ves.Where(n => n.NgayDat.Value.Month == 3 && n.NgayDat.Value.Year == nam).Count();


            return Thang3;
        }
        public int tongVE4(int nam)
        {

            int Thang4 = db.Ves.Where(n => n.NgayDat.Value.Month == 4 && n.NgayDat.Value.Year == nam).Count();


            return Thang4;
        }
        public int tongVE5(int nam)
        {

            int Thang5 = db.Ves.Where(n => n.NgayDat.Value.Month == 5 && n.NgayDat.Value.Year == nam).Count();


            return Thang5;
        }
        public int tongVE6(int nam)
        {

            int Thang6 = db.Ves.Where(n => n.NgayDat.Value.Month == 6 && n.NgayDat.Value.Year == nam).Count();


            return Thang6;
        }
        public int tongVE7(int nam)
        {

            int Thang7 = db.Ves.Where(n => n.NgayDat.Value.Month == 7 && n.NgayDat.Value.Year == nam).Count();


            return Thang7;
        }
        public int tongVE8(int nam)
        {

            int Thang8 = db.Ves.Where(n => n.NgayDat.Value.Month == 8 && n.NgayDat.Value.Year == nam).Count();


            return Thang8;
        }
        public int tongVE9(int nam)
        {

            int Thang9 = db.Ves.Where(n => n.NgayDat.Value.Month == 9 && n.NgayDat.Value.Year == nam).Count();


            return Thang9;
        }
        public int tongVE10(int nam)
        {

            int Thang10 = db.Ves.Where(n => n.NgayDat.Value.Month == 10 && n.NgayDat.Value.Year == nam).Count();


            return Thang10;
        }
        public int tongVE11(int nam)
        {

            int Thang11 = db.Ves.Where(n => n.NgayDat.Value.Month == 11 && n.NgayDat.Value.Year == nam).Count();


            return Thang11;
        }
        public int tongVE12(int nam)
        {

            int Thang12 = db.Ves.Where(n => n.NgayDat.Value.Month == 12 && n.NgayDat.Value.Year == nam).Count();


            return Thang12;
        }
        //Doanh thu
        public float tongdoanhthu1(int nam)
        {
            var tongtien1 = (db.Ves.Where(n => n.NgayDat.Value.Month == 1 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong1 = float.Parse(tongtien1);
            return tong1;
        }
        public float tongdoanhthu2(int nam)
        {
            var tongtien1 = (db.Ves.Where(n => n.NgayDat.Value.Month == 2 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong2 = float.Parse(tongtien1);
            return tong2;
        }
        public float tongdoanhthu3(int nam)
        {
            var tongtien3 = (db.Ves.Where(n => n.NgayDat.Value.Month == 3 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong3 = float.Parse(tongtien3);
            return tong3;
        }
        public float tongdoanhthu4(int nam)
        {
            var tongtien4 = (db.Ves.Where(n => n.NgayDat.Value.Month == 4 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong4 = float.Parse(tongtien4);
            return tong4;
        }
        public float tongdoanhthu5(int nam)
        {
            var tongtien5 = (db.Ves.Where(n => n.NgayDat.Value.Month == 5 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong5 = float.Parse(tongtien5);
            return tong5;
        }
        public float tongdoanhthu6(int nam)
        {
            var tongtien6 = (db.Ves.Where(n => n.NgayDat.Value.Month == 6 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong1 = float.Parse(tongtien6);
            return tong1;
        }
        public float tongdoanhthu7(int nam)
        {
            var tongtien7 = (db.Ves.Where(n => n.NgayDat.Value.Month == 7 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong1 = float.Parse(tongtien7);
            return tong1;
        }
        public float tongdoanhthu8(int nam)
        {
            var tongtien8 = (db.Ves.Where(n => n.NgayDat.Value.Month == 8 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong1 = float.Parse(tongtien8);
            return tong1;
        }
        public float tongdoanhthu9(int nam)
        {
            var tongtien9 = (db.Ves.Where(n => n.NgayDat.Value.Month == 9 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong1 = float.Parse(tongtien9);
            return tong1;
        }
        public float tongdoanhthu10(int nam)
        {
            var tongtien10 = (db.Ves.Where(n => n.NgayDat.Value.Month == 10 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong1 = float.Parse(tongtien10);
            return tong1;
        }
        public float tongdoanhthu11(int nam)
        {
            var tongtien11 = (db.Ves.Where(n => n.NgayDat.Value.Month == 11 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong1 = float.Parse(tongtien11);
            return tong1;
        }
        public float tongdoanhthu12(int nam)
        {
            var tongtien12 = (db.Ves.Where(n => n.NgayDat.Value.Month == 12 && n.NgayDat.Value.Year == nam).Sum(c => c.TongTien)).Value.ToString();
            float tong1 = float.Parse(tongtien12);
            return tong1;
        }
    }
}