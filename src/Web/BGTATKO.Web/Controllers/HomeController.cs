namespace BGTATKO.Web.Controllers
{
    using System.Diagnostics;

    using BGTATKO.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;
    using ViewModels.Categories;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public HomeController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var viewModel = new ListCategoriesViewModel
            {
                Categories = this.categoriesService.GetAll<CategoryViewModel>(),
            };

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
