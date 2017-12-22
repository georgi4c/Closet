using Closet.Common.Mapping;
using Closet.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Closet.Web.Models.Memes
{
    public class MemeCreateViewModel : IMapFrom<Meme>
    {
        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
