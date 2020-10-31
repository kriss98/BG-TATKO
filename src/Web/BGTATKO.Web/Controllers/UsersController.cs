namespace BGTATKO.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;
    using ViewModels.Users;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        private readonly IPostsService postsService;

        private readonly IVotesService votesService;

        private readonly IVotesForCommentsService votesForCommentsService;

        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersService usersService, IPostsService postsService, IVotesService votesService, IVotesForCommentsService votesForCommentsService, UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.postsService = postsService;
            this.votesService = votesService;
            this.votesForCommentsService = votesForCommentsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Show(string id, int page = 1)
        {
            var viewModel = await this.usersService.GetUserByIdAsync<ViewUserViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.ForumPosts = this.postsService.GetByUserId<PostFromUserViewModel>(id, 10, (page - 1) * 10);
            var count = this.postsService.GetPostsCountByUserId(id);
            viewModel.PagesCount = (int) Math.Ceiling((double) count / 10);
            viewModel.CurrentPage = page;
            viewModel.PostsKarma = this.votesService.GetAllVotesForUserPost(id);
            viewModel.CommentsKarma = this.votesForCommentsService.GetAllVotesForUserComment(id);

            var user = await this.userManager.GetUserAsync(this.User);
            viewModel.IsFollowedByCurrentUser = this.usersService.CheckIfUserFollowsUser(user.Id, id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Follow(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.Id == id)
            {
                return this.BadRequest();
            }

            await this.usersService.FollowUser(id, user.Id);

            return this.RedirectToAction("Show", "Users", new { id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Unfollow(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.Id == id)
            {
                return this.BadRequest();
            }

            await this.usersService.UnfollowUser(id, user.Id);

            return this.RedirectToAction("Show", "Users", new {id});
        }
    }
}
