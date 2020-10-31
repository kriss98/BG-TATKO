namespace BGTATKO.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<T> GetUserByIdAsync<T>(string id);

        Task UpdateAsync(string firstName, string lastName, string phoneNumber, string email, string imageUrl, string id);

        Task FollowUser(string followerId, string id);

        Task UnfollowUser(string followerId, string id);

        bool CheckIfUserFollowsUser(string currentUserId, string userId);
    }
}
