using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDatVe.Models;

namespace WebsiteDatVe.Controllers
{
    public class UserController : Controller
    {

        private DatVeDB db = new DatVeDB();

        public ActionResult Login()
        {
            return PartialView();
        }

        //login
        public JsonResult CheckAccount(string email, string password)
        {
            try
            {
                Boolean thanhcong = false;
                TaiKhoan user = (from u in db.TaiKhoans where u.Email == email && u.MatKhau == password select u).FirstOrDefault();         
                if(user != null)
                {
                    thanhcong = true;
                    Session["TaiKhoan"] = user;
                }
                return Json(new { code = 200, user = user, thanhcong = thanhcong }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Đã lưu
        public ActionResult Saved()
        {
            return View();
        }

        //Lịch sử đặt vé
        public ActionResult MyBooking()
        {
            TaiKhoan taikhoan = (TaiKhoan)Session["TaiKhoan"];
            List<Ve> lstVe = (from v in db.Ves where v.MaTaiKhoan == taikhoan.MaTaiKhoan orderby v.NgayDat descending select v).ToList();
            return View(lstVe);
        }

        //Đăng nhập Facebook
        public Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult loginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppID"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                respone_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppID"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,picture,id,email,phone");
                string id = me.id;
                string email = me.email;
                string firstname = me.first_name;
                string lastname = me.last_name;
                string middlename = me.middle_name;
                string phone = me.phone;
                var hinh = me.picture;

                var user = new TaiKhoan();
                user.MatKhau = id;
                user.Email = email;
                user.SDT = phone;
                //var a = GetMD5(email).ToString();
                //user.Password = email;
                //user.NgayThamGia = DateTime.Now;
                user.HoTen = lastname + " " + middlename + " " + firstname;
                user.Quyen = "0";
                

                var resultInsert = (InsertLoginFacebook(user).ToString());
                if (resultInsert != null)
                {
                    //var tv = (from c in db.TaiKhoans where c.Username == user.Username && c.Password== user.Password select c).FirstOrDefault();
                    var tv = (from c in db.TaiKhoans where c.Email == user.Email select c).FirstOrDefault();
                    if (tv != null)
                    {
                        Session["TaiKhoan"] = tv;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        public string InsertLoginFacebook(TaiKhoan enty)
        {
            var user = db.TaiKhoans.SingleOrDefault(n => n.Email == enty.Email);
            if (user == null)
            {
                db.TaiKhoans.Add(enty);
                db.SaveChanges();
                return enty.Email;
            }

            return user.Email;

        }

        //login google
        public JsonResult GoogleLogin(string TenNguoiDung, string Email)
        {
            try
            {
                var taikhoan = (from c in db.TaiKhoans where c.Email == Email select c).FirstOrDefault();
                if (taikhoan != null)
                {
                    Session["TaiKhoan"] = taikhoan;
                }
                else
                {
                    TaiKhoan tk = new TaiKhoan();
                    tk.Email = Email;
                    tk.HoTen = TenNguoiDung;
                    db.TaiKhoans.Add(tk);
                    db.SaveChanges();
                    Session["TaiKhoan"] = tk;
                }

                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}