using Model.Models;

namespace Services.OtherServices
{
    public interface IEmailRelatedService
    {
        string GeneratedPinCode();

        string PinCodeHtmlContent(string email, string pinCode);

        string ResetPasswordContent(string email, string resetUrl);
    }
}
