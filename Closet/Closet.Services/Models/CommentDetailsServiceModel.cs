using Closet.Common.Mapping;
using Closet.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using System.Collections.Generic;

namespace Closet.Services.Models
{
    public class CommentDetailsServiceModel : IMapFrom<Comment>, IHaveCustomMapping
    {
        public int Id { get; set; }
        
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
                
        public string AuthorId { get; set; }

        public string Author { get; set; }


        public List<CommentChildServiceModel> ChildrenComments { get; set; }
        
        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Comment, CommentDetailsServiceModel>()
                .ForMember(cm => cm.Author, cfg => cfg.MapFrom(c => c.Author.UserName));
    }
}
