namespace BGTATKO.Web.ViewModels.Comments
{
    using Data.Models;
    using Services.Mapping;

    public class DeleteCommentInputModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int PostId { get; set; }
    }
}
