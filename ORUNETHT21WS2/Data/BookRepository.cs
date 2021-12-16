using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data {
    public class BookRepository {
        private readonly BookContext _context;

        public BookRepository(BookContext context) {
            _context = context;
        }

        public Book GetBook(int id) {
            return _context.Books
                .Include(x => x.AuthoredBy)
                .Include(x => x.Genres)
                .FirstOrDefault(x => x.Id == id);

        }

        public bool DeleteBook(int id) {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null) return false;
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;

        }

        public List<Book> GetAllBooks() {
            return _context.Books
                .Include(x => x.AuthoredBy)
                .Include(x => x.Genres)
                .ToList();
        }

        public Book SaveBook(Book book) {
            if (book.Id != 0) { //om har fått ett id, då finns boken redan, vi ska spara det som ändrats.
                _context.Entry(book).State = EntityState.Modified; // vi säger åt EF att denna boken med dess [Key] att vi vill spara om alla fält
            } else {
                _context.Books.Add(book);
            }

            _context.SaveChanges();
            return book;

        }
    }
}
