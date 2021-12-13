using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models {
    public class Genre {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Namn")]
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
