namespace BGTATKO.Web.ViewModels.Categories
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class PostInCategoryViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserUserName { get; set; }

        public string UserId { get; set; }

        public int CommentsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public string TimeSincePosted => this.CreatedOn.Date == DateTime.UtcNow.Date
            ? "Today"
            : $"{(DateTime.UtcNow.Date - this.CreatedOn.Date).TotalDays} days ago";
    }
}
