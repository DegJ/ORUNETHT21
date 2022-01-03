﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data {
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser {
        public int RatingForUser { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBookCoAuthored> AuthorBookCo { get; set; }
        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasOptional(x => x.AuthoredBy)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthoredById)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Author>().HasMany(x => x.CoAuthoredBooks)
                .WithRequired(x => x.Author);
            modelBuilder.Entity<Book>().HasMany(x => x.CoAuthoredBy)
                .WithRequired(x => x.Book);
            modelBuilder.Entity<AuthorBookCoAuthored>()
                .HasKey(x => new { AuthorId = x.AuthorId, BookId = x.BookId });
        }
    }
}