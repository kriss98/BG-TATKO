namespace BGTATKO.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Services.Mapping;

    public class EditCommentInputModel : IMapFrom<Comment>, IMapTo<Comment>
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
