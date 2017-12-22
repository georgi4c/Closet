using Closet.Common.Mapping;
using Closet.Data.Models;
using System;
using AutoMapper;

namespace Closet.Services.Models
{
    public class MemeMinifiedModel : IMapFrom<Meme>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
