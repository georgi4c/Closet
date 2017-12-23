using Closet.Data;
using Closet.Data.Models;
using Closet.Services;
using Closet.Tests.Mocks;
using Closet.Web.Controllers;
using Closet.Web.Models.Memes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

using static Closet.Web.WebConstants;

namespace Closet.Tests.Web.Controllers
{
    public class MemeControllerTests
    {
        private ClosetDbContext GetDbContext()
            => new ClosetDbContext(
                new DbContextOptionsBuilder<ClosetDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns("1");

            return userManager;
        }

        [Fact]
        public void MemesControllerShould_BeOnlyForRegisteredUsers()
        {
            // Arrange
            var controller = typeof(MemesController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            Assert.NotNull(areaAttribute);
        }

        [Fact]
        public async Task PostCreateShouldReturnRedirectWithValidModel()
        {
            // Arrange
            string titleValue = "TestMeme";
            string imageUrlValue = "test.com/dadsefaef";

            string modelUserId = null;
            string modelTitle = null;
            string modelImageUrl = null;
            string successMessage = null;

            var userManager = this.GetUserManagerMock();

            var memeService = new Mock<IMemeService>();
            memeService
                .Setup(c => c.CreateAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Callback((string userId, string title, string imageUrl) =>
                {
                    modelUserId = userId;
                    modelTitle = title;
                    modelImageUrl = imageUrl;
                })
                .Returns(Task.CompletedTask);

            var tempData = new Mock<ITempDataDictionary>();

            tempData
                .SetupSet(t => t[TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => successMessage = message as string);

            var controller = new MemesController(memeService.Object, userManager.Object);
            controller.TempData = tempData.Object;

            // Act
            var result = await controller.Create(new MemeCreateViewModel
            {
                Title = titleValue,
                ImageUrl = imageUrlValue
            });

            // Assert

            Assert.Equal(titleValue, modelTitle);
            Assert.Equal(imageUrlValue, modelImageUrl);

            Assert.Equal("Meme created successfully!", successMessage);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("All", (result as RedirectToActionResult).ActionName);
            Assert.Equal(null , (result as RedirectToActionResult).ControllerName);           
        }

        [Fact]
        public void MemesControllerAllShould_BeForRegisteredUsersAndGuests()
        {
            // Arrange
            var controller = typeof(MemesController);
            var method = controller.GetMethods().Where(m => m.Name == "All").FirstOrDefault();

            // Act
            Assert.NotNull(method);
            var allowGuestsAttribute = method
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AllowAnonymousAttribute))
                as AllowAnonymousAttribute;

            // Assert
            Assert.NotNull(allowGuestsAttribute);
        }
    }
}
