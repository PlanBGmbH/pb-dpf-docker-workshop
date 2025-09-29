using AdvancedBackend.Data;
using AdvancedBackend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Read connection string from environment variables (docker-compose provides this)
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseNpgsql(connectionString));

var app = builder.Build();

// Auto-create DB schema at startup (for demo purposes)
using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
  db.Database.Migrate();

  if (!db.Messages.Any())
  {
    db.Messages.Add(new Message { Text = "Hello from EF + Postgres!" });
    db.SaveChanges();
  }
}

// Simple endpoints
app.MapGet("/", () => "API is running");

app.MapGet("/messages", async (AppDbContext db) =>
  await db.Messages.ToListAsync());

app.MapPost("/messages", async (AppDbContext db, Message msg) =>
{
  db.Messages.Add(msg);
  await db.SaveChangesAsync();
  return Results.Created($"/messages/{msg.Id}", msg);
});

app.Run();