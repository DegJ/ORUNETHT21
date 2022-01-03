using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data {
    public class Author {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<AuthorBookCoAuthored> CoAuthoredBooks { get; set; }
    }

    public class AuthorBookCoAuthored {
        [Key, ForeignKey("Author")]
        public int AuthorId { get; set; }
        [Key, ForeignKey("Book")]
        public int BookId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
