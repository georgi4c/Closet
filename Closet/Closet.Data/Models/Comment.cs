using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Closet.Data.DataConstants;

namespace Closet.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CommentContentMaxLength, MinimumLength = CommentContentMinLength)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? MemeId { get; set; }

        public Meme Meme { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        public int? ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }

        public List<Comment> ChildrenComments { get; set; } = new List<Comment>();

        public List<CommentVote> Votes { get; set; } = new List<CommentVote>();
    }
}
