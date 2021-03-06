﻿namespace BGTATKO.Services.Data.Impl
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BGTATKO.Data.Common.Repositories;
    using BGTATKO.Data.Models;
    using BGTATKO.Services.Data.Contracts;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        private readonly IDeletableEntityRepository<Post> postRepository;

        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository, IDeletableEntityRepository<Post> postRepository, IDeletableEntityRepository<Comment> commentRepository)
        {
            this.categoryRepository = categoryRepository;
            this.postRepository = postRepository;
            this.commentRepository = commentRepository;
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

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.categoryRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query = this.categoryRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<bool> CategoryExists(int id)
        {
            return await this.categoryRepository.All().FirstOrDefaultAsync(x => x.Id == id) != null;
        }

        public async Task EditAsync(string name, string description, string imageUrl, int id)
        {
            var category = await this.categoryRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            category.Name = name;
            category.Description = description;
            category.ImageUrl = imageUrl;

            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await this.categoryRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            foreach (var post in this.postRepository.All().Where(x => x.CategoryId == category.Id))
            {
                foreach (var comment in this.commentRepository.All().Where(x => x.PostId == post.Id))
                {
                    this.commentRepository.Delete(comment);
                }

                this.postRepository.Delete(post);
            }

            this.categoryRepository.Delete(category);
            await this.categoryRepository.SaveChangesAsync();
        }
    }
}
