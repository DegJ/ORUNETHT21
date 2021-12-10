using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using System.Data.Entity;

namespace Data {
    public class GenreRepository {
        private readonly BookContext _context;

        //vi tar en själva contexten som en "dependency" till repositoryt
        public GenreRepository(BookContext context) {
            _context = context;
        }

        public Genre GetGenre(int id) {
            return _context.Genres
                .Include(x => x.Books)
                .FirstOrDefault(x => x.Id == id);

        }

        public List<Genre> GetGenres() {
            return _context.Genres
                .Include(x => x.Books)
                .ToList();
        }

        public void Create(Genre genre) {
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void Edit(Genre genre) {
            var genrefromdb = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);
            if (genrefromdb != null) {
                genrefromdb.Name = genre.Name;
            }
            _context.SaveChanges();

        }

        public void Delete(int id) {
            var genrefromdb = _context.Genres.FirstOrDefault(x => x.Id == id);
            if (genrefromdb != null) {
                _context.Genres.Remove(genrefromdb);
                _context.SaveChanges();
            }

        }
    }
}
