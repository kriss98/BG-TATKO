namespace BGTATKO.Web.ViewModels.Users
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class PostFromUserViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string TimeSincePosted => this.CreatedOn.Date == DateTime.UtcNow.Date
            ? "Today"
            : $"{(DateTime.UtcNow.Date - this.CreatedOn.Date).TotalDays} days ago";

        public int CommentsCount { get; set; }
    }
}
