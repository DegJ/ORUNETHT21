using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shared
{
    public class BookCreateModel
    {
        public string Title { get; set; }
        public int? AuthorId { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public List<BookCreateAuthorsModel> Authors { get; set; }
    }

    public class BookEditModel {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? AuthorId { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public List<BookCreateAuthorsModel> Authors { get; set; }
    }

    public class BookCreateAuthorsModel {
        public int AuthorId { get; set; }
        public string Name { get; set; }
    }
}
