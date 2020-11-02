namespace BGTATKO.Services.Data.Impl
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BGTATKO.Data.Common.Repositories;
    using BGTATKO.Data.Models;
    using Contracts;
    using Mapping;
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

        public async Task DeleteAsync(int id)
        {
            var comment = await this.commentsRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            this.commentsRepository.Delete(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllCommentChildrenById<T>(int id)
        {
            var allChildren = new List<T>();
            var comment = await this.commentsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
            allChildren = await this.GetChildren(id, allChildren);
            allChildren.Add(comment);

            return allChildren;
        }

        private async Task<List<T>> GetChildren<T>(int id, List<T> allChildren)
        {
            var children = this.commentsRepository.All().Where(x => x.ParentId == id);

            foreach (var child in children)
            {
                await GetChildren<T>(child.Id, allChildren);
                allChildren.Add(await this.commentsRepository.All().Where(x => x.Id == child.Id).To<T>().FirstOrDefaultAsync());
            }

            return allChildren;
        }
    }
}
