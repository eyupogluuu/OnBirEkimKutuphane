using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnBirEkimKutuphane.Models.entity;
using System.Web.Helpers;

namespace OnBirEkimKutuphane.Controllers
{
    public class hesapController : Controller
    {
        KutuphaneDBEntities db = new KutuphaneDBEntities();
        private object crypto;

        [HttpGet]
        public ActionResult girisyap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult girisyap(admin a)
        {
            var admin = db.admin.FirstOrDefault(x => x.email == a.email && x.sifre == a.sifre);
            if (admin != null)
            {
                FormsAuthentication.SetAuthCookie(admin.email, false);
                Session["Email"] = admin.email.ToString();
                Session["AdSoyad"] = admin.adSoyad.ToString();
                return RedirectToAction("ilksayfa", "anasayfa");
            }
            else
            {
                Response.Write("Hatalı Kullanıcı Adı veya Şifre Girişi Yaptınız");
            }

            return View();
        }

        public ActionResult adminliste()
        {
            var adminler = db.admin.ToList();
            return View(adminler);
        }

        [HttpGet]
        public ActionResult yeniadmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult yeniadmin(admin a)
        {
            db.admin.Add(a);
            a.rol = 2;
            db.SaveChanges();
            return RedirectToAction("girisyap", "hesap");
        }

        public ActionResult cikisyap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("girisyap", "hesap");
        }

        public ActionResult guncelle()
        {
            var kullanici = (string)Session["Email"];
            var deger = db.admin.FirstOrDefault(x => x.email == kullanici);
            return View(deger);
        }
        [HttpPost]
        public ActionResult guncelle(admin a)
        {
            var kullanici = (string)Session["Email"];
            var user = db.admin.Where(x => x.email == kullanici).FirstOrDefault();
            user.adSoyad = a.adSoyad;
            user.email = a.email;
            user.sifre = a.sifre;
            //user.rol = a.rol;
            db.SaveChanges();
            return RedirectToAction("ilksayfa", "anasayfa");
        }
        public ActionResult sifrereset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sifrereset(string eposta)
        {
            var kisi = db.admin.Where(x => x.email == eposta).SingleOrDefault();//single or default tek bir değer döndür demek
            if (kisi!=null)
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();
                admin sifre = new admin();
                kisi.sifre = Crypto.Hash(Convert.ToString(yenisifre), "MD5");
                db.SaveChanges();
                WebMail.SmtpServer = "smtp.gmail.com";//gmailin kendi smtpsi
                WebMail.EnableSsl = true;
                WebMail.UserName = "onureyupoglu95@gmail.com";
                WebMail.Password = "onurbey";
                WebMail.SmtpPort = 587;//gmailin kendi portu
                WebMail.Send(eposta, "Giriş Şifreniz", "şifreniz:" + yenisifre);
                ViewBag.uyarı = "Şİfreniz Başarıyla Değiştirilmiştir";

            }
            else
            {
                ViewBag.uyari = "Hata Oluştu Tekrar Deneyiniz";
            }
            return View();
        }
    }
}