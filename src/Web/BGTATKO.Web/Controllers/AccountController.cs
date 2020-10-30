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

        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly IVotesService votesService;

        private readonly IVotesForCommentsService votesForCommentsService;

        private readonly IUsersService usersService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IVotesService votesService, IVotesForCommentsService votesForCommentsService, IUsersService usersService)
        {
            this.userManager = userManager;
            this.votesService = votesService;
            this.votesForCommentsService = votesForCommentsService;
            this.usersService = usersService;
            this.signInManager = signInManager;
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

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassowrd(ChangePasswordInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user wid ID '{this.userManager.GetUserId(this.User)}'");
            }

            var changePasswordResult =
                await this.userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    this.ModelState.AddModelError(error.Code, error.Description);
                }

                return this.View(input);
            }

            await this.signInManager.RefreshSignInAsync(user);

            return this.RedirectToAction("Manage", "Account");
        }
    }
}
