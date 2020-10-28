namespace BGTATKO.Services.Data.Impl
{
    using System.Threading.Tasks;

    using BGTATKO.Data.Common.Repositories;
    using BGTATKO.Data.Models;
    using BGTATKO.Services.Data.Contracts;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(string name, string description, string imageUrl)
        {
            var category = new Category
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }
    }
}
