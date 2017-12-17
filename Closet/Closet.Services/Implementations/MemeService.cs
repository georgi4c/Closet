using System.Collections.Generic;
using System.Threading.Tasks;
using Closet.Services.Models;
using Closet.Data.Models;
using Closet.Data;

namespace Closet.Services.Implementations
{
    public class MemeService : IMemeService
    {
        private readonly ClosetDbContext db;

        public MemeService(ClosetDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<MemeListingServiceModel>> AllAsync(int page = 1)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateAsync(string userId, string title, string imageUrl)
        {
            Meme meme = new Meme
            {
                Title = title,
                ImageUrl = imageUrl
            };

            this.db.Add(meme);
            await this.db.SaveChangesAsync();
        }
    }
}
