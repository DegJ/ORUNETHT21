﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey(nameof(AuthoredBy))]
        public int? AuthoredById { get; set; }
        public virtual Author AuthoredBy { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public decimal Rating { get; set; }
        public string UserIdThatCreated { get; set; }
    }
}
