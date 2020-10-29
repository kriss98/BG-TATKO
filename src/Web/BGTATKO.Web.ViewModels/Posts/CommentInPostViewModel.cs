namespace BGTATKO.Web.ViewModels.Posts
{
    using System;
    using System.Linq;

    using AutoMapper;
    using BGTATKO.Data.Models;
    using BGTATKO.Services.Mapping;
    using Ganss.XSS;

    public class CommentInPostViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }

        public string UserId { get; set; }

        public string UserImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public int VoteForCommentCount { get; set; }

        public string CreatedOnString => this.CreatedOn.Date.ToString("MMM dd, yyyy");

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentInPostViewModel>()
                .ForMember(x => x.VoteForCommentCount, options =>
                {
                    options.MapFrom(c => c.Votes.Sum(v => (int)v.Type));
                });
        }
    }
}
