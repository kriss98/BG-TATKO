namespace BGTATKO.Data.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Vote : BaseModel<int>
    {
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [DefaultValue(VoteType.Neutral)]
        public VoteType Type { get; set; }
    }
}
