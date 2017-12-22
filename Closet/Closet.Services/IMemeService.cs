using Closet.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Closet.Services
{
    public interface IMemeService
    {
        Task<IEnumerable<MemeListingServiceModel>> AllAsync(int page = 1);

        Task CreateAsync(string userId, string title, string imageUrl);

        Task<MemeWithCommentsServiceModel> WithCommentsById(int id);

        Task Edit(int id, string title, string imageUrl);

        Task<bool> Delete(int id);

        Task<int> AddOrUpdateVote(int memeId, string userId, int value);

        Task<int> Votes(int memeId);

        Task<IEnumerable<MemeTopThreeServiceModel>> TopThree();

        Task<MemeMinifiedModel> ById(int id);

        Task<string> AuthorId(int id);
    }
}
