﻿using Closet.Data;
using Closet.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Closet.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ClosetDbContext db;

        public CommentService(ClosetDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string authorId, string content, int? memeId, int? parentCommentId)
        {
            Comment comment = new Comment
            {
                AuthorId = authorId,
                Content = content,
                MemeId = memeId,
                ParentCommentId = parentCommentId,
                CreatedOn = DateTime.Now
            };

            db.Add(comment);
            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var comment = this.db.Comments.Find(id);

            if (comment != null)
            {

                var subComments = this.db.Comments.Where(c => c.ParentCommentId == id);

                foreach (var subComment in subComments)
                {
                    subComment.MemeId = null;
                }

                this.db.Remove(comment);
                await this.db.SaveChangesAsync();
            }
        }

        public async Task<int?> MemeId(int commentId)
            => await this.db.Comments
                .Where(c => (c.Id == commentId || c.ChildrenComments.Any(cc => cc.Id == commentId)) && c.MemeId != null)
                .Select(c => c.MemeId)
                .FirstOrDefaultAsync();

        public async Task<string> AuthorId(int id)
            => await this.db.Comments.Where(m => m.Id == id).Select(m => m.AuthorId).FirstOrDefaultAsync();
    }
}
