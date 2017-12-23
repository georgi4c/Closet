using Closet.Data.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;

namespace Closet.Tests.Mocks
{
    public class UserManagerMock
    {
        public static Mock<UserManager<User>> New
            => new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    }
}
