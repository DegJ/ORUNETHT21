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
    }
}
