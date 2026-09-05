namespace RedFox.Dtos;

public class MessengerExport
{
    public List<string> Participants { get; set; } = [];

    public string ThreadName { get; set; } = string.Empty;

    public List<MessengerMessage> Messages { get; set; } = [];
}