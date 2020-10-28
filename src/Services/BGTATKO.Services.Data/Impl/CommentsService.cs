namespace BGTATKO.Services.Data.Impl
{
    using System.Linq;
    using System.Threading.Tasks;
    using BGTATKO.Data.Common.Repositories;
    using BGTATKO.Data.Models;
    using Contracts;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task CreateAsync(int postId, string content, string userId, int? parentId = null)
        {
            var comment = new Comment
            {
                PostId = postId,
                Content = content,
                UserId = userId,
                ParentId = parentId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public bool IsInPostId(int commentId, int postId)
        {
            var commentPostId = this.commentsRepository.All().Where(x => x.Id == commentId).Select(x => x.PostId)
                .FirstOrDefault();

            return commentPostId == postId;
        }
    }
}
