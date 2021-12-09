using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Shared.Models;

namespace Services {
    public class BookService {
        private BookRepository BookRepository {
            get { return new BookRepository(); }
        }
        private ImageService ImageService {
            get { return new ImageService(); }
        }

        public BookEditViewModel GetEditModel(int id) {
            var book = BookRepository.GetBook(id);
            return new BookEditViewModel {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ExistingImagePath = book.ImagePath
            };
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
                Title = model.Title
            };
            if (model.Image != null) {
                newbook.ImagePath = ImageService.SaveImageToDisk(model.Image);
                model.ExistingImagePath = newbook.ImagePath;
            }

            if (model.ChosenAuthor.HasValue) {

            }
            BookRepository.SaveBook(newbook);
            return model;
        }
    }
}
