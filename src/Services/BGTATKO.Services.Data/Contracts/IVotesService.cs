namespace BGTATKO.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task VoteAsync(int postId, string userId, bool isUpVote);

        int GetVotes(int postId);

        Task<int> GetVoteStatus(int postId, string userId);

        int GetAllVotesForUserPost(string userId);
    }
}
