namespace BGTATKO.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<T> GetUserByIdAsync<T>(string id);

        Task UpdateAsync(string firstName, string lastName, string phoneNumber, string email, string imageUrl, string id);
    }
}
