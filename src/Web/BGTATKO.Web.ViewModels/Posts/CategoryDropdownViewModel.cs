namespace BGTATKO.Web.ViewModels.Posts
{
    using Data.Models;
    using Services.Mapping;

    public class CategoryDropdownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
