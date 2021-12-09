using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data {
    public class BookRepository {
        public Book GetBook(int id) {
            using (var context = new BookContext()) {
                return context.Books
                    .Include(x => x.AuthoredBy)
                    .Include(x => x.Genres)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public bool DeleteBook(int id) {
            using (var context = new BookContext()) {
                var book = context.Books.FirstOrDefault(x => x.Id == id);
                if (book == null) return false;
                context.Books.Remove(book);
                context.SaveChanges();
                return true;
            }
        }

        public List<Book> GetAllBooks() {
            using (var context = new BookContext()) {
                return context.Books
                    .Include(x => x.AuthoredBy)
                    .Include(x => x.Genres)
                    .ToList();
            }
        }

        public Book SaveBook(Book book) {
            using (var context = new BookContext()) {
                if (book.Id != 0) { //om har fått ett id, då finns boken redan, vi ska spara det som ändrats.
                    context.Entry(book).State = EntityState.Modified; // vi säger åt EF att denna boken med dess [Key] att vi vill spara om alla fält
                } else {
                    context.Books.Add(book);
                }

                context.SaveChanges();
                return book;
            }
        }
    }
}
