namespace BGTATKO.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.Models;

    public class UserFollower : BaseDeletableModel<int>
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [Required]
        public string FollowerId { get; set; }

        [ForeignKey(nameof(FollowerId))]
        public ApplicationUser Follower { get; set; }
    }
}
