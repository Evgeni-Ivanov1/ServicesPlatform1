using System.ComponentModel.DataAnnotations;
using ReservationPlatform.Common;

namespace ServicesPlatform.Models.ViewModels
{
    public class ManageAccountViewModel
    {
  
        public string Email { get; set; }
      
        [MaxLength(ValidationConstants.PhoneNumberMaxLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [MaxLength(ValidationConstants.ManageNameMaxLength)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        // Properties for Changing Email
        [EmailAddress]
        [Display(Name = "New Email")]
        public string NewEmail { get; set; }

        // Properties for Changing Password
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
