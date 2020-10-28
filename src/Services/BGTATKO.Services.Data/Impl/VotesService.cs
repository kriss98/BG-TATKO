namespace BGTATKO.Services.Data.Impl
{
    using System.Threading.Tasks;
    using Contracts;

    public class VotesService : IVotesService
    {
        public Task VoteAsync(int postId, string userId, bool isUpVote)
        {
            throw new System.NotImplementedException();
        }

        public int GetVotes(int postId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetVoteStatus(int postId, string userId)
        {
            throw new System.NotImplementedException();
        }

        public int GetAllVotesForUserPost(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
