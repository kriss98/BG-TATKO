namespace BGTATKO.Services.Data.Contracts
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task CreateAsync(string name, string description, string imageUrl);

        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
