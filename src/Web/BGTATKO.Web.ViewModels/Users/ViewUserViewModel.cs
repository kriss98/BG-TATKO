namespace BGTATKO.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using Data.Models;
    using Services.Mapping;

    public class ViewUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ImageUrl { get; set; }

        public int CommentsCount { get; set; }

        public int PostsCount { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int PostsKarma { get; set; }

        public int CommentsKarma { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public bool IsFollowedByCurrentUser { get; set; }

        public IEnumerable<PostFromUserViewModel> ForumPosts { get; set; }
    }
}
