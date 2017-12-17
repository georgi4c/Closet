using System;
using System.ComponentModel.DataAnnotations;

namespace Closet.Services.Models
{
    public class MemeListingServiceModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
