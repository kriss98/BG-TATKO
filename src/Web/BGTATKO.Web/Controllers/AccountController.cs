namespace BGTATKO.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;
    using ViewModels.Account;

    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IVotesService votesService;

        private readonly IVotesForCommentsService votesForCommentsService;

        private readonly IUsersService usersService;

        public AccountController(UserManager<ApplicationUser> userManager, IVotesService votesService, IVotesForCommentsService votesForCommentsService, IUsersService usersService)
        {
            this.userManager = userManager;
            this.votesService = votesService;
            this.votesForCommentsService = votesForCommentsService;
            this.usersService = usersService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Manage()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = await this.usersService.GetUserByIdAsync<ManageAccountViewModel>(user.Id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.PostsKarma = this.votesService.GetAllVotesForUserPost(user.Id);
            viewModel.CommentsKarma = this.votesForCommentsService.GetAllVotesForUserComment(user.Id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Manage(ManageAccountViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.usersService.UpdateAsync(input.FirstName, input.LastName, input.PhoneNumber, input.Email,
                input.ImageUrl, user.Id);

            return this.Redirect("/");
        }
    }
}
