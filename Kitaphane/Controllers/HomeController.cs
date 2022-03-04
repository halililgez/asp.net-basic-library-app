using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kitaphane.Models.Entity;

namespace Kitaphane.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        MvcKitapEntities1 db = new MvcKitapEntities1();

        public ActionResult Index()
        {
            
            return View(db.TBLKITAP.ToList());
        }

        public ActionResult Detay(int id)
        {
            kitapvecheck degerler = new kitapvecheck();
            degerler.tblkitaplar = db.TBLKITAP.Find(id);

            if(degerler.tblkitaplar.kitapcheckid == null)
            {
                degerler.tblcheck.islemkisi = 0;
                degerler.tblcheck.islemtarih = DateTime.Now.Date;
            }
            else
            {
                degerler.tblcheck = db.TBLCHECK.Find(degerler.tblkitaplar.kitapcheckid);
                degerler.kisiadi = db.TBLKISILER.Find(degerler.tblcheck.islemkisi).kisiad;
            }


            return View(degerler);
        }

        public ActionResult List()
        {

            return View(db.TBLKITAP.ToList());
        }

        [HttpGet]

        public ActionResult CheckOut(int id)
        {
            return View();

        }

        [HttpPost]

        public ActionResult CheckOut(int id, TBLKISILER yenikisi)
        {
            var yenikisi1 = db.TBLKISILER.Add(yenikisi);

            TBLCHECK yeniislem1 = new TBLCHECK();
            yeniislem1.islemtarih = DateTime.Now.Date;
            yeniislem1.islemkisi = yenikisi1.kisiid;

            var yeniislem = db.TBLCHECK.Add(yeniislem1);

            db.TBLKITAP.Find(id).kitapcheckid = yeniislem.checkid;
            db.TBLKITAP.Find(id).kitapcheck = "Alınamaz";
            db.SaveChanges();
            return RedirectToAction("../");
        }

        [HttpGet]

        public ActionResult CheckIn(int id)
        {
            
            var alinankitap = db.TBLKITAP.Find(id);
            var yapilancheck = db.TBLCHECK.Find(alinankitap.kitapcheckid);
            var alankisi = db.TBLKISILER.Find(yapilancheck.islemkisi);
            kitapkisiveislem alinanVeri = new kitapkisiveislem();
            alinanVeri.kitapAdi = alinankitap.kitapad;
            alinanVeri.kisiAdi = alankisi.kisiad;
            alinanVeri.kisiTelefon = alankisi.kisitelno;
            DateTime yeniTarih = yapilancheck.islemtarih.Value.AddDays(+15);
            alinanVeri.beklenenTeslim = yeniTarih.ToString();
            alinanVeri.gercekTeslim = DateTime.Now.Date.ToString();
            TimeSpan gunFarki = (DateTime.Now.Date)-yeniTarih;
            int ceza = 0;
            if (gunFarki.TotalDays > 0)
            {
                int gunler = (int)gunFarki.TotalDays;
                ceza = gunler * 5;
            }

            alinanVeri.toplamCeza = ceza.ToString();
            return View(alinanVeri);
        }

        [HttpPost]

        public ActionResult CheckIn(int id, int? ad)
        {
            var kitap = db.TBLKITAP.Find(id);
            kitap.kitapcheck = "Alınabilir";
            kitap.kitapcheckid = null;
            db.SaveChanges();
            return RedirectToAction("../");
        }
    }
}