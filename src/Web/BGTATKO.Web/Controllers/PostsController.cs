namespace BGTATKO.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
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
        public IActionResult Create()
        {
            var viewModel = new CreatePostInputModel
            {
                Categories = this.categoriesService.GetAll<CategoryDropdownViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.postsService.CreateAsync(input.Title, input.Content, input.CategoryId, user.Id);

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> ById(int id)
        {
            var viewModel = await this.postsService.GetPostByIdAsync<PostViewModel>(id);

            return this.View(viewModel);
        }
    }
}
