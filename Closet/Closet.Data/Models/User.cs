using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Closet.Data.Models
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        public List<Meme> Memes { get; set; } = new List<Meme>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<MemeVote> MemeVotes { get; set; } = new List<MemeVote>();

        public List<CommentVote> CommentVotes { get; set; } = new List<CommentVote>();

    }
}
