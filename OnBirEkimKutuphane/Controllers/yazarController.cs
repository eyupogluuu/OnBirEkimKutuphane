using OnBirEkimKutuphane.Models.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBirEkimKutuphane.Controllers
{
    [Authorize]
    public class yazarController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        
        public ActionResult yazarliste()
        {
            var yazarlar = db.yazar.ToList();
            return View(yazarlar);
        }

        [HttpGet]
        public ActionResult yeniyazar()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult yeniyazar(yazar y)
        {
            db.yazar.Add(y);
            db.SaveChanges();
            return RedirectToAction("yazarliste", "yazar");
        }

        public ActionResult yazareser(int id)
        {
            var eser = db.kitap.Where(x => x.yazar.yazarID == id).ToList();
            return PartialView(eser);
        }
        public ActionResult yazarsil(int id)
        {
            var silyazar = db.yazar.Find(id);
            db.yazar.Remove(silyazar);
            db.SaveChanges();
            return RedirectToAction("yazarliste", "yazar");
        }
        public ActionResult yazargetir(int id)
        {
            var yazar = db.yazar.Find(id);
            return View("yazargetir", yazar);
        }
        public ActionResult yazarguncelle(yazar y)
        {
            var gunyazar = db.yazar.Find(y.yazarID);
            gunyazar.yazarAdi = y.yazarAdi;
            gunyazar.yazarSoyadi = y.yazarSoyadi;
            db.SaveChanges();
            return RedirectToAction("yazarliste", "yazar");
        }
    }
}