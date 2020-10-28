namespace BGTATKO.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Services.Mapping;

    public class CreatePostInputModel : IMapTo<Post>, IMapFrom<Post>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }
    }
}
