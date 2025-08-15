using System.Net.Http.Headers;
using System.Text.Json;

// Define variables.
var rand = new Random();
int gameListLength = 0;
string configPath = "config.json";
string steamLibCall;
SteamResponse? steamData;
Config? config;
List<Game> gameList = new();

// Check if config exists and creates if not.
if (!File.Exists(configPath))
{
    // Create config file.
    config = new Config();
    string defaultJson = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
    Console.WriteLine(defaultJson);
    File.WriteAllText(configPath, defaultJson);

    // End program to let user change values.
    Console.WriteLine("Config file created. Please edit the file with your Steam API key and username, as well as any other settings you wish to change.");
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
    return;
}
// Read the config file and get user defined values.
using FileStream openStream = File.OpenRead(configPath);
config = await JsonSerializer.DeserializeAsync<Config>(openStream);

// Define the HTTP Client.
using HttpClient client = new();

// Build Steam API call url.
steamLibCall = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={config!.API_Key}&steamid={config.Steam_ID}&include_appinfo=1&include_played_free_games={config.Include_Free}&format.json";
// Call Steam API task.
await ProcessGamesAsync(client);
// Call function to select a random game.
SelectGame(gameListLength);
// Prevent console window from auto-closing on program end.
Console.ReadKey();

// Task to call the Steam API to get a user's game library, and then output all the game IDs and names to the console.
async Task ProcessGamesAsync(HttpClient client)
{
    await using Stream stream = await client.GetStreamAsync(steamLibCall);
    steamData = await JsonSerializer.DeserializeAsync<SteamResponse>(stream);
    gameList = steamData!.response.games;
    gameListLength = gameList.Count;

    Console.WriteLine("Games found: " + steamData.response.game_count);
    Console.WriteLine("Length of Game List: " + steamData.response.games.Count);
    foreach (var game in gameList ?? Enumerable.Empty<Game>())
        Console.WriteLine(game.appid + " ..... " + game.name);

}

// Simple method to select a random game from a user's library.
void SelectGame(int max)
{
    int selectedGameNum = rand.Next(max);
    Console.WriteLine("Randomly generated number: " + selectedGameNum);
    Console.WriteLine("Game selected: " + gameList[selectedGameNum].name);
}