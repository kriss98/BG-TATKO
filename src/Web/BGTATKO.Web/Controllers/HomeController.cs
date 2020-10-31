namespace BGTATKO.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using BGTATKO.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;
    using ViewModels.Categories;
    using ViewModels.Home;
    using ViewModels.Posts;
    using ViewModels.Users;

    public class HomeController : BaseController
    {
        private readonly IUsersService usersService;

        private readonly IPostsService postsService;

        private readonly IVotesService votesService;

        private readonly IVotesForCommentsService votesForCommentsService;

        public HomeController(IUsersService usersService, IPostsService postsService, IVotesService votesService, IVotesForCommentsService votesForCommentsService)
        {
            this.usersService = usersService;
            this.postsService = postsService;
            this.votesService = votesService;
            this.votesForCommentsService = votesForCommentsService;
        }

        public IActionResult Index()
        {
            var allUsers = this.usersService.GetAllUsers<ViewUserViewModel>();

            foreach (var user in allUsers)
            {
                user.PostsKarma = this.votesService.GetAllVotesForUserPost(user.Id);
                user.CommentsKarma = this.votesForCommentsService.GetAllVotesForUserComment(user.Id);
            }

            var viewModel = new IndexViewModel
            {
                TopCommenters = allUsers.OrderByDescending(x => x.CommentsKarma).Take(5),
                TopPosters = allUsers.OrderByDescending(x => x.PostsKarma).Take(5),
                TopPosts = this.postsService.GetAllPosts<PostViewModel>().OrderByDescending(x => x.VotesCount).Take(10),
            };

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
