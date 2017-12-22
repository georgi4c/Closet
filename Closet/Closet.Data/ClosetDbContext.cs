using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Closet.Data.Models;

namespace Closet.Data
{
    public class ClosetDbContext : IdentityDbContext<User>
    {
        public ClosetDbContext(DbContextOptions<ClosetDbContext> options)
            : base(options)
        {
        }

        public DbSet<Meme> Memes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<MemeVote> MemeVotes { get; set; }

        public DbSet<CommentVote> CommentVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Meme>()
                .HasOne(m => m.Author)
                .WithMany(a => a.Memes)
                .HasForeignKey(m => m.AuthorId);

            builder.Entity<Comment>()
                .HasOne(c => c.Meme)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MemeId);

            builder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId);

            builder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(pc => pc.ChildrenComments)
                .HasForeignKey(c => c.ParentCommentId);
            
            builder.Entity<MemeVote>()
                .HasOne(mv => mv.Meme)
                .WithMany(m => m.Votes)
                .HasForeignKey(mv => mv.MemeId);

            builder.Entity<MemeVote>()
                .HasOne(mv => mv.User)
                .WithMany(m => m.MemeVotes)
                .HasForeignKey(mv => mv.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<CommentVote>()
                .HasOne(mv => mv.Comment)
                .WithMany(m => m.Votes)
                .HasForeignKey(mv => mv.CommentId);

            builder.Entity<CommentVote>()
                .HasOne(mv => mv.User)
                .WithMany(m => m.CommentVotes)
                .HasForeignKey(mv => mv.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
