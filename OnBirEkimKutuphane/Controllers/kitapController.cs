using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnBirEkimKutuphane.Models.entity;

namespace OnBirEkimKutuphane.Controllers
{
    [Authorize]
    public class kitapController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        
        public ActionResult kitapliste(string ara)
        {
            var kitaplar = from k in db.kitap select k;
            if (!string.IsNullOrEmpty(ara))
            {
                kitaplar = kitaplar.Where(x => x.kitapAdi.Contains(ara)||x.yazar.yazarAdi.Contains(ara));
            }
            return View(kitaplar.ToList());
            //var liste = db.kitap.ToList();
            //if (!string.IsNullOrEmpty(ara))
            //{
            //    liste = liste.Where(x => x.kitapAdi.Contains(ara) || x.yazar.yazarAdi.Contains(ara)).ToList();//arama işlemi
            //}
            //return View(liste);
        }

        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult yenikitap()
        {
            var sorgu1 = (from x in db.kategori.ToList()
                          select new SelectListItem
                          {
                              Text = x.kategoriAdi,
                              Value = x.kategoriID.ToString()
                          }
                        ).ToList();
            ViewBag.ktgr = sorgu1;

            var sorgu2 = (from x in db.yazar.ToList()
                          select new SelectListItem
                          {
                              Text = x.yazarAdi + x.yazarSoyadi,
                              Value = x.yazarID.ToString()
                          }
                        ).ToList();
            ViewBag.yzr = sorgu2;


            return View();
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult yenikitap(kitap k, HttpPostedFileBase File)
        {
            string path = Path.Combine("~/Content/image/" + File.FileName);
            File.SaveAs(Server.MapPath(path));
            k.gorsel = File.FileName.ToString();
            db.kitap.Add(k);
            k.durum = true;
            db.SaveChanges();

            return RedirectToAction("kitapliste", "kitap");
        }

        [Authorize(Roles = "A")]
        public ActionResult kitapsil(int id)
        {
            var silkitap = db.kitap.Find(id);
            db.kitap.Remove(silkitap);
            db.SaveChanges();
            return RedirectToAction("kitapliste", "kitap");
        }

        [Authorize(Roles = "A")]
        public ActionResult kitapgetir(int id)
        {
            var sorgu1 = (from x in db.kategori.ToList()
                          select new SelectListItem
                          {
                              Text = x.kategoriAdi,
                              Value = x.kategoriID.ToString()
                          }
                        ).ToList();
            ViewBag.ktgr = sorgu1;

            var sorgu2 = (from x in db.yazar.ToList()
                          select new SelectListItem
                          {
                              Text = x.yazarAdi + "  " + x.yazarSoyadi,
                              Value = x.yazarID.ToString()
                          }
                        ).ToList();
            ViewBag.yzr = sorgu2;
            var getirkitap = db.kitap.Find(id);
            return View("kitapgetir", getirkitap);
        }
        //not!! kitapgetir sayfasına hiddenfor tanımlanmazsa güncelleme işlemi null döner yapmaz (nesne başvurusu hatası verir)
        [Authorize(Roles = "A")]
        public ActionResult kitapguncelle(kitap k, HttpPostedFileBase File)
        {
            var gunkitap = db.kitap.Find(k.kitapID);
            if (File == null)//resim kısmını boş geçtiysem sadecediğerlerini güncelle geç
            {
                gunkitap.kitapAdi = k.kitapAdi;
                gunkitap.yazari = k.yazari;
                gunkitap.kategori = k.kategori;
                gunkitap.durum = k.durum;
                db.SaveChanges();
                return RedirectToAction("kitapliste", "kitap");
            }
            else//resimle beraber güncelle 
            {
                gunkitap.kitapAdi = k.kitapAdi;
                gunkitap.yazari = k.yazari;
                gunkitap.kategori = k.kategori;
                gunkitap.gorsel = File.FileName.ToString();
                gunkitap.durum = k.durum;
                db.SaveChanges();
                return RedirectToAction("kitapliste", "kitap");
            }

        }


    }
}