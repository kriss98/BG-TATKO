namespace BGTATKO.Web.Controllers
{
    using System.Threading.Tasks;
    using Common;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;
    using ViewModels.Posts;

    public class PostsController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        private readonly IPostsService postsService;

        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(ICategoriesService categoriesService, IPostsService postsService, UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.postsService = postsService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreatePostInputModel
            {
                Categories = this.categoriesService.GetAll<CategoryDropdownViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePostInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var id = await this.postsService.CreateAsync(input.Title, input.Content, input.CategoryId, user.Id);

            return this.RedirectToAction("ById", "Posts", new {Id = id});
        }

        [HttpGet]
        public async Task<IActionResult> ById(int id)
        {
            var viewModel = await this.postsService.GetPostByIdAsync<PostViewModel>(id);

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.postsService.PostExists(id))
            {
                return this.NotFound();
            }

            var viewModel = await this.postsService.GetPostByIdAsync<EditPostInputModel>(id);
            var user = await this.userManager.GetUserAsync(this.User);
            var isAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);

            if (user.Id != viewModel.UserId && !isAdmin)
            {
                return this.BadRequest();
            }

            viewModel.Categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditPostInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!await this.postsService.PostExists(input.Id))
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var isAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);

            await this.postsService.EditAsync(input.Title, input.Content, input.CategoryId, input.Id, user.Id, isAdmin);

            return this.RedirectToAction("ById", "Posts", new { Id = input.Id });
        }
    }
}
