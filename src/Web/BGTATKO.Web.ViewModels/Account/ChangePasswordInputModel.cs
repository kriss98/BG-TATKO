namespace BGTATKO.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "Confirm new password")]
        public string ConfirmNewPassword { get; set; }
    }
}
