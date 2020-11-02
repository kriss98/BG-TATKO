namespace BGTATKO.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task<int> CreateAsync(string title, string content, int categoryId, string userId);

        IEnumerable<T> GetAllByCategoryId<T>(int categoryId, int take, int skip = 0);

        int GetPostsCountByCategoryId(int categoryId);

        Task<T> GetPostByIdAsync<T>(int id);

        IEnumerable<T> GetByUserId<T>(string userId, int? take = null, int skip = 0);

        int GetPostsCountByUserId(string userId);

        IEnumerable<T> GetAllPosts<T>();

        Task<bool> PostExists(int id);

        Task EditAsync(string title, string content, int categoryId, int id);

        Task DeleteAsync(int id);

        Task<bool> IsUserPostAuthor(int postId, string userId);
    }
}
