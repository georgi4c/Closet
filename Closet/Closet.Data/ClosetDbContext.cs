using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Meme>()
                .HasOne(m => m.Author)
                .WithMany(a => a.Memes)
                .HasForeignKey(m => m.AuthorId);

            base.OnModelCreating(builder);
        }
    }
}
