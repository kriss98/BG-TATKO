namespace BGTATKO.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/{controller}")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;

        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(IVotesService votesService, UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.votesService.VoteAsync(input.PostId, user.Id, input.IsUpVote);
            var votes = this.votesService.GetVotes(input.PostId);

            return new VoteResponseModel
            {
                VotesCount = votes,
            };
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<int>> Get(int postId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var voteStatus = await this.votesService.GetVoteStatus(postId, user.Id);

            return voteStatus;
        }
    }
}
