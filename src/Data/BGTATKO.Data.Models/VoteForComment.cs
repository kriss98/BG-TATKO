namespace BGTATKO.Data.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class VoteForComment : BaseModel<int>
    {
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [DefaultValue(VoteType.Neutral)]
        public VoteType Type { get; set; }
    }
}
