using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using System.Data.Entity;

namespace Data {
    public class AuthorRepository {
        public Author GetAuthor(int id) {
            using (var context = new BookContext()) {
                return context.Authors
                    .Include(x => x.Books)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Author> GetAuthors() {
            using (var context = new BookContext()) {
                return context.Authors
                    .Include(x => x.Books)
                    .ToList();
            }
        }
        public void DeleteAuthor(int id) {
            using (var context = new BookContext()) {
                var author = context.Authors.FirstOrDefault(x => x.Id == id);
                if (author != null) {
                    context.Authors.Remove(author);
                    context.SaveChanges();
                }
            }
        }

        public void CreateAuthor(Author author) {
            using (var context = new BookContext()) {
                context.Authors.Add(author);
                context.SaveChanges();
            }
        }
    }
}
