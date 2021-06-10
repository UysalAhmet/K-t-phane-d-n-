using Akınsoft_Kutuphane_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Akınsoft_Kutuphane_2.Controllers
{
    public class OduncTeslimController : BaseController
    {
        // GET: OduncTeslim
        Akınsoft_Kutuphane_2.Models.Akinsoft_KutuphaneEntities db = new Models.Akinsoft_KutuphaneEntities();
        public ActionResult Index()
        {
            ViewBag.Ogrenci = db.Ogrenci.ToList();
            ViewBag.Kitap = db.Kitap.Where(x => x.StokDurum == true).ToList();
            return View();
        }
        public JsonResult KontrolEt(int id)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                var Odunclist = db.OduncTeslim.Where(c => c.OgrenciId == id && c.TeslimTarihi == null).ToList();
                if (Odunclist == null || Odunclist.Count == 0)
                {
                    json.Mesaj = "Ogrencinin Elinde Herhangi Bir Kitap Bulunmamıştır Ödünç Kitap Almaya Uygundur";
                    json.Durum = true;
                }
                else
                {
                    json.Mesaj = "Ogrencinin Elinde:</br>";
                    foreach (var item in Odunclist)
                    {
                        json.Mesaj += "->" + item.Kitap.KitapAd + "</br>";
                    }
                    json.Mesaj += "Kitapları Mevcuttur";
                    json.Durum = true;
                }

            }
            catch (Exception)
            {
                json.Durum = false;
                json.Mesaj = "Başarısız";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OduncEkle(OduncTeslim ot)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                ot.OduncTarihi = DateTime.Now;

                db.OduncTeslim.Add(ot);
                db.SaveChanges();
                Kitap k = db.Kitap.Find(ot.KitapId);
                k.StokDurum = false;
                db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                json.Mesaj = "Ödünç Verilme İşlemi Başarılı";
                json.Durum = true;
            }
            catch (Exception)
            {

                json.Durum = false;
                json.Mesaj = "Başarısız";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OgrenciKitaplari(int id)
        {
            var list = db.OduncTeslim.Where(empt => (empt.OgrenciId == id) && empt.TeslimTarihi == null).ToList();
            var kitaplar = new List<Kitap>();
            foreach (var item in list)
            {
                var kitap = db.Kitap.Find(item.KitapId);
                kitap.TamAd = kitap.KitapAd + "" + " - " + kitap.Yazar.YazarAdSoyad + " -" + " " + kitap.ISBNNo;
                kitaplar.Add(kitap);
                
            }
            return Json(new SelectList(kitaplar, "KitapId", "TamAd"));
        }
        //[HttpPost]
        //public JsonResult TeslimAl(OduncTeslim  ot)
        //{
        //    KontrolJson json = new KontrolJson();
        //    try
        //    {
        //        OduncTeslim teslim = db.OduncTeslim.FirstOrDefault(x => x.OgrenciId == ot.OgrenciId && x.KitapId == ot.KitapId && x.TeslimTarihi == null);
        //        teslim.TeslimTarihi = DateTime.Now;
        //        db.Entry(teslim).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        Kitap k = db.Kitap.Find(ot.KitapId);
        //        k.StokDurum = true;
        //        db.Entry(k).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        json.Mesaj = "Teslim Alma İşlemi Başarılı";
        //        json.Durum = true;
        //    }
        //    catch (Exception)
        //    {

        //        json.Durum = false;
        //        json.Mesaj = "Başarısız";
        //    }
        //    return Json(json, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult TeslimAll(int id)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                var Teslim = db.OduncTeslim.Find(id);
                Teslim.TeslimTarihi = DateTime.Now;
                db.Entry(Teslim).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Kitap k = db.Kitap.Find(Teslim.KitapId);
                k.StokDurum = true;
                db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                json.Mesaj = "Teslim Alma İşlemi Başarılı";
                json.Durum = true;

            }
            catch (Exception)
            {

                json.Durum = false;
                json.Mesaj = "Başarısız";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Odunc()
        {
            ViewBag.Ogrenci = db.Ogrenci.ToList();
            ViewBag.Kitap = db.Kitap.Where(x => x.StokDurum == true).ToList();
            return View();
        }
        public JsonResult OduncKitapYenile()
        {
            var Kitaplar = db.Kitap.Where(x => x.StokDurum == true).ToList();
            foreach (var item in Kitaplar)
            {
                item.TamAd = item.KitapAd + "" + " - " + item.Yazar.YazarAdSoyad + " -" + " " + item.ISBNNo;
            }
            return Json(new SelectList(Kitaplar, "KitapId", "TamAd"));
        }
        public ActionResult OduncTeslimTablo()
        {
            var ot = db.OduncTeslim.ToList();
            return View(ot);
        }
        public JsonResult TeslimIptal(int id)
        {
            KontrolJson json = new KontrolJson();
            try
            {
                var Teslim = db.OduncTeslim.Find(id);
                Teslim.TeslimTarihi = null;
                db.Entry(Teslim).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Kitap k = db.Kitap.Find(Teslim.KitapId);
                k.StokDurum = false;
                db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                json.Mesaj = "Teslim Alma İşlemi İptal Edildi";
                json.Durum = true;

            }
            catch (Exception)
            {

                json.Durum = false;
                json.Mesaj = "Başarısız";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}