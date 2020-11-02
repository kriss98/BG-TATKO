namespace BGTATKO.Services.Data.Contracts
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task CreateAsync(string name, string description, string imageUrl);

        Task<T> GetByIdAsync<T>(int id);

        IEnumerable<T> GetAll<T>(int? count = null);

        Task<bool> CategoryExists(int id);

        Task EditAsync(string name, string description, string imageUrl, int id);
    }
}
