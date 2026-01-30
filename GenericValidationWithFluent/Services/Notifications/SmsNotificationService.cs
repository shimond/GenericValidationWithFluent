namespace GenericValidationWithFluent.Services.Notifications;

public sealed class SmsNotificationService : INotificationService
{
    private readonly ILogger<SmsNotificationService> _logger;

    public SmsNotificationService(ILogger<SmsNotificationService> logger)
    {
        _logger = logger;
    }

    public string ChannelName => "SMS";

    public async Task<bool> SendAsync(
        string recipient, 
        string subject, 
        string message, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation(
                "Sending SMS to {Recipient}. Message: {Message}",
                recipient,
                message);

            // Simulate SMS sending (in real app: use Twilio, AWS SNS, etc.)
            await Task.Delay(150, cancellationToken);

            _logger.LogInformation(
                "SMS sent successfully to {Recipient}",
                recipient);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send SMS to {Recipient}",
                recipient);
            
            return false;
        }
    }
}
