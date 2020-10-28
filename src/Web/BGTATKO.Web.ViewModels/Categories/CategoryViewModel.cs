namespace BGTATKO.Web.ViewModels.Categories
{
    using Data.Models;
    using Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ShortDescription => this.Description.Length > 500
            ? this.Description.Substring(0, 500) + "..."
            : this.Description;
    }
}
