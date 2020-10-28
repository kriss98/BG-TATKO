namespace BGTATKO.Web.ViewModels.Categories
{
    using System.Collections.Generic;
    using Data.Models;
    using Services.Mapping;

    public class CategoryByIdViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<PostInCategoryViewModel> ForumPosts { get; set; }
    }
}
