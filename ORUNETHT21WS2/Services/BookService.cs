using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data;
using Data.Models;
using Shared.Models;

namespace Services {
    public class BookService {
        private readonly BookContext _context;

        private BookRepository BookRepository {
            get { return new BookRepository(_context ?? new BookContext()); }
        }
        private AuthorRepository AuthorRepository {
            get { return new AuthorRepository(_context ?? new BookContext()); }
        }
        private GenreRepository GenreRepository {
            get { return new GenreRepository(_context ?? new BookContext()); }
        }
        private ImageService ImageService {
            get { return new ImageService(); }
        }

        public BookService() { }
        public BookService(BookContext context) {
            _context = context;
        }

        public BookEditViewModel GetEditModel(int? id) {
            var model = new BookEditViewModel();
            if (id.HasValue) {
                var book = BookRepository.GetBook(id.Value);
                model.Id = book.Id;
                model.Title = book.Title;
                model.ExistingImagePath = book.ImagePath;
                model.ChosenAuthor = book.AuthoredById;
                model.ChosenGenres = book.Genres.Select(x => x.Id).ToArray();
            } else {
                model.IsCreateBookView = true;
            }

            FillEditModelWithAssociations(model);

            return model;
        }

        public BookEditViewModel FillEditModelWithAssociations(BookEditViewModel model) {
            var genres = GenreRepository.GetGenres();
            var authors = AuthorRepository.GetAuthors();

            model.Genres = genres.Select(x => new BookEditKeyValueViewModel { Name = x.Name, Id = x.Id }).ToList();
            model.Authors = authors.Select(x => new BookEditKeyValueViewModel { Name = x.Name, Id = x.Id }).ToList();
            return model;
        }

        public BookEditViewModel EditBook(BookEditViewModel model) {
            var book = BookRepository.GetBook(model.Id);
            if (book == null) throw new ArgumentException("Boken fanns inte!");
            if (model.Image != null) {
                if (!string.IsNullOrEmpty(book.ImagePath)) ImageService.RemoveImageFromDiskIfExists(book.ImagePath); //gör lite cleanup
                book.ImagePath = ImageService.SaveImageToDisk(model.Image);
            }
            model.ExistingImagePath = book.ImagePath;

            book.Title = model.Title;
            book.AuthoredById = model.ChosenAuthor != 0 ? model.ChosenAuthor : null;
            /*
             * ta bort de genres vi inte längre har valda
             * vi gör det i två steg, en för att ta reda på vilka som bör tas bort för de inte längre är valda
             * det andra ssteget är att faktiskt ta bort dem från boken och därmed låta EF veta med en "remove" på listan vilka som ska bort.
             */
            if (book.Genres != null) {
                var toRemoveGenres = new List<Genre>();
                foreach (var genre in book.Genres) {
                    if (model.ChosenGenres == null || !model.ChosenGenres.Contains(genre.Id))
                        toRemoveGenres.Add(genre);
                }

                foreach (var genre in toRemoveGenres) {
                    book.Genres.Remove(genre);
                }
            }

            //vi lägger sedan till de nya valen som inte tidigare var valda.
            if (model.ChosenGenres != null) {
                if (book.Genres == null) book.Genres = new List<Genre>();
                foreach (var genreid in model.ChosenGenres.Where(genreId => book.Genres.All(x => x.Id != genreId))) {
                    var genre = GenreRepository.GetGenre(genreid);
                    if (genre != null) {
                        book.Genres.Add(genre);
                    }
                }
            }

            BookRepository.SaveBook(book);

            return model;
        }

        public BookEditViewModel CreateNewBook(BookEditViewModel model) {
            var newbook = new Book() {
                Title = model.Title,
                Genres = new List<Genre>()
            };
            if (model.Image != null) {
                newbook.ImagePath = ImageService.SaveImageToDisk(model.Image);
                model.ExistingImagePath = newbook.ImagePath;
            }

            newbook.AuthoredById = model.ChosenAuthor != 0 ? model.ChosenAuthor : null;

            if (model.ChosenGenres != null) {
                foreach (var genreid in model.ChosenGenres) {
                    var genre = GenreRepository.GetGenre(genreid);
                    if (genre != null) {
                        newbook.Genres.Add(genre);
                    }
                }
            }

            BookRepository.SaveBook(newbook);
            return model;
        }
    }
}