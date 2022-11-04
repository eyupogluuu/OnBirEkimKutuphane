using OnBirEkimKutuphane.Models.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBirEkimKutuphane.Controllers
{
    [Authorize]
    public class iadeController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        public ActionResult iadeliste()
        {
            var iadeler = db.iade.ToList();
            return View(iadeler);
        }
        [HttpGet]
        public ActionResult yeniiade()
        {
            var sorgu1 = (from x in db.kitap.ToList()
                          select new SelectListItem
                          {
                              Text = x.kitapAdi,
                              Value = x.kitapID.ToString()
                          }
                        ).ToList();
            ViewBag.ktplr = sorgu1;

            var sorgu2 = (from x in db.uye.ToList()
                          select new SelectListItem
                          {
                              Text = x.uyeAdiSoyadi,
                              Value = x.uyeID.ToString()
                          }
                        ).ToList();
            ViewBag.uyelr = sorgu2;

            var sorgu3 = (from x in db.personel.ToList()
                          select new SelectListItem
                          {
                              Text = x.personelAdiSoyadi,
                              Value = x.personelID.ToString()
                          }
                        ).ToList();
            ViewBag.prslr = sorgu3;
            return View();
            
        }

        [HttpPost]
        public ActionResult yeniiade(iade i)
        {
            db.iade.Add(i);
            i.iadetarihi = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("iadeliste","iade");
        }
      
    }
}