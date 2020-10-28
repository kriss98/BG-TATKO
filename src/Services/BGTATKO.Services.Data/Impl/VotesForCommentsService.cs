namespace BGTATKO.Services.Data.Impl
{
    using System.Threading.Tasks;
    using Contracts;

    public class VotesForCommentsService : IVotesForCommentsService
    {
        public Task VoteAsync(int commentId, string userId, bool isUpVote)
        {
            throw new System.NotImplementedException();
        }

        public int GetVotes(int commentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetVoteStatus(int commentId, string userId)
        {
            throw new System.NotImplementedException();
        }

        public int GetAllVotesForUserComment(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
