using System;
using System.ComponentModel.DataAnnotations;

using static Closet.Data.DataConstants;

namespace Closet.Web.Models.Comments
{
    public class CommentCreateViewModel
    {
        [Required]
        [StringLength(CommentContentMaxLength, MinimumLength = CommentContentMinLength)]
        public string Content { get; set; }

        public int? MemeId { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
