namespace BGTATKO.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BGTATKO.Common;
    using Microsoft.EntityFrameworkCore.Internal;
    using Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<(string Name, string ImageUrl)>
            {
                ("Sport", "https://www.von.gov.ng/wp-content/uploads/2020/04/sports.jpg"),
                ("COVID-19", "https://osis.bg/wp-content/uploads/2020/03/covid-19.jpg"),
                ("News", "https://newzradar.com/wp-content/uploads/Breaking-News-September-8-2020-As-it-happened.jpg"),
                ("Music", "https://images.macrumors.com/t/MKlRm9rIBpfcGnjTpf6ZxgpFTUg=/1600x1200/smart/article-new/2018/05/apple-music-note.jpg"),
                ("Cats", "https://undark.org/wp-content/uploads/2020/02/GettyImages-1199242002-1-scaled.jpg"),
                ("Dogs", "https://www.sciencemag.org/sites/default/files/styles/article_main_large/public/dogs_1280p_0.jpg?itok=cnRk0HYq"),
                ("Programming", "https://hub.packtpub.com/wp-content/uploads/2018/05/programming.jpg"),
            };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category.Name,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    ImageUrl = category.ImageUrl,
                });
            }
        }
    }
}
