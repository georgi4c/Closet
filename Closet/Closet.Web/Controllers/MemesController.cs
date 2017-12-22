using Closet.Data.Models;
using Closet.Services;
using Closet.Services.Models;
using Closet.Web.Infrastructure.Extesions;
using Closet.Web.Infrastructure.Filters;
using Closet.Web.Models.Memes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using static Closet.Web.WebConstants;

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

        [AllowAnonymous]
        public async Task<IActionResult> All(int page = 1)
            => View(await this.memes.AllAsync(page));

        public IActionResult Create()
            => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(MemeCreateViewModel model)
        {
            //string apiKey = "cc08605624883c548f7fd6c8b8eed713";
            //string sharedSecret = "396d24e278293b56";

            //var flickr = FlickrManager.GetInstance();

            var userId = this.userManager.GetUserId(User);

            await this.memes.CreateAsync(userId, model.Title, model.ImageUrl);

            return RedirectToAction(nameof(All));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details([FromRoute]int id)
        {
            var memes = await this.memes.WithCommentsById(id);
            return this.ViewOrNotFound(memes);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddOrUpdateVote(MemeVoteViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            return this.Ok(await this.memes.AddOrUpdateVote(model.MemeId, userId, model.Value));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var memeAuthorId = await this.memes.AuthorId(id);
            if (memeAuthorId != userId && !User.IsInRole(AdministratorRole))
            {
                return BadRequest();
            }

            return View(await this.memes.ById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MemeMinifiedModel model)
        {
            var userId = this.userManager.GetUserId(User);
            var memeAuthorId = await this.memes.AuthorId(id);
            if (memeAuthorId != userId && !User.IsInRole(AdministratorRole))
            {
                return BadRequest();
            }

            await this.memes.Edit(id, model.Title, model.ImageUrl);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var memeAuthorId = await this.memes.AuthorId(id);
            if (memeAuthorId != userId && !User.IsInRole(AdministratorRole))
            {
                return BadRequest();
            }

            return View(await this.memes.ById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, MemeMinifiedModel model)
        {
            var userId = this.userManager.GetUserId(User);
            var memeAuthorId = await this.memes.AuthorId(id);
            if (memeAuthorId != userId && !User.IsInRole(AdministratorRole))
            {
                return BadRequest();
            }

            var success = await this.memes.Delete(id);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
