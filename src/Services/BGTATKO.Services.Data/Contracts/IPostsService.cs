namespace BGTATKO.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task CreateAsync(string title, string content, int categoryId, string userId);
    }
}
