using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Microsoft.AspNet.Identity.Owin;

namespace WS2.Controllers {
    public class AuthorController : Controller {
        // GET: Author
        public ActionResult Index() {
            var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var authors = context.Authors.ToList();
            return View(authors);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id) {
            var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var author = context.Authors
                .Include(x=> x.Books)
                .FirstOrDefault(x => x.Id == id);
            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public ActionResult Create(Author model) {
            try {
                var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

                var newauthor = new Author();
                newauthor.Name = model.Name;
                context.Authors.Add(newauthor);
                context.SaveChanges();

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: Author/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {
            try {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: Author/Delete/5
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
