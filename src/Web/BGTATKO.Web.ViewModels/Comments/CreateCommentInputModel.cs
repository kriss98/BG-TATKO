namespace BGTATKO.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Services.Mapping;

    public class CreateCommentInputModel : IMapTo<Comment>
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        public string Content { get; set; }

        public int ParentId { get; set; }
    }
}
