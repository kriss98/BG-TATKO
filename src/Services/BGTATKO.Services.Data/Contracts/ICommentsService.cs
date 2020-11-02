namespace BGTATKO.Services.Data.Contracts
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task CreateAsync(int postId, string content, string userId, int? parentId = null);

        bool IsInPostId(int commentId, int postId);

        Task<bool> CommentExists(int id);

        Task<bool> IsUserCommentAuthor(int commentId, string userId);

        Task EditAsync(string content, int id);

        Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllCommentChildrenById<T>(int id);
    }
}
