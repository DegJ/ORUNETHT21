using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data;
using Shared;

namespace Services {
    public class BookService {
        private readonly HttpContext _httpcontext;

        public BookService(HttpContext httpcontext) {
            _httpcontext = httpcontext;
        }

        public void SaveNewBook(BookCreateModel model) {
            using (var context = new ApplicationDbContext())
            {
                var newbook = new Book()
                {
                    Title = model.Title,
                    Author = model.Author
                };

                var filename = model.Image.FileName;
                var filepath = _httpcontext.Server.MapPath("~/UploadedImages");
                model.Image.SaveAs(filepath + "/" + filename);

                newbook.ImagePath = filename;

                context.Books.Add(newbook);
                context.SaveChanges();
            }
        }
    }
}
