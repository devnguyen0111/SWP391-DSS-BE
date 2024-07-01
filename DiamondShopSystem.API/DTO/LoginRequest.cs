namespace DiamondShopSystem.API.DTO
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsGoogleLogin { get; set; }
    }
}
