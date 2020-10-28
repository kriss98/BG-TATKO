namespace BGTATKO.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Services.Mapping;

    public class CreateCategoryInputModel : IMapTo<Category>
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(1024, MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}
