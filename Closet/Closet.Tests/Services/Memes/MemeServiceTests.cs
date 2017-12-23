using Closet.Data;
using Closet.Data.Models;
using Closet.Services.Implementations;
using Closet.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

using static Closet.Services.ServiceConstants;

namespace Closet.Tests.Services.Memes
{
    public class MemeServiceTests
    {     
        public MemeServiceTests()
        {
            Tests.Initialize();
        }

        private const int MemeIdForDeleting = 1;

        private ClosetDbContext GetDbContext()
            => new ClosetDbContext(
                new DbContextOptionsBuilder<ClosetDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        private readonly IEnumerable<Meme> testData = new List<Meme>
        {
            new Meme{ Id = 4, Title = "Title1", ImageUrl = "/image1.jpg", CreatedOn = DateTime.Now, Author = new User{ Id = "xdas", UserName = "Ivan" }, Comments = new List<Comment>(), Votes = new List<MemeVote>()},
            new Meme{ Id = 2, Title = "Title2", ImageUrl = "/image2.jpg", CreatedOn = DateTime.Now, Author = new User{ Id = "xdas", UserName = "Ivan" }, Comments = new List<Comment>(), Votes = new List<MemeVote>()},
            new Meme{ Id = 5, Title = "Title3", ImageUrl = "/image3.jpg", CreatedOn = DateTime.Now, Author = new User{ Id = "xdas", UserName = "Vasil" }, Comments = new List<Comment>(), Votes = new List<MemeVote>()},
            new Meme{ Id = 1, Title = "Title4", ImageUrl = "/image4.jpg", CreatedOn = DateTime.Now, Author = new User{ Id = "xdas", UserName = "Ivan" }, Comments = new List<Comment>(), Votes = new List<MemeVote>()},
            new Meme{ Id = 3, Title = "Title5", ImageUrl = "/image5.jpg", CreatedOn = DateTime.Now, Author = new User{ Id = "xdas", UserName = "Ivan" }, Comments = new List<Comment>(), Votes = new List<MemeVote>()}
        };

        private void PopulateData(ClosetDbContext db)
        {
            db.AddRange(this.testData);
            db.SaveChanges();
        }

        private bool CompareMemesWithMemeListingServiceModelExact(MemeListingServiceModel thisMeme, Meme otherMeme)
            => thisMeme.Id == otherMeme.Id
            && thisMeme.Title == otherMeme.Title
            && thisMeme.ImageUrl == otherMeme.ImageUrl
            && thisMeme.CreatedOn == otherMeme.CreatedOn;

        [Fact]
        public async Task MemeServiceAllAsyncShould_ReturnsMemesForPageOneByDefault()
        {
            // Arrange

            var context = this.GetDbContext();

            this.PopulateData(context);

            var memeService = new MemeService(context);

            // Act

            var returnedData = await memeService.AllAsync();

            var expectedFirstPageMemes = context.Memes.OrderByDescending(m => m.CreatedOn).Take(MemesPageSize).ToList();

            // Assert

            foreach (var returnedModel in returnedData)
            {
                var testModel = expectedFirstPageMemes.First(n => returnedModel.Id == n.Id);

                Assert.NotNull(testModel);
                Assert.True(CompareMemesWithMemeListingServiceModelExact(returnedModel, testModel));
            }
        }

        [Fact]
        public async Task MemeServiceDeleteShould_DeleteEntry()
        {
            // Arrange
            
            var context = this.GetDbContext();

            this.PopulateData(context);

            var memeService = new MemeService(context);

            // Act

            var returnedData = await memeService.Delete(MemeIdForDeleting);

            // Assert
            Assert.True(!context.Memes.Any(m => m.Id == MemeIdForDeleting));

        }

    }
}
