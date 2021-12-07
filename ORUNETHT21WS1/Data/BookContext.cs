using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data {
    public class BookContext : DbContext {
        public BookContext() : base("DefaultConnection") {

        }

        public DbSet<Book> Books { get; set; }
    }
}
