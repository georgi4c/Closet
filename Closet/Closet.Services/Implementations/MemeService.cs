using System.Collections.Generic;
using System.Threading.Tasks;
using Closet.Services.Models;
using Closet.Data.Models;
using Closet.Data;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

using static Closet.Services.ServiceConstants;

namespace Closet.Services.Implementations
{
    public class MemeService : IMemeService
    {
        private readonly ClosetDbContext db;

        public MemeService(ClosetDbContext db)
        {
            this.db = db;
        }

        public async Task<int> AddOrUpdateVote(int memeId, string userId, int value)
        {
            if (!await this.DoesMemeExistAsync(memeId))
            {
                return -1;
            }
            var vote = this.db.MemeVotes
                .Where(m => m.MemeId == memeId && m.UserId == userId)
                .FirstOrDefault();

            value = this.NormalizeValue(value);

            if (vote == null)
            {
                vote = new MemeVote
                {
                    Value = value,
                    MemeId = memeId,
                    UserId = userId
                };

                this.db.Add(vote);
            }
            else
            {
                var difference = vote.Value + value;
                vote.Value = this.NormalizeValue(difference); ;
            }

            await this.db.SaveChangesAsync();

            return await this.Votes(memeId);
        }

        public async Task<IEnumerable<MemeListingServiceModel>> AllAsync(int page = 1)
            => await this.db
                .Memes
                .OrderByDescending(m => m.CreatedOn)
                .Skip((page - 1) * MemesPageSize)
                .Take(MemesPageSize)
                .ProjectTo<MemeListingServiceModel>()
                .ToListAsync();


        public async Task CreateAsync(string userId, string title, string imageUrl)
        {
            Meme meme = new Meme
            {
                Title = title,
                ImageUrl = imageUrl,
                AuthorId = userId,
                CreatedOn = DateTime.UtcNow
            };

            this.db.Add(meme);
            await this.db.SaveChangesAsync();
        }

        public async Task<int> Votes(int memeId)
        {
            if (!await this.DoesMemeExistAsync(memeId))
            {
                return -1;
            }

            return this.db.MemeVotes.Where(v => v.MemeId == memeId).Select(v => v.Value).DefaultIfEmpty(0).Sum();
        }

        public Task<MemeWithCommentsServiceModel> WithCommentsById(int id)
            => this.db
                .Memes
                .Where(m => m.Id == id)
                .ProjectTo<MemeWithCommentsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> DoesMemeExistAsync(int id)
            => await this.db.Memes.AnyAsync(m => m.Id == id);

        public async Task<IEnumerable<MemeTopThreeServiceModel>> TopThree()
            => await this.db
                .Memes
                .OrderByDescending(m => m.CreatedOn)
                .Take(3)
                .ProjectTo<MemeTopThreeServiceModel>()
                .ToListAsync();

        public async Task Edit(int id, string title, string imageUrl)
        {
            var meme = this.db.Memes.Find(id);

            if (meme != null)
            {
                meme.Title = title;
                meme.ImageUrl = imageUrl;

                await this.db.SaveChangesAsync();
            }
        }

        public async Task<bool> Delete(int id)
        {
            var meme = this.db.Memes.Find(id);

            if (meme == null)
            {
                return false;
            }

            var memeComments = this.db.Comments.Where(c => c.MemeId == id);

            foreach (var comment in memeComments)
            {
                comment.MemeId = null;
            }

            this.db.Remove(meme);
            await this.db.SaveChangesAsync();

            return true;
        }



        public async Task<MemeMinifiedModel> ById(int id)
            => await this.db.Memes.Where(m => m.Id == id).ProjectTo<MemeMinifiedModel>().FirstOrDefaultAsync();

        private int NormalizeValue(int value)
        {
            if (value > 1)
            {
                value = 1;
            }
            if (value < -1)
            {
                value = -1;
            }
            return value;
        }

        public async Task<string> AuthorId(int id)
            => await this.db.Memes.Where(m => m.Id == id).Select(m => m.AuthorId).FirstOrDefaultAsync();
    }
}
