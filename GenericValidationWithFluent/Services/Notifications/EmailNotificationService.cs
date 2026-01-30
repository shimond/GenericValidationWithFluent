namespace GenericValidationWithFluent.Services.Notifications;

public sealed class EmailNotificationService : INotificationService
{
    private readonly ILogger<EmailNotificationService> _logger;

    public EmailNotificationService(ILogger<EmailNotificationService> logger)
    {
        _logger = logger;
    }

    public string ChannelName => "Email";

    public async Task<bool> SendAsync(
        string recipient, 
        string subject, 
        string message, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation(
                "Sending email to {Recipient}. Subject: {Subject}",
                recipient,
                subject);

            // Simulate email sending (in real app: use SendGrid, SMTP, etc.)
            await Task.Delay(100, cancellationToken);

            _logger.LogInformation(
                "Email sent successfully to {Recipient}",
                recipient);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send email to {Recipient}",
                recipient);
            
            return false;
        }
    }
}
