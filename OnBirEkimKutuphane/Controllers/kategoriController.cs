using OnBirEkimKutuphane.Models.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBirEkimKutuphane.Controllers
{
    [Authorize]
    public class kategoriController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();

        public ActionResult kategoriliste()
        {
            var kategoriler = db.kategori.ToList();
            return View(kategoriler);
        }

        [HttpGet]
        public ActionResult yenikategori()
        {

            return View();
        }
        [HttpPost]
        public ActionResult yenikategori(kategori k)
        {
            db.kategori.Add(k);
            db.SaveChanges();
            return RedirectToAction("kategoriliste", "kategori");
        }
        public ActionResult kategorisil(int id)
        {
            var silkate = db.kategori.Find(id);
            db.kategori.Remove(silkate);
            db.SaveChanges();
            return RedirectToAction("kategoriliste", "kategori");
        }

        public ActionResult kategorigetir(int id)
        {
            var kate = db.kategori.Find(id);
            return View("kategorigetir", kate);
        }
        public ActionResult kategoriguncelle(kategori k)
        {
            var gunkate = db.kategori.Find(k.kategoriID);
            gunkate.kategoriAdi = k.kategoriAdi;
            db.SaveChanges();

            return RedirectToAction("kategoriliste", "kategori");
        }
        public ActionResult kategorieser(int id)
        {
            var eser = db.kitap.Where(x => x.kategori1.kategoriID == id).ToList();
            return View(eser);
        }

    }
}