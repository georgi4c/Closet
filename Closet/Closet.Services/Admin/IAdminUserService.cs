using Closet.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Closet.Services.Admin
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync();
    }
}
