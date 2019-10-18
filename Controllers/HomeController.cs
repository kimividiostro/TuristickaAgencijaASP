using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        //PREUSMERI NA SVE ARANZMANE AKO JE KORISNIK ULOGOVAN
        public ActionResult Index()
        {
            
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("SviAranzmani", "Aranzmani");

            return View();
        }

        
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}