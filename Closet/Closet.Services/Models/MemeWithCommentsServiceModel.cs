using Closet.Common.Mapping;
using Closet.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Closet.Services.Models
{
    public class MemeWithCommentsServiceModel : IMapFrom<Meme>, IHaveCustomMapping
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public int Votes { get; set; }

        public List<CommentDetailsServiceModel> Comments { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Meme, MemeWithCommentsServiceModel>()
                .ForMember(mm => mm.Author, cfg => cfg.MapFrom(m => m.Author.UserName))
                .ForMember(mm => mm.Votes, cfg => cfg.MapFrom(m => m.Votes.Sum(v => v.Value)));
    }
}
