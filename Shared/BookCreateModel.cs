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
        public string Author { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}
