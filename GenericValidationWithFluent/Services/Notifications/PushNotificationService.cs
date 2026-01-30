namespace GenericValidationWithFluent.Services.Notifications;

public sealed class PushNotificationService : INotificationService
{
    private readonly ILogger<PushNotificationService> _logger;

    public PushNotificationService(ILogger<PushNotificationService> logger)
    {
        _logger = logger;
    }

    public string ChannelName => "Push";

    public async Task<bool> SendAsync(
        string recipient, 
        string subject, 
        string message, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation(
                "Sending push notification to device {DeviceId}. Title: {Subject}",
                recipient,
                subject);

            // Simulate push notification (in real app: use Firebase, Azure Notification Hub, etc.)
            await Task.Delay(80, cancellationToken);

            _logger.LogInformation(
                "Push notification sent successfully to device {DeviceId}",
                recipient);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send push notification to device {DeviceId}",
                recipient);
            
            return false;
        }
    }
}
