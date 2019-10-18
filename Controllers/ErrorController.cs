using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ViewResult EmailNotConfirmed()
        {
            return View();
        }
    }
}