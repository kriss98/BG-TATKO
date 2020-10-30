namespace BGTATKO.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<T> GetUserByIdAsync<T>(string id);
    }
}
