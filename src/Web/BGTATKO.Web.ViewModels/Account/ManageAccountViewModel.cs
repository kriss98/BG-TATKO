namespace BGTATKO.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Services.Mapping;

    public class ManageAccountViewModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
         public string Id { get; set; }

         public string UserName { get; set; }

         [MinLength(2)]
         [Display(Name = "First name")]
         public string FirstName { get; set; }

         [MinLength(2)]
         [Display(Name = "Last name")]
         public string LastName { get; set; }

         [Phone]
         [Display(Name = "Phone number")]
         public string PhoneNumber { get; set; }

         [Required]
         [EmailAddress]
         public string Email { get; set; }

         [Url]
         [Display(Name = "Image URL")]
         public string ImageUrl { get; set; }

         public int PostsCount { get; set; }

         public int CommentsCount { get; set; }

         public int PostsKarma { get; set; }

         public int CommentsKarma { get; set; }

         public int FollowersCount { get; set; }

         public int FollowingCount { get; set; }
    }
}
