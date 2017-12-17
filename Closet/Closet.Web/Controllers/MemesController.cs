using Closet.Data.Models;
using Closet.Services;
using Closet.Web.Infrastructure.Filters;
using Closet.Web.Models.Memes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Closet.Web.Controllers
{   
    [Authorize]
    public class MemesController : Controller
    {
        private readonly IMemeService memes;
        private readonly UserManager<User> userManager;

        public MemesController(IMemeService memes, UserManager<User> userManager)
        {
            this.memes = memes;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All(int page = 1)
            => View();

        public IActionResult Create()
            => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create([FromBody] MemeCreateViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            await this.memes.CreateAsync(userId, model.Title, model.ImageUrl);

            return RedirectToAction(nameof(All));
        }
    }
}
