﻿namespace BGTATKO.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BGTATKO.Services.Data.Contracts;
    using BGTATKO.Web.ViewModels.Categories;
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
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
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
    }
}