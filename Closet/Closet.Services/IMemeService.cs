using Closet.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Closet.Services
{
    public interface IMemeService
    {
        Task<IEnumerable<MemeListingServiceModel>> AllAsync(int page = 1);

        Task CreateAsync(string userId, string title, string imageUrl);
    }
}
