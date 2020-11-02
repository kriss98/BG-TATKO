namespace BGTATKO.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BGTATKO.Services.Data.Contracts;
    using BGTATKO.Web.ViewModels.Categories;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        private readonly IPostsService postsService;

        public CategoriesController(ICategoriesService categoriesService, IPostsService postsService)
        {
            this.categoriesService = categoriesService;
            this.postsService = postsService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoriesService.CreateAsync(input.Name, input.Description, input.ImageUrl);

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> ById(int id, int page = 1)
        {
            var viewModel = await this.categoriesService.GetByIdAsync<CategoryByIdViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.ForumPosts = this.postsService.GetAllByCategoryId<PostInCategoryViewModel>(viewModel.Id, 10, (page - 1) * 10);
            var count = this.postsService.GetPostsCountByCategoryId(viewModel.Id);
            viewModel.PagesCount = (int) Math.Ceiling((double) count / 10);
            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Manage()
        {
            var viewModel = new ManageCategoriesViewModel
            {
                Categories = this.categoriesService.GetAll<CategoryViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.categoriesService.CategoryExists(id))
            {
                return this.BadRequest();
            }

            var viewModel = await this.categoriesService.GetByIdAsync<EditCategoryInputModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(EditCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!await this.categoriesService.CategoryExists(input.Id))
            {
                return this.BadRequest();
            }

            await this.categoriesService.EditAsync(input.Name, input.Description, input.ImageUrl, input.Id);

            return this.RedirectToAction("ById", "Categories", new { Id = input.Id });
        }
    }
}
