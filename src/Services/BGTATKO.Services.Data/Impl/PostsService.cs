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

        public IEnumerable<T> GetAllPosts<T>()
        {
            return this.postsRepository.All().To<T>().ToList();
        }

        public async Task<bool> PostExists(int id)
        {
            return await this.postsRepository.All().FirstOrDefaultAsync(x => x.Id == id) != null;
        }

        public async Task EditAsync(string title, string content, int categoryId, int id)
        {
            var post = await this.postsRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            post.Title = title;
            post.Content = content;
            post.CategoryId = categoryId;

            this.postsRepository.Update(post);
            await this.postsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = await this.postsRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            this.postsRepository.Delete(post);
            await this.postsRepository.SaveChangesAsync();
        }

        public async Task<bool> IsUserPostAuthor(int postId, string userId)
        {
            var post = await this.postsRepository.All().FirstOrDefaultAsync(x => x.Id == postId);

            return userId == post.UserId;
        }
    }
}
