using System.ComponentModel.DataAnnotations;

namespace DiamondShopSystem.API.DTO
{
    public class PasswordRequest
    {

        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Old password is required.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is required.")]
        public string ConfirmNewPassword { get; set; }
    }
    public class PasswordRequestForgor
    {

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is required.")]
        public string ConfirmNewPassword { get; set; }
    }
}
