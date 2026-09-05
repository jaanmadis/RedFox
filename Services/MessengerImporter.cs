using System.Text.Json;
using RedFox.Data;
using RedFox.Dtos;
using RedFox.Models;

namespace RedFox.Services;

public class MessengerImporter
{
    private readonly RedFoxDbContext _db;

    public async Task<int> ImportAsync(Stream stream)
    {
        var import = await JsonSerializer.DeserializeAsync<MessengerExport>(
            stream,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (import == null)
        {
            return 0;
        }

        foreach (var dto in import.Messages)
        { 
            var message = new Message
            {
                SenderName = dto.SenderName,
                Text = dto.Text,
                Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(dto.Timestamp).UtcDateTime,
                Type = dto.Type,
                IsUnsent = dto.IsUnsent,
                ReactionsJson = JsonSerializer.Serialize(dto.Reactions)
            };
            _db.Messages.Add(message);
        }

        await _db.SaveChangesAsync();

        return import.Messages.Count;
    }

    public MessengerImporter(RedFoxDbContext db)
    {
        _db = db;
    }
}