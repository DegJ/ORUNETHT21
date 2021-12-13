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
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author>().HasMany(x => x.Books)
                .WithOptional(x => x.AuthoredBy)
                .HasForeignKey(x => x.AuthoredById)
                .WillCascadeOnDelete(false);
        }
    }
}
