namespace BGTATKO.Services.Data.Impl
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BGTATKO.Data.Common.Repositories;
    using BGTATKO.Data.Models;
    using Contracts;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<int> CreateAsync(string title, string content, int categoryId, string userId)
        {
            var post = new Post
            {
                Title = title,
                Content = content,
                CategoryId = categoryId,
                UserId = userId,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();

            return post.Id;
        }

        public IEnumerable<T> GetAllByCategoryId<T>(int categoryId, int take, int skip)
        {
            return this.postsRepository.All().Where(x => x.CategoryId == categoryId).Skip(skip).Take(take)
                .To<T>().ToList();
        }

        public int GetPostsCountByCategoryId(int categoryId)
        {
            return this.postsRepository.All().Count(x => x.CategoryId == categoryId);
        }

        public async Task<T> GetPostByIdAsync<T>(int id)
        {
            return await this.postsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
        }

        public IEnumerable<T> GetByUserId<T>(string userId, int? take = null, int skip = 0)
        {
            var query = this.postsRepository.All().OrderByDescending(x => x.CreatedOn).Where(x => x.UserId == userId)
                .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public int GetPostsCountByUserId(string userId)
        {
            return this.postsRepository.All().Count(x => x.UserId == userId);
        }
    }
}
