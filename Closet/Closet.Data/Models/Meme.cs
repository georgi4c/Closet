﻿using System.ComponentModel.DataAnnotations;

namespace Closet.Data.Models
{
    public class Meme
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
