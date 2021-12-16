using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Models;
using Microsoft.AspNet.Identity.Owin;

namespace ORUNETHT21WS2.Controllers {
    public class GenreController : Controller {

        private GenreRepository GenreRepository {
            get { // detta funderar för att vi i Startup.Configuration har sagt att vi ska skapa upp en BookContext per request, det är då en instans som delas i hela requestet
                return new GenreRepository(HttpContext.GetOwinContext().Get<BookContext>());
            }
        }

        // GET: Genre
        public ActionResult Index()
        {
            var genres = GenreRepository.GetGenres();
            return View(genres);
        }

        // GET: Genre/Details/5
        public ActionResult Details(int id)
        {
            var genre = GenreRepository.GetGenre(id);
            return View(genre);
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            var isValid = ModelState.IsValid;
            if (!isValid) return View(genre);
            try
            {
                GenreRepository.Create(genre);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(int id)
        {
            var genre = GenreRepository.GetGenre(id);
            return View(genre);
        }

        // POST: Genre/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Genre genre)
        {
            try
            {
                GenreRepository.Edit(genre);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genre/Delete/5
        public ActionResult Delete(int id)
        {
            var genre = GenreRepository.GetGenre(id);
            return View(genre);
        }

        // POST: Genre/Delete/5
        [HttpPost]
        public ActionResult DeleteGenre(int id)
        {
            try
            {
                GenreRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var genre = GenreRepository.GetGenre(id);
                return View("Delete", genre);
            }
        }
    }
}
