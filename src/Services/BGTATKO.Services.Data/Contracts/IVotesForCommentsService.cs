namespace BGTATKO.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IVotesForCommentsService
    {
        Task VoteAsync(int commentId, string userId, bool isUpVote);

        int GetVotes(int commentId);

        Task<int> GetVoteStatus(int commentId, string userId);

        int GetAllVotesForUserComment(string userId);
    }
}
