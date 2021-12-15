using System;
using System.Web;
using System.Web.Mvc;
using Data;
using Microsoft.AspNet.Identity.Owin;
using Services;
using Shared.Models;

namespace ORUNETHT21WS2.Controllers
{
    public class BookController : Controller
    {

        public BookService BookService
        {
            get { return new BookService(HttpContext.GetOwinContext().Get<BookContext>()); }
        }
        public BookRepository BookRepository
        {
            get { return new BookRepository(HttpContext.GetOwinContext().Get<BookContext>()); }
        }

        public ActionResult Index()
        {
            var books = BookRepository.GetAllBooks();
            if (Request.IsAjaxRequest())
            {
                return PartialView("BookTable", books);
            }
            return View(books);
        }

        public ActionResult Create()
        {
            var model = BookService.GetEditModel(null);
            //det går bra att själv sätta vilken vy/cshtml fil som ska försöka renderas
            return View("Edit", model);
        }
        public ActionResult Edit(int id)
        {
            var model = BookService.GetEditModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                BookService.FillEditModelWithAssociations(model);
                return View(model);
            }
            try
            {
                //kollar om vi kommer från "Create" istället för att vi som nu editerar något, hade också kunnat kolla om .Id == 0 men detta är mer explicit.
                if (model.IsCreateBookView)
                {
                    model = BookService.CreateNewBook(model);
                    model.IsCreateBookView = false;
                }
                else
                {
                    model = BookService.EditBook(model);
                }
                ViewBag.Saved = true;

                BookService.FillEditModelWithAssociations(model);
                return View(model);
            }
            catch
            {
                ViewBag.Error = true;
                BookService.FillEditModelWithAssociations(model);
                return View(model);
            }
        }

        public ActionResult Details(int id)
        {
            var book = BookRepository.GetBook(id);
            return View(book);
        }
    }
}
