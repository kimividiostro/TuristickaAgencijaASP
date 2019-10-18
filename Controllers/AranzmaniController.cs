using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using Microsoft.AspNet.Identity;


namespace WebApplication1.Controllers
{
    [Authorize]
    public class AranzmaniController : Controller
    {

        public ActionResult NoviAranzman()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DodajAranzman(Aranzman aranzman)
        {
            bool upisano = Broker.Instanca.DodajAranzman(aranzman);

            //Sta ako nije uspelo ? Dodaj.

            return RedirectToAction("Index", "Manage");
        }

        // GET: Aranzmani
        public ActionResult SviAranzmani()
        {
            SviAranzmaniViewModel savm = new SviAranzmaniViewModel();
            savm.Aranzmani = Broker.Instanca.UcitajSveAranzmane();

            
            
            return View(savm);
        }

        
        
        public ActionResult Detalji(Aranzman aranzman)
        {
            if (aranzman.Equals(default(Aranzman)))
                return HttpNotFound();
            

            return View(aranzman);
        }


        [HttpPost]
        public ActionResult Rezervisi(Aranzman aranzman)
        {
            if (aranzman.Equals(default(Aranzman)))
                return HttpNotFound();

            string korisnikID = User.Identity.GetUserId();

            bool rezervisao = Broker.Instanca.ProveriIRezervisi(korisnikID, aranzman);

            if (rezervisao)
                return View("~/Views/Aranzmani/UspesnoRezervisano.cshtml");

            return View("~/Views/Aranzmani/NeuspesnoRezervisano.cshtml");
        }
    }
}