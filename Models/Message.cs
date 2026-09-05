namespace RedFox.Models;

public class Message
{
    public int Id { get; set; }

    public string SenderName { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public string Type { get; set; } = string.Empty;

    public bool IsUnsent { get; set; }

    public string ReactionsJson { get; set; } = "";
}