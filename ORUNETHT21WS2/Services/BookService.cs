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
            get { return new BookRepository(); }
        }
        private AuthorRepository AuthorRepository {
            get { return new AuthorRepository(); }
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
                model.Author = book.Author;
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
                model.ExistingImagePath = book.ImagePath; //den nya pathen vi nu fick assignar vi så vi kan visa den för användaren med hjälp av BookEditModel i Viewn.
            }

            book.Title = model.Title;
            book.Author = model.Author;
            BookRepository.SaveBook(book);

            return model;
        }

        public BookEditViewModel CreateNewBook(BookEditViewModel model) {
            var newbook = new Book() {
                Author = model.Author,
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
                        newbook.Genres.Add(genre); //multi contexts, need to fix = use owin context getter for singleton
                    }
                }
            }

            BookRepository.SaveBook(newbook);
            return model;
        }
    }
}