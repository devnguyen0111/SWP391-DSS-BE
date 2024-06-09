namespace Services.Utility
{
    public interface IDiscordWebhookService
    {
        Task SendLogAsync(string message);
    }
}
