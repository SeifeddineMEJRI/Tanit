namespace Tanit.User.Domain.Notifier
{
    public interface INotifier
    {
        Task SendAsync(string subject, string body);
    }
}
