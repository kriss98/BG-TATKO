namespace BGTATKO.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task CreateAsync(int postId, string content, string userId, int? parentId = null);

        bool IsInPostId(int commentId, int postId);

        Task<bool> CommentExists(int id);

        Task<bool> IsUserCommentAuthor(int commentId, string userId);

        Task EditAsync(string content, int id);
    }
}
