using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using Data;
using Data.Models;

namespace ORUNETHT21WS2.Controllers {
    public class AuthorController : Controller {
        public ActionResult Index() {
            var authors = new AuthorRepository().GetAuthors();
            return View(authors);
        }
        public ActionResult Details(int id) {
            var author = new AuthorRepository().GetAuthor(id);
            return View(author);
        }
        public ActionResult Edit(int id) {
            var author = new AuthorRepository().GetAuthor(id);
            return View(author);
        }
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author) {
            if (!ModelState.IsValid) {
                return View(author);
            }
            return View(author);
        }

        public ActionResult Create() {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author) {
            try {
                new AuthorRepository().CreateAuthor(author);
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }
    }

    [System.Web.Http.RoutePrefix("api/author")]
    public class AuthorApiController : ApiController {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("delete/{id}")]
        public IHttpActionResult Delete(int id) {
            try {
                var repository = new AuthorRepository();
                repository.DeleteAuthor(id);
                return Ok();
            } catch {
                return BadRequest();
            }
        }
    }
}
