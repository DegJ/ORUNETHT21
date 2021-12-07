﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Data;
using Services;
using Shared.Models;

namespace ORUNETHT21WS1.Controllers {
    public class BookController : Controller {

        public BookService BookService {
            get { return new BookService(); }
        }
        public BookRepository BookRepository {
            get { return new BookRepository(); }
        }

        public ActionResult Index() {
            var books = BookRepository.GetAllBooks();
            return View(books);
        }

        public ActionResult Create() {
            var model = new BookEditViewModel() {
                IsCreateBookView = true
            };
            //det går bra att själv sätta vilken vy/cshtml fil som ska försöka renderas
            return View("Edit", model);
        }
        public ActionResult Edit(int id) {
            var model = BookService.GetEditModel(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BookEditViewModel model) {
            try {
                //kollar om vi kommer från "Create" istället för att vi som nu editerar något, hade också kunnat kolla om .Id == 0 men detta är mer explicit.
                if (model.IsCreateBookView) {
                    model = BookService.CreateNewBook(model);
                    model.IsCreateBookView = false;
                } else {
                    model = BookService.EditBook(model);
                }
                ViewBag.Saved = true;
                return View(model);

            } catch (Exception e) {
                ViewBag.Error = true;
                return View(model);
            }
        }
    }
}
