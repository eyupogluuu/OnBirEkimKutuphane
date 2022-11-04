using OnBirEkimKutuphane.Models.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBirEkimKutuphane.Controllers
{
    [Authorize]
    public class kartlarController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        public ActionResult istatistikler()
        {
            var toplamkitap = db.kitap.Count().ToString();
            ViewBag.d1 = toplamkitap;

            var toplamuye = db.uye.Count().ToString();
            ViewBag.d2 = toplamuye;

            var personel = db.personel.Count().ToString();
            ViewBag.d3 = personel;

            var odunc = db.odunc.Count().ToString();
            ViewBag.d4 = odunc;

            var iade = db.iade.Count().ToString();
            ViewBag.d5 = iade;

            var yazar = db.yazar.Count().ToString();
            ViewBag.d6 = yazar;

            var kate = db.kategori.Count().ToString();
            ViewBag.d7 = kate;
            return View();
        }
    }
}