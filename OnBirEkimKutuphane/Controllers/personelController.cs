using OnBirEkimKutuphane.Models.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBirEkimKutuphane.Controllers
{
    [Authorize]
    public class personelController : Controller
    {

        KutuphaneDBEntities db = new KutuphaneDBEntities();
        public ActionResult personelliste()
        {
            var personeller = db.personel.ToList();
            return View(personeller);
        }
        [HttpGet]
        public ActionResult yeniper()
        {
            return View();
        }

        [HttpPost]
        public ActionResult yeniper(personel p)
        {
            db.personel.Add(p);
            db.SaveChanges();
            return RedirectToAction("personelliste","personel");
        }

        public ActionResult personelsil(int id)
        {
            var silpersonel = db.personel.Find(id);
            db.personel.Remove(silpersonel);
            db.SaveChanges();
            return RedirectToAction("personelliste", "personel");
        }
        public ActionResult perbul(int id)
        {
            var gunpersonel = db.personel.Find(id);
            return View("perbul", gunpersonel);
        }

        public ActionResult personelgun(personel p)
        {
            var gunpersonel = db.personel.Find(p.personelID);
            gunpersonel.personelAdiSoyadi = p.personelAdiSoyadi;
            db.SaveChanges();
            return RedirectToAction("personelliste", "personel");
        }
    }
}