using Akınsoft_Kutuphane_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Akınsoft_Kutuphane_2.Controllers
{
    public class KitapController : BaseController
    {
        // GET: Kitap
        Akinsoft_KutuphaneEntities db = new Akinsoft_KutuphaneEntities();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult KitapTablo()
        {
            var Kitaplar = db.Kitap.ToList();
            return View(Kitaplar);
        }
        public ActionResult Ekle(int? id)
        {
            ViewBag.kategori = db.Kategori.ToList();
            ViewBag.yazar = db.Yazar.ToList();
            if (id == null)
            {
                return View(new Kitap());
            }
            else
            {
                var kitap = db.Kitap.Find(id);
                return View(kitap);
            }

        }
        [HttpPost]
        public JsonResult Sil(int id)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                db.Kitap.Remove(db.Kitap.Find(id));
                db.SaveChanges();
                json.Durum = true;
                json.Mesaj = "Silme İşlemi Başarılı";
            }
            catch (Exception)
            {
                json.Mesaj = "Ekleme İşleminde Hata";
                json.Durum = false;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Ekle(Kitap k)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                if (k.KitapId != 0)
                {
                    db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Kitap.Add(k);
                    db.SaveChanges();
                }

                json.Mesaj = "Ekleme İşlemi Başarılı";
                json.Durum = true;
            }
            catch (Exception)
            {
                json.Mesaj = "Ekleme İşleminde Hata";
                json.Durum = false;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        //Kategori İşlemleri\\
        public ActionResult KategoriEkle(int ? id)
        {
            if (id == null)
            {
                return View(new Kategori());
            }
            else
            {
                var kategori = db.Kategori.Find(id);
                return View(kategori);
            }
        }
        [HttpPost]
        public JsonResult KategoriSil(int id)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                db.Kategori.Remove(db.Kategori.Find(id));
                db.SaveChanges();
                json.Durum = true;
                json.Mesaj = "Silme İşlemi Başarılı";
            }
            catch (Exception)
            {
                json.Mesaj = "Ekleme İşleminde Hata";
                json.Durum = false;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult KategoriEkle(Kategori k)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                if (k.KategoriId != 0)
                {
                    db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {

                    db.Kategori.Add(k);
                    db.SaveChanges();
                }

                json.Mesaj = "Ekleme İşlemi Başarılı";
                json.Durum = true;
            }
            catch (Exception ex)
            {
                json.Mesaj = ex.Message;
                json.Durum = false;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult KategoriTablo()
        {
            var Kategori = db.Kategori.ToList();
            return View(Kategori);
        }

        //Yazar İşlemleri\\
        public ActionResult YazarEkle(int? id)
        {
            if (id == null)
            {
                return View(new Yazar());
            }
            else
            {
                var yazar = db.Yazar.Find(id);
                return View(yazar);
            }
        }
        [HttpPost]
        public JsonResult YazarSil(int id)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                db.Yazar.Remove(db.Yazar.Find(id));
                db.SaveChanges();
                json.Durum = true;
                json.Mesaj = "Silme İşlemi Başarılı";
            }
            catch (Exception)
            {
                json.Mesaj = "Ekleme İşleminde Hata";
                json.Durum = false;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }


       
       
        [HttpPost]
        public JsonResult YazarEkle(Yazar k)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                if (k.YazarId != 0)
                {
                    db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {

                    db.Yazar.Add(k);
                    db.SaveChanges();
                }

                json.Mesaj = "Ekleme İşlemi Başarılı";
                json.Durum = true;
            }
            catch (Exception ex)
            {
                json.Mesaj = ex.Message;
                json.Durum = false;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult YazarTablo()
        {
            var Yazar = db.Yazar.ToList();
            return View(Yazar);
        }
       
    }
}