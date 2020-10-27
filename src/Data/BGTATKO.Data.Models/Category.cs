namespace BGTATKO.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
