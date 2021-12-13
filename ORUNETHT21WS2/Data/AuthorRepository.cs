using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using System.Data.Entity;

namespace Data {
    public class AuthorRepository {
        private readonly BookContext _context;

        public AuthorRepository(BookContext context) {
            _context = context;
        }

        public Author GetAuthor(int id) {
            return _context.Authors
                .Include(x => x.Books)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Author> GetAuthors() {
            return _context.Authors
                .Include(x => x.Books)
                .ToList();

        }
        public void DeleteAuthor(int id) {
            var author = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (author != null) {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }

        public void CreateAuthor(Author author) {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }
}
