namespace BGTATKO.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }
    }
}
