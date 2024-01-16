using FluentEmail.Core;

namespace Tanit.User.Domain.Notifier;

public class EmailNotifier : INotifier
{
    private readonly IFluentEmail _fluentEmail;

    public EmailNotifier(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public async Task SendAsync(string subject, string body)
    {
        var email = await _fluentEmail
                    .To("mej.seifeddine@gmail.com", "Seifeddine")
                    .Subject(subject)
                    .Body(body)
                    .SendAsync();
    }
}
