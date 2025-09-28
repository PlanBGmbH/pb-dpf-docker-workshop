using System.Text.Json;
using ClientApplication;

// Read config from config.json
var configText = File.ReadAllText("config.json");
var config = JsonSerializer.Deserialize<Config>(configText);
var url = config?.Url ?? throw new Exception("URL not found in config");

using var client = new HttpClient();

while(true)
{
  try
  {
    var response = client.GetAsync($"{url}/weatherforecast").Result;
    var content = response.Content.ReadAsStringAsync().Result;
    Console.WriteLine($"Response: {content}");
  }
  catch (Exception ex)
  {
    Console.WriteLine($"Error: {ex.Message}");
  }
  finally
  {
    await Task.Delay(2000);
  }
}