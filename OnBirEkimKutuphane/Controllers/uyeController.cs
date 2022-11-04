using OnBirEkimKutuphane.Models.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBirEkimKutuphane.Controllers
{
    [Authorize]
    public class uyeController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        public ActionResult uyeliste()
        {
            var uyeler = db.uye.ToList();
            return View(uyeler);
        }
        [HttpGet]
        public ActionResult yeniuye()
        {
            return View();
        }
        [HttpPost]
        public ActionResult yeniuye(uye u)
        {
            db.uye.Add(u); 
            db.SaveChanges();
            return RedirectToAction("uyeliste","uye");
        }

        public ActionResult uyesil(int id)
        {
            var siluye = db.uye.Find(id);
            db.uye.Remove(siluye);
            db.SaveChanges();
            return RedirectToAction("uyeliste", "uye");
        }
        public ActionResult uyegetir(int id)
        {
            var uyem = db.uye.Find(id);
            return View("uyegetir", uyem);
        }

        public ActionResult uyeguncelle(uye u)
        {
            var gunuye = db.uye.Find(u.uyeID);
            gunuye.uyeAdiSoyadi = u.uyeAdiSoyadi;
            gunuye.uyeCinsiyet = u.uyeCinsiyet;
            gunuye.uyeDogumTarihi = u.uyeDogumTarihi;
            gunuye.uyeAdres = u.uyeAdres;
            gunuye.uyePuan = u.uyePuan;
            gunuye.uyeRol = u.uyeRol;
            db.SaveChanges();
            return RedirectToAction("uyeliste","uye");
        }

       
    }
}