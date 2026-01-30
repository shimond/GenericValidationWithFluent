using System.Collections.Frozen;

namespace GenericValidationWithFluent.Services.Notifications;

public sealed class NotificationManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<NotificationManager> _logger;
    
    // Frozen collections are optimized for read-heavy scenarios (.NET 8+)
    private static readonly FrozenSet<string> ValidChannels = new[]
    {
        "email",
        "sms",
        "push"
    }.ToFrozenSet(StringComparer.OrdinalIgnoreCase);

    public NotificationManager(
        IServiceProvider serviceProvider,
        ILogger<NotificationManager> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<bool> SendAsync(
        string channel,
        string recipient,
        string subject,
        string message,
        CancellationToken cancellationToken = default)
    {
        if (!ValidChannels.Contains(channel))
        {
            _logger.LogWarning(
                "Invalid notification channel: {Channel}. Valid channels: {ValidChannels}",
                channel,
                string.Join(", ", ValidChannels));
            
            return false;
        }

        // Resolve the specific implementation using keyed services
        var notificationService = _serviceProvider
            .GetRequiredKeyedService<INotificationService>(channel.ToLowerInvariant());

        _logger.LogInformation(
            "Using {ChannelName} channel to send notification",
            notificationService.ChannelName);

        return await notificationService.SendAsync(
            recipient, 
            subject, 
            message, 
            cancellationToken);
    }

    public async Task<Dictionary<string, bool>> SendToAllChannelsAsync(
        string recipient,
        string subject,
        string message,
        CancellationToken cancellationToken = default)
    {
        var results = new Dictionary<string, bool>();

        foreach (var channel in ValidChannels)
        {
            var result = await SendAsync(channel, recipient, subject, message, cancellationToken);
            results[channel] = result;
        }

        return results;
    }

    public IReadOnlySet<string> GetAvailableChannels() => ValidChannels;
}
