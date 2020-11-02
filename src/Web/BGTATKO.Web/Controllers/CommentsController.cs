namespace BGTATKO.Web.Controllers
{
    using System.Threading.Tasks;
    using Common;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;
    using ViewModels.Comments;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;

        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            int? parentId = input.ParentId == 0 ? (int?)null : input.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInPostId(parentId.Value, input.PostId))
                {
                    return this.BadRequest();
                }
            }

            await this.commentsService.CreateAsync(input.PostId, input.Content, user.Id, parentId);

            return this.RedirectToAction("ById", "Posts", new {Id = input.PostId});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditCommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            if (!await this.commentsService.CommentExists(input.Id))
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var isAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);

            if (!await this.commentsService.IsUserCommentAuthor(input.Id, user.Id) && !isAdmin)
            {
                return this.BadRequest();
            }

            await this.commentsService.EditAsync(input.Content, input.Id);

            return this.RedirectToAction("ById", "Posts", new { Id = input.PostId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteCommentInputModel input)
        {
            if (!await this.commentsService.CommentExists(input.Id))
            {
                return this.NotFound();
            }

            var commentAndChildren =
                await this.commentsService.GetAllCommentChildrenById<DeleteCommentInputModel>(input.Id);

            var user = await this.userManager.GetUserAsync(this.User);
            var isAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);

            foreach (var comment in commentAndChildren)
            {
                if (!await this.commentsService.IsUserCommentAuthor(comment.Id, user.Id) && !isAdmin)
                {
                    return this.BadRequest();
                }

                await this.commentsService.DeleteAsync(comment.Id);
            }

            return this.RedirectToAction("ById", "Posts", new { Id = input.PostId });
        }
    }
}
