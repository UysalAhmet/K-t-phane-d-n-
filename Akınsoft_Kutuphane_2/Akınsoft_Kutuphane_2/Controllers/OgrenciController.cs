using Akınsoft_Kutuphane_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Akınsoft_Kutuphane_2.Controllers
{
    public class OgrenciController : BaseController
    {
        // GET: Ogrenci
        Akınsoft_Kutuphane_2.Models.Akinsoft_KutuphaneEntities db = new Models.Akinsoft_KutuphaneEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OgrenciTablo()
        {
            var Ogrenci = db.Ogrenci.ToList();
            return View(Ogrenci);

        }
        public string Adres(int id)
        {
            var adres = db.Ogrenci.Find(id).Adres;
            return adres;
        }
        public string MemleketAdres(int id)
        {
            var adres = db.Ogrenci.Find(id).MemleketAdres;
            return adres;
        }
        public ActionResult OgrenciEkle(int ? id)
        {
            ViewBag.Fakulteler = db.Fakulte.ToList();
            ViewBag.Bolumler = db.Bolum.ToList();
            if (id == null)
            {
                return View(new Ogrenci());
            }
            else
            {
               
               var Ogrenci = db.Ogrenci.Find(id);
                ViewBag.Bolumler = db.Bolum.Where(x => x.FakulteId == Ogrenci.FakulteId);
                return View(Ogrenci);
            }

        }
        public JsonResult BolumGetir(int id)
        {
            return Json(new SelectList(db.Bolum.Where(empt => (empt.FakulteId == id)), "BolumId", "BolumAd"));
        }
        [HttpPost]
        public JsonResult OgrenciEkle(Ogrenci k)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                if (k.OgrenciIId != 0)
                {
                    k.KayitTarihi = DateTime.Now;
                    db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    k.KayitTarihi = DateTime.Now;
                    db.Ogrenci.Add(k);
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
        [HttpPost]
        public JsonResult OgrenciSil(int id)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                db.Ogrenci.Remove(db.Ogrenci.Find(id));
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


        //Fakulte İşlemleri\\
        public ActionResult FakulteTablo()
        {
            var Fakulte = db.Fakulte.ToList();
            return View(Fakulte);
        }
        public ActionResult FakulteEkle(int? id)
        {
            if (id == null)
            {
                return View(new Fakulte());
            }
            else
            {

                var Fakulte = db.Fakulte.Find(id);
                
                return View(Fakulte);
            }

        }
        [HttpPost]
        public JsonResult FakulteEkle(Fakulte k)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                if (k.FakulteId != 0)
                {
                    db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Fakulte.Add(k);
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
        [HttpPost]
        public JsonResult FakulteSil(int id)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                db.Fakulte.Remove(db.Fakulte.Find(id));
                db.SaveChanges();
                json.Durum = true;
                json.Mesaj = "Silme İşlemi Başarılı";
            }
            catch (Exception)
            {
                json.Mesaj = "Silme İşleminde Hata (Bağlı Bir Veriyi Silmeye Çalışıyor Olabilirsiniz)";
                json.Durum = false;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //Bölüm İşlemleri\\
        public ActionResult BolumTablo()
        {
            var Bolum = db.Bolum.ToList();
            return View(Bolum);
        }
        public ActionResult BolumEkle(int? id)
        {
            ViewBag.Fakulte = db.Fakulte.ToList();

            if (id == null)
            {
                return View(new Bolum());
            }
            else
            {

                var Bolum = db.Bolum.Find(id);

                return View(Bolum);
            }

        }
        [HttpPost]
        public JsonResult BolumEkle(Bolum k)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                if (k.BolumId != 0)
                {
                    db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Bolum.Add(k);
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
        [HttpPost]
        public JsonResult BolumSil(int id)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                db.Bolum.Remove(db.Bolum.Find(id));
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

    }
}