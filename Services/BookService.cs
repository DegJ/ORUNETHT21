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
            using (var context = new ApplicationDbContext()) {

                var newbook = new Book() {
                    Title = model.Title,
                };

                var username = _httpcontext.User.Identity.Name;
                if (!string.IsNullOrEmpty(username)) {
                    var user = context.Users.FirstOrDefault(x => x.UserName == username);
                    if (user != null) {
                        newbook.SavedByUser = user;
                    }
                }

                if (model.AuthorId != null && model.AuthorId != 0) {
                    var author = context.Authors.FirstOrDefault(x => x.Id == model.AuthorId);
                    if (author != null) {
                        newbook.AuthoredBy = author;
                    }
                }

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
