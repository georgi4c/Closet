using Closet.Data.Models;
using Closet.Services;
using Closet.Services.Implementations;
using Closet.Web.Infrastructure.Filters;
using Closet.Web.Models.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Closet.Web.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentService comments;
        private readonly UserManager<User> userManager;

        public CommentsController(ICommentService comments, UserManager<User> userManager)
        {
            this.comments = comments;
            this.userManager = userManager;
        }

        public IActionResult Create([FromQuery]int? memeId, [FromQuery]int? parentCommentId)
        {
            if ((memeId == null && parentCommentId == null) || (memeId != null && parentCommentId != null))
            {
                return BadRequest();
            }

            var model = new CommentCreateViewModel
            {
                MemeId = memeId,
                ParentCommentId = parentCommentId
            };

            return View(model);
        }

        [HttpPost]
        //[ValidateModelState]
        public async Task<IActionResult> Create(CommentCreateViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            await this.comments.CreateAsync(userId, model.Content, model.MemeId, model.ParentCommentId);

            if (model.MemeId == null)
            {
                model.MemeId = await this.comments.MemeId(model.ParentCommentId.Value);
            }
            
            return RedirectToAction("Details", "Memes", new { id = model.MemeId });
        }
    }
}
