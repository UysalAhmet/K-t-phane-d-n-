using Akınsoft_Kutuphane_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Akınsoft_Kutuphane_2.Controllers
{
    public class LoginController : Controller
    {
        Akinsoft_KutuphaneEntities db = new Akinsoft_KutuphaneEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginForm(Kullanici k)
        {
            string userName = k.KullaniciAd;
            string password = k.KullaniciSifre;
            Kullanici user = db.Kullanici.FirstOrDefault(x => x.KullaniciAd == userName);
            if (user != null && (user.KullaniciAd == userName && user.KullaniciSifre == password))
            {
                Session["Login"] = user.KullaniciAd;
                return Redirect("/Home/Index");
            }
            ViewBag.mesaj = "Kullanıcı adı veya parola hatalı";
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session["Login"] = null;
            return Redirect("/Login/Index");
        }
    }
}