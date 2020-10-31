namespace BGTATKO.Services.Data.Impl
{
    using System.Linq;
    using System.Threading.Tasks;
    using BGTATKO.Data.Common.Repositories;
    using BGTATKO.Data.Models;
    using Contracts;
    using Microsoft.EntityFrameworkCore;

    public class VotesForCommentsService : IVotesForCommentsService
    {
        private readonly IRepository<VoteForComment> voteForCommentRepository;

        public VotesForCommentsService(IRepository<VoteForComment> voteForCommentRepository)
        {
            this.voteForCommentRepository = voteForCommentRepository;
        }

        public async Task VoteAsync(int commentId, string userId, bool isUpVote)
        {
            var vote = await this.voteForCommentRepository.All()
                .FirstOrDefaultAsync(x => x.CommentId == commentId && x.UserId == userId);
            var voteType = isUpVote ? VoteType.UpVote : VoteType.DownVote;

            if (vote != null)
            {
                vote.Type = vote.Type == voteType ? VoteType.Neutral : voteType;
            }
            else
            {
                vote = new VoteForComment
                {
                    CommentId = commentId,
                    UserId = userId,
                    Type = voteType,
                };

                await this.voteForCommentRepository.AddAsync(vote);
            }

            await this.voteForCommentRepository.SaveChangesAsync();
        }

        public int GetVotes(int commentId)
        {
            return this.voteForCommentRepository.All().Where(x => x.CommentId == commentId).Sum(x => (int)x.Type);
        }

        public async Task<int> GetVoteStatus(int commentId, string userId)
        {
            var vote = await this.voteForCommentRepository.All()
                .FirstOrDefaultAsync(x => x.CommentId == commentId && x.UserId == userId);

            if (vote == null)
            {
                return 0;
            }

            return (int)vote.Type;
        }

        public int GetAllVotesForUserComment(string userId)
        {
            var karma = this.voteForCommentRepository.All().Where(x => x.Comment.UserId == userId).Sum(x => (int) x.Type);

            return karma;
        }
    }
}
