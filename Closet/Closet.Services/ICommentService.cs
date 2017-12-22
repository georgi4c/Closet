using Closet.Services.Models;
using System;
using System.Threading.Tasks;

namespace Closet.Services
{
    public interface ICommentService
    {
        Task CreateAsync(string authorId, string content, int? memeId, int? parentCommentId);

        Task<int?> MemeId(int commentId);

        Task Delete(int id);

        Task<string> AuthorId(int id);
    }
}