namespace BGTATKO.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using BGTATKO.Common;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Models;

    public class PostsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Posts.Any())
            {
                return;
            }

            var categories = dbContext.Categories.AsQueryable();
            var user = await dbContext.Users.FirstOrDefaultAsync();
            foreach (var category in categories)
            {
                for (int i = 0; i < 3; i++)
                {
                    await dbContext.Posts.AddAsync(new Post
                    {
                        Title = $"{category.Name} post {i}",
                        Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        UserId = user.Id,
                        CategoryId = category.Id,
                    });
                }
            }
        }
    }
}
