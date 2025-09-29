using AdvancedBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedBackend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<Message> Messages => Set<Message>();
}