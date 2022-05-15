namespace manager.Pages.Shared;

public class NotificationModel
{
    public NotificationModel(Level notificationLevel, string message)
    {
        NotificationLevel = notificationLevel;
        Message = message;
    }

    public Level NotificationLevel { get; set; }
    public string Message { get; set; }

    public enum Level
    {
        Info,
        Warning,
        Danger
    }
}