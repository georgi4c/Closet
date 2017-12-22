using AutoMapper.QueryableExtensions;
using Closet.Data;
using Closet.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Closet.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private ClosetDbContext db;

        public AdminUserService(ClosetDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
            => await this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
    }
}
