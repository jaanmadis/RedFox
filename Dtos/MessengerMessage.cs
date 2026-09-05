namespace RedFox.Dtos;

public class MessengerMessage
{
    public bool IsUnsent { get; set; }

    public string SenderName { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public long Timestamp { get; set; }

    public string Type { get; set; } = string.Empty;

    public object[] Reactions { get; set; } = [];
}