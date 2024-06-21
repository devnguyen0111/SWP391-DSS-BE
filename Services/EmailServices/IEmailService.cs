namespace Services.EmailServices
{
    public interface IEmailService
    {
        void SendEmail(Email request);
        void SendPinCode(string request, string pin);
        void SendConfirmationLink(string request, string url);
        void SendAttachment(string request);
        void SendResetLink(string request, string url);
        void SendGIA(string request);
    }
}
