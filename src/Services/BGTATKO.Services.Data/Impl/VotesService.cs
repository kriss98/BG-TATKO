namespace BGTATKO.Services.Data.Impl
{
    using System.Linq;
    using System.Threading.Tasks;
    using BGTATKO.Data.Common.Repositories;
    using BGTATKO.Data.Models;
    using Contracts;
    using Microsoft.EntityFrameworkCore;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public async Task VoteAsync(int postId, string userId, bool isUpVote)
        {
            var vote = await this.votesRepository.All()
                .FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
            var voteType = isUpVote ? VoteType.UpVote : VoteType.DownVote;

            if (vote != null)
            {
                vote.Type = vote.Type == voteType ? VoteType.Neutral : voteType;
            }
            else
            {
                vote = new Vote
                {
                    PostId = postId,
                    UserId = userId,
                    Type = voteType,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }

        public int GetVotes(int postId)
        {
            return this.votesRepository.All().Where(x => x.PostId == postId).Sum(x => (int)x.Type);
        }

        public async Task<int> GetVoteStatus(int postId, string userId)
        {
            var vote = await this.votesRepository.All().FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);

            if (vote == null)
            {
                return 0;
            }

            return (int)vote.Type;
        }

        public int GetAllVotesForUserPost(string userId)
        {
            var karma = this.votesRepository.All().Where(x => x.Post.UserId == userId).Sum(x => (int) x.Type);

            return karma;
        }
    }
}
