namespace BGTATKO.Web.ViewModels.Posts
{
    using System;
    using Data.Models;
    using Ganss.XSS;
    using Services.Mapping;

    public class PostViewModel : IMapTo<Post>, IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }

        public string UserId { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public int CommentsCount { get; set; }

        public int UserCommentsCount { get; set; }

        public int UserPostsCount { get; set; }

        public string UserImageUrl { get; set; }

        public DateTime UserCreatedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public string TimeSincePosted => this.CreatedOn.Date == DateTime.UtcNow.Date
            ? "TODAY"
            : $"{(DateTime.UtcNow.Date - this.CreatedOn.Date).TotalDays} DAYS AGO";
    }
}
