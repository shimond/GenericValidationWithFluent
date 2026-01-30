namespace GenericValidationWithFluent.Services.Notifications;

public interface INotificationService
{
    Task<bool> SendAsync(string recipient, string subject, string message, CancellationToken cancellationToken = default);
    string ChannelName { get; }
}
