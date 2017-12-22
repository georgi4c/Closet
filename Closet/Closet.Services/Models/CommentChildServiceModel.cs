using AutoMapper;
using Closet.Common.Mapping;
using Closet.Data.Models;
using System;

namespace Closet.Services.Models
{
    public class CommentChildServiceModel : IMapFrom<Comment>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Comment, CommentChildServiceModel>()
                .ForMember(cm => cm.Author, cfg => cfg.MapFrom(c => c.Author.UserName));
    }
}
