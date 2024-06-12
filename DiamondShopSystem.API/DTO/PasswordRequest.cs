namespace DiamondShopSystem.API.DTO
{
    public class PasswordRequest
    {
        public int userId { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }
    }
}
