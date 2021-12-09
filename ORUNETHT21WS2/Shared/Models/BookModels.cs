using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shared.Models {
    public class BookEditViewModel {
        public int Id { get; set; }
        [DisplayName("Titel")]
        public string Title { get; set; }
        [DisplayName("Författare")]
        public string Author { get; set; }
        [DisplayName("Bild")]
        public HttpPostedFileBase Image { get; set; }
        public string ExistingImagePath { get; set; }
        public bool IsCreateBookView { get; set; }
        public int? ChosenAuthor { get; set; }
        public int[] ChosenGenres { get; set; }
        public List<BookEditKeyValueViewModel> Authors { get; set; }
        public List<BookEditKeyValueViewModel> Genres { get; set; }
    }

    public class BookEditKeyValueViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class BookDeleteModel {
        public int BookId { get; set; }
    }
}
