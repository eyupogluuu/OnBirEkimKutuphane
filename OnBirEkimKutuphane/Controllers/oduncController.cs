using OnBirEkimKutuphane.Models.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBirEkimKutuphane.Controllers
{
    [Authorize]
    public class oduncController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        public ActionResult oduncliste()
        {
            var oduncler = db.odunc.ToList();
            return View(oduncler);
        }

        [HttpGet]
        public ActionResult yeniodunc()
        {
            var sorgu1 = (from x in db.kitap.ToList()
                          select new SelectListItem
                          {
                              Text = x.kitapAdi,
                              Value = x.kitapID.ToString()
                          }
                        ).ToList();
            ViewBag.ktp = sorgu1;

            var sorgu2 = (from x in db.uye.ToList()
                          select new SelectListItem
                          {
                              Text = x.uyeAdiSoyadi,
                              Value = x.uyeID.ToString()
                          }
                        ).ToList();
            ViewBag.uye = sorgu2;

            var sorgu3 = (from x in db.personel.ToList()
                          select new SelectListItem
                          {
                              Text = x.personelAdiSoyadi,
                              Value = x.personelID.ToString()
                          }
                        ).ToList();
            ViewBag.prs = sorgu3;
            return View();
        }

        [HttpPost]
        public ActionResult yeniodunc(odunc o)
        {
            o.verilmetarihi = DateTime.Now;
            db.odunc.Add(o);
            db.SaveChanges();
            return RedirectToAction("oduncliste", "odunc");
        }
    }
}