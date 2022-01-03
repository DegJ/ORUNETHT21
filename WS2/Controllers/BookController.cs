using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Microsoft.AspNet.Identity.Owin;
using Services;
using Shared;

namespace WS2.Controllers {
    public class BookController : Controller {
        // GET: Book
        public ActionResult Index() {
            using (var context = new ApplicationDbContext()) {
                var books = context.Books
                    .Include(x => x.SavedByUser)
                    .Include(x => x.AuthoredBy)
                    .ToList();

                return View(books);
            }
        }

        [HttpPost]
        public ActionResult SearchBooks(BookSearchModel model) {
            using (var context = new ApplicationDbContext()) {
                var books = context.Books
                    .Where(x => x.Title == model.Search
                               || x.AuthoredBy.Name == model.Search)
                    .Include(x => x.SavedByUser)
                    .Include(x => x.AuthoredBy)
                    .ToList();

                return View("Index", books);
            }
        }

        // GET: Book/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: Book/Create
        [Authorize]
        public ActionResult Create() {
            var model = new BookCreateModel();
            var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            var authorsToChooseFrom = context.Authors.ToList();

            model.Authors = authorsToChooseFrom
                .Select(x => new BookCreateAuthorsModel { AuthorId = x.Id, Name = x.Name })
                .ToList();

            return View(model);
        }

        // POST: Book/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(BookCreateModel model) {
            try {
                var service = new BookService(System.Web.HttpContext.Current);
                service.SaveNewBook(model);
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Book/Edit/5
        [Authorize]
        public ActionResult Edit(int id) {
            var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var book = context.Books
                .Include(x => x.SavedByUser)
                .FirstOrDefault(x => x.Id == id);
            var username = User.Identity.Name;
            if (username != book.SavedByUser.UserName) {
                return RedirectToAction("Index");
            }

            var model = new BookEditModel();
            model.Title = book.Title;
            model.AuthorId = book.AuthoredById;
            model.Id = book.Id;

            var authorsToChooseFrom = context.Authors.ToList();

            model.Authors = authorsToChooseFrom
                .Select(x => new BookCreateAuthorsModel { AuthorId = x.Id, Name = x.Name })
                .ToList();
            
            return View(model);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {
            try {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            try {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }
    }
}
