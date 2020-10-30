namespace BGTATKO.Data.Seeding
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using System;
    using System.Threading.Tasks;
    using Models;

    public class CommentsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Comments.Any())
            {
                return;
            }

            var posts = dbContext.Posts.AsQueryable();
            var user = await dbContext.Users.FirstOrDefaultAsync();

            foreach (var post in posts)
            {
                for (int i = 0; i < 5; i++)
                {
                    await dbContext.Comments.AddAsync(new Comment
                    {
                        PostId = post.Id,
                        Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        UserId = user.Id,
                    });
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
