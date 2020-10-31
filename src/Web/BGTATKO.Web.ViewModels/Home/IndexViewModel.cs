namespace BGTATKO.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using Posts;
    using Users;

    public class IndexViewModel
    {
        public IEnumerable<ViewUserViewModel> TopCommenters { get; set; }

        public IEnumerable<ViewUserViewModel> TopPosters { get; set; }

        public IEnumerable<PostViewModel> TopPosts { get; set; }
    }
}
