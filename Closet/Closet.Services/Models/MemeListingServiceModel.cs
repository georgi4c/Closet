using AutoMapper;
using Closet.Common.Mapping;
using Closet.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Closet.Services.Models
{
    public class MemeListingServiceModel : IMapFrom<Meme>, IHaveCustomMapping
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Votes { get; set; }

        public int Comments { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Meme, MemeListingServiceModel>()
                .ForMember(mm => mm.Comments, cfg => cfg.MapFrom(m => m.Comments.Count()))
                .ForMember(mm => mm.Votes, cfg => cfg.MapFrom(m => m.Votes.Sum(v => v.Value)));
    }
}
