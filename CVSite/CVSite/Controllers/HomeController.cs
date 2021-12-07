using System.Web.Mvc;

namespace Cvsite.Controllers {
    public class HomeController : Controller {

        public ActionResult Index() {
            return View();
        }

        [Route("home/about")]
        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View("Contact");
        }
    }
}