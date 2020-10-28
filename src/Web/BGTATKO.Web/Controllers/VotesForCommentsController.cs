namespace BGTATKO.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/{controller}")]
    public class VotesForCommentsController : ControllerBase
    {
        private readonly IVotesForCommentsService votesForCommentsService;

        private readonly UserManager<ApplicationUser> userManager;

        public VotesForCommentsController(IVotesForCommentsService votesForCommentsService, UserManager<ApplicationUser> userManager)
        {
            this.votesForCommentsService = votesForCommentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VoteForCommentResponseModel>> Post(VoteForCommentInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.votesForCommentsService.VoteAsync(inputModel.CommentId, user.Id, inputModel.IsUpVote);
            var votes = this.votesForCommentsService.GetVotes(inputModel.CommentId);

            return new VoteForCommentResponseModel
            {
                VotesCount = votes,
            };
        }

        [HttpGet("{commentId}")]
        [Authorize]
        public async Task<ActionResult<int>> Get(int commentId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var voteStatus = await this.votesForCommentsService.GetVoteStatus(commentId, user.Id);

            return voteStatus;
        }
    }
}
