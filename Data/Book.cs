using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data {
    public class Book {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImagePath { get; set; }
        public int Rating { get; set; }

        [ForeignKey(nameof(SavedByUser))]
        public string SavedByUserId { get; set; }
        public virtual ApplicationUser SavedByUser { get; set; }

        [ForeignKey(nameof(AuthoredBy))]
        public int? AuthoredById { get; set; }
        public virtual Author AuthoredBy { get; set; }

        public virtual ICollection<AuthorBookCoAuthored> CoAuthoredBy { get; set; }
    }
}
