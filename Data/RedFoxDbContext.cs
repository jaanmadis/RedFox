using Microsoft.EntityFrameworkCore;
using RedFox.Models;

namespace RedFox.Data;

public class RedFoxDbContext : DbContext
{
    public RedFoxDbContext(DbContextOptions<RedFoxDbContext> options) : base(options)
    {
    }

    public DbSet<Message> Messages => Set<Message>();
}