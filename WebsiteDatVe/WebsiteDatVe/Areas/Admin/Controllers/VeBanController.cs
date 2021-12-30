using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDatVe.Models;

namespace WebsiteDatVe.Areas.Admin.Controllers
{
    public class VeBanController : Controller
    {
        DatVeDB db = new DatVeDB();
        // GET: Admin/VeBan
        public ActionResult Index(string searchKey = "", int option = 0)
        {
            //Tìm danh sách mã chuyến bay
            List<long> listMaChuyenBay = new List<long>();
            foreach(var item in db.ChuyenBays.ToList())
            {
                listMaChuyenBay.Add(item.MaChuyenBay);
            }

            //Tìm danh sách điểm đi
            List<long> listDiemDiInt = new List<long>();
            foreach(var item in db.ChuyenBays.ToList())
            {
                listDiemDiInt.Add(item.DiemDi.GetValueOrDefault(0));
            }
            List<string> listDiemDiString = new List<string>();
            foreach(var item in listDiemDiInt)
            {
                listDiemDiString.Add(db.SanBays.Where(s => s.MaSanBay == item).FirstOrDefault().TenSanBay);
            }
            //Tìm danh sách điểm đến
            List<long> listDiemDenInt = new List<long>();
            foreach (var item in db.ChuyenBays.ToList())
            {
                listDiemDenInt.Add(item.DiemDen.GetValueOrDefault(0));
            }
            List<string> listDiemDenString = new List<string>();
            foreach (var item in listDiemDenInt)
            {
                listDiemDenString.Add(db.SanBays.Where(s => s.MaSanBay == item).FirstOrDefault().TenSanBay);
            }
            //Tìm danh sách mã máy bay
            List<string> listMaMayBay = new List<string>();
            foreach(var item in db.ChuyenBays.ToList())
            {
                listMaMayBay.Add(item.MaMayBay);
            }
            List<string> listMayBay = new List<string>();
            foreach(var item in listMaMayBay)
            {
                listMayBay.Add(item + "(" + 
                    db.MayBays.Where(m => m.MaMayBay == item).FirstOrDefault().HangBay.TenHangBay + ")");
            }
            //Tìm tổng số ghế hạng thương gia có trong chuyến bay
            List<int> listTongGheThuongGia = new List<int>();
            foreach(var item in listMaMayBay)
            {
                listTongGheThuongGia.Add(
                    db.MayBays.Where(m => m.MaMayBay == item).FirstOrDefault().SoGheThuongGia
                    .GetValueOrDefault(0));
            }
            //Tìm tổng số ghế hạng nhất có trong chuyến bay
            List<int> listTongGheHangNhat = new List<int>();
            foreach (var item in listMaMayBay)
            {
                listTongGheHangNhat.Add(
                    db.MayBays.Where(m => m.MaMayBay == item).FirstOrDefault().SoGheHangNhat
                    .GetValueOrDefault(0));
            }
            //Tìm tổng số ghế hạng phổ thông đặt biệt có trong chuyến bay
            List<int> listTongGhePTDB = new List<int>();
            foreach (var item in listMaMayBay)
            {
                listTongGhePTDB.Add(
                    db.MayBays.Where(m => m.MaMayBay == item).FirstOrDefault().SoGhePhoThongDacBiet
                    .GetValueOrDefault(0));
            }
            //Tìm tổng số ghế hạng phổ thông có trong chuyến bay
            List<int> listTongGhePT = new List<int>();
            foreach (var item in listMaMayBay)
            {
                listTongGhePT.Add(
                    db.MayBays.Where(m => m.MaMayBay == item).FirstOrDefault().SoGhePhoThong
                    .GetValueOrDefault(0));
            }
            //Tìm số ghế hạng thương gia bán được trong chuyến bay
            List<int> listTongGheThuongGiaDaBan = new List<int>();
            foreach(var item in listMaChuyenBay)
            {
                //Danh sách vé thương gia có trong chuyến bay
                var ve = db.Ves.Where(v => (v.MaChuyenBay == item) && v.HangVe.Equals("Business"))
                    .ToList();
                int count = 0;
                foreach(var item1 in ve)
                {
                    count += item1.SoLuongGhe.GetValueOrDefault(0);
                }
                listTongGheThuongGiaDaBan.Add(count);
            }
            //Tìm số ghế hạng nhất bán được trong chuyến bay
            List<int> listTongGheHangNhatDaBan = new List<int>();
            foreach (var item in listMaChuyenBay)
            {
                //Danh sách vé hạng nhất có trong chuyến bay
                var ve = db.Ves.Where(v => (v.MaChuyenBay == item) && v.HangVe.Equals("First"))
                    .ToList();
                int count = 0;
                foreach (var item1 in ve)
                {
                    count += item1.SoLuongGhe.GetValueOrDefault(0);
                }
                listTongGheHangNhatDaBan.Add(count);
            }
            //Tìm số ghế hạng phổ thông đặt biệt đã bán được trong chuyến bay
            List<int> listTongGhePTDBDaBan = new List<int>();
            foreach (var item in listMaChuyenBay)
            {
                //Danh sách vé phổ thông đặt biệt có trong chuyến bay
                var ve = db.Ves.Where(v => (v.MaChuyenBay == item) && v.HangVe.Equals("Premium"))
                    .ToList();
                int count = 0;
                foreach (var item1 in ve)
                {
                    count += item1.SoLuongGhe.GetValueOrDefault(0);
                }
                listTongGhePTDBDaBan.Add(count);
            }
            //Tìm số ghế hạng phổ thông bán được trong chuyến bay
            List<int> listTongGhePTDaBan = new List<int>();
            foreach (var item in listMaChuyenBay)
            {
                //Danh sách vé phổ thông có trong chuyến bay
                var ve = db.Ves.Where(v => (v.MaChuyenBay == item) && v.HangVe.Equals("Economy"))
                    .ToList();
                int count = 0;
                foreach (var item1 in ve)
                {
                    count += item1.SoLuongGhe.GetValueOrDefault(0);
                }
                listTongGhePTDaBan.Add(count);
            }

            //Tìm tổng doanh thu của chuyến bay
            List<double> listDoanhThu = new List<double>();
            foreach(var item in listMaChuyenBay)
            {
                //Danh sách vé của chuyến bay
                var ve =  db.Ves.Where(v => v.MaChuyenBay == item).ToList();
                double count = 0;
                foreach (var item1 in ve)
                {
                    count += item1.TongTien.GetValueOrDefault(0);
                }
                listDoanhThu.Add(count);
            }

            List<long> listMaChuyenBayReturn = new List<long>();
            List<string> listDiemDiReturn = new List<string>();
            List<string> listDiemDenReturn = new List<string>();
            List<string> listMayBayReturn = new List<string>();
            List<int> listTongGheThuongGiaReturn = new List<int>();
            List<int> listTongGheHangNhatReturn = new List<int>();
            List<int> listTongGhePTDBReturn = new List<int>();
            List<int> listTongGhePTReturn = new List<int>();
            List<int> listTongGheThuongGiaDaBanReturn = new List<int>();
            List<int> listTongGheHangNhatDaBanReturn = new List<int>();
            List<int> listTongGhePTDBDaBanReturn = new List<int>();
            List<int> listTongGhePTDaBanReturn = new List<int>();
            List<double> listDoanhThuReturn = new List<double>();
            if(searchKey == "" && option == 0)
            {
                for(int i = 0; i < listMaChuyenBay.Count; i++)
                {
                    listMaChuyenBayReturn.Add(listMaChuyenBay[i]);
                    listDiemDiReturn.Add(listDiemDiString[i]);
                    listDiemDenReturn.Add(listDiemDenString[i]);
                    listMayBayReturn.Add(listMayBay[i]);
                    listTongGheThuongGiaReturn.Add(listTongGheThuongGia[i]);
                    listTongGheHangNhatReturn.Add(listTongGheHangNhat[i]);
                    listTongGhePTDBReturn.Add(listTongGhePTDB[i]);
                    listTongGhePTReturn.Add(listTongGhePT[i]);
                    listTongGheThuongGiaDaBanReturn.Add(listTongGheThuongGiaDaBan[i]);
                    listTongGheHangNhatDaBanReturn.Add(listTongGheHangNhatDaBan[i]);
                    listTongGhePTDBDaBanReturn.Add(listTongGhePTDBDaBan[i]);
                    listTongGhePTDaBanReturn.Add(listTongGhePTDaBan[i]);
                    listDoanhThuReturn.Add(listDoanhThu[i]);
                }
            }

            //Tìm kiếm bằng mã chuyến bay
            if (option == 1)
            {
                for (int i = 0; i < listMaChuyenBay.Count; i++)
                {
                    if(listMaChuyenBay[i] == Int64.Parse(searchKey))
                    {
                        listMaChuyenBayReturn.Add(listMaChuyenBay[i]);
                        listDiemDiReturn.Add(listDiemDiString[i]);
                        listDiemDenReturn.Add(listDiemDenString[i]);
                        listMayBayReturn.Add(listMayBay[i]);
                        listTongGheThuongGiaReturn.Add(listTongGheThuongGia[i]);
                        listTongGheHangNhatReturn.Add(listTongGheHangNhat[i]);
                        listTongGhePTDBReturn.Add(listTongGhePTDB[i]);
                        listTongGhePTReturn.Add(listTongGhePT[i]);
                        listTongGheThuongGiaDaBanReturn.Add(listTongGheThuongGiaDaBan[i]);
                        listTongGheHangNhatDaBanReturn.Add(listTongGheHangNhatDaBan[i]);
                        listTongGhePTDBDaBanReturn.Add(listTongGhePTDBDaBan[i]);
                        listTongGhePTDaBanReturn.Add(listTongGhePTDaBan[i]);
                        listDoanhThuReturn.Add(listDoanhThu[i]);
                    }
                }
            }

            //Tìm kiếm bằng điểm đi
            if (option == 2)
            {
                for (int i = 0; i < listDiemDiString.Count; i++)
                {
                    if (listDiemDiString[i].Equals(searchKey))
                    {
                        listMaChuyenBayReturn.Add(listMaChuyenBay[i]);
                        listDiemDiReturn.Add(listDiemDiString[i]);
                        listDiemDenReturn.Add(listDiemDenString[i]);
                        listMayBayReturn.Add(listMayBay[i]);
                        listTongGheThuongGiaReturn.Add(listTongGheThuongGia[i]);
                        listTongGheHangNhatReturn.Add(listTongGheHangNhat[i]);
                        listTongGhePTDBReturn.Add(listTongGhePTDB[i]);
                        listTongGhePTReturn.Add(listTongGhePT[i]);
                        listTongGheThuongGiaDaBanReturn.Add(listTongGheThuongGiaDaBan[i]);
                        listTongGheHangNhatDaBanReturn.Add(listTongGheHangNhatDaBan[i]);
                        listTongGhePTDBDaBanReturn.Add(listTongGhePTDBDaBan[i]);
                        listTongGhePTDaBanReturn.Add(listTongGhePTDaBan[i]);
                        listDoanhThuReturn.Add(listDoanhThu[i]);
                    }
                }
            }

            //Tìm kiếm bằng điểm đến
            if (option == 3)
            {
                for (int i = 0; i < listDiemDenString.Count; i++)
                {
                    if (listDiemDenString[i].Equals(searchKey))
                    {
                        listMaChuyenBayReturn.Add(listMaChuyenBay[i]);
                        listDiemDiReturn.Add(listDiemDiString[i]);
                        listDiemDenReturn.Add(listDiemDenString[i]);
                        listMayBayReturn.Add(listMayBay[i]);
                        listTongGheThuongGiaReturn.Add(listTongGheThuongGia[i]);
                        listTongGheHangNhatReturn.Add(listTongGheHangNhat[i]);
                        listTongGhePTDBReturn.Add(listTongGhePTDB[i]);
                        listTongGhePTReturn.Add(listTongGhePT[i]);
                        listTongGheThuongGiaDaBanReturn.Add(listTongGheThuongGiaDaBan[i]);
                        listTongGheHangNhatDaBanReturn.Add(listTongGheHangNhatDaBan[i]);
                        listTongGhePTDBDaBanReturn.Add(listTongGhePTDBDaBan[i]);
                        listTongGhePTDaBanReturn.Add(listTongGhePTDaBan[i]);
                        listDoanhThuReturn.Add(listDoanhThu[i]);
                    }
                }
            }

            //Tìm kiếm bằng máy bay
            if (option == 4)
            {
                for (int i = 0; i < listMayBay.Count; i++)
                {
                    if (listMayBay[i].Contains(searchKey))
                    {
                        listMaChuyenBayReturn.Add(listMaChuyenBay[i]);
                        listDiemDiReturn.Add(listDiemDiString[i]);
                        listDiemDenReturn.Add(listDiemDenString[i]);
                        listMayBayReturn.Add(listMayBay[i]);
                        listTongGheThuongGiaReturn.Add(listTongGheThuongGia[i]);
                        listTongGheHangNhatReturn.Add(listTongGheHangNhat[i]);
                        listTongGhePTDBReturn.Add(listTongGhePTDB[i]);
                        listTongGhePTReturn.Add(listTongGhePT[i]);
                        listTongGheThuongGiaDaBanReturn.Add(listTongGheThuongGiaDaBan[i]);
                        listTongGheHangNhatDaBanReturn.Add(listTongGheHangNhatDaBan[i]);
                        listTongGhePTDBDaBanReturn.Add(listTongGhePTDBDaBan[i]);
                        listTongGhePTDaBanReturn.Add(listTongGhePTDaBan[i]);
                        listDoanhThuReturn.Add(listDoanhThu[i]);
                    }
                }
            }

            ViewBag.MaChuyenBay = listMaChuyenBayReturn;
            ViewBag.DiemDi = listDiemDiReturn;
            ViewBag.DiemDen = listDiemDenReturn;
            ViewBag.MayBay = listMayBayReturn;
            ViewBag.TongGheThuongGia = listTongGheThuongGiaReturn;
            ViewBag.TongGheHangNhat = listTongGheHangNhatReturn;
            ViewBag.TongGhePTDB = listTongGhePTDBReturn;
            ViewBag.TongGhePT = listTongGhePTReturn;
            ViewBag.ThuongGiaDB = listTongGheThuongGiaDaBanReturn;
            ViewBag.HangNhatDB = listTongGheHangNhatDaBanReturn;
            ViewBag.PTDBDB = listTongGhePTDBDaBanReturn;
            ViewBag.PTDB = listTongGhePTDaBanReturn;
            ViewBag.DoanhThu = listDoanhThuReturn;

            return View();
        }

        public ActionResult Search(string searchKey, int option)
        {
            return RedirectToAction("Index", new { searchKey = searchKey, option = option });
        }
    }
}