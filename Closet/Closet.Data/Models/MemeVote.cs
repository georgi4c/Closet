using System;
using System.ComponentModel.DataAnnotations;

namespace Closet.Data.Models
{
    public class MemeVote
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int MemeId { get; set; }

        public Meme Meme { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
