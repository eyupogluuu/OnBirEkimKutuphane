using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBirEkimKutuphane.Controllers
{
    public class anasayfaController : Controller
    {
        // GET: anasayfa
        [Authorize]
        public ActionResult ilksayfa()
        {
            return View();
        }
    }
}