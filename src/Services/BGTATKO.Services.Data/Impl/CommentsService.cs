namespace BGTATKO.Services.Data.Impl
{
    using System.Linq;
    using System.Threading.Tasks;
    using BGTATKO.Data.Common.Repositories;
    using BGTATKO.Data.Models;
    using Contracts;
    using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> CommentExists(int id)
        {
            return await this.commentsRepository.All().FirstOrDefaultAsync(x => x.Id == id) != null;
        }

        public async Task<bool> IsUserCommentAuthor(int commentId, string userId)
        {
            var comment = await this.commentsRepository.All().FirstOrDefaultAsync(x => x.Id == commentId);

            return comment.UserId == userId;
        }

        public async Task EditAsync(string content, int id)
        {
            var comment = await this.commentsRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            comment.Content = content;

            this.commentsRepository.Update(comment);
            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
