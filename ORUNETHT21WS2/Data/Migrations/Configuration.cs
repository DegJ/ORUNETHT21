using System.Collections.Generic;
using Data.Models;

namespace Data.Migrations {
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Data.BookContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(Data.BookContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var jrrtolkienauthor = new Author() {
                Id = 1,
                Name = "JRR Tolkien"
            };
            var fantasygenre = new Genre() {
                Id = 1,
                Name = "Fantasy"
            };
            var adventuregenre = new Genre() {
                Id = 2,
                Name = "Adventure"
            };
            context.Books.AddOrUpdate(x => x.Id, new[] {
                new Book() {
                    Id = 1,
                    Title = "Lord of the rings: the fellowship of the ring",
                    AuthoredBy = jrrtolkienauthor,
                    Genres = new List<Genre>() { fantasygenre }
                },
                new Book() {
                    Id = 2,
                    Title = "Lord of the rings: the two towers",
                    AuthoredBy = jrrtolkienauthor,
                    Genres = new List<Genre>() { fantasygenre, adventuregenre }
                },
            });
        }
    }
}
