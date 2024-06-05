using Model.Models;

namespace Services.EmailServices
{
    public interface IEmailService
    {
        void SendEmail(Email request);
    }
}
