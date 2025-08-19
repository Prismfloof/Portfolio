using System.Net.Http.Headers;
using System.Text.Json;

// Define variables.
var rand = new Random();
string version = "0.0.2";
string configPath = "config.json";
string libraryAPIResponsePath = "libraryAPIResponse.json";
string steamLibCall;
string currentMenu;
SteamResponse steamData;
Config? config;
List<Game> gameList = new();

Console.WriteLine($"Currently using version: {version} of the Steam Library Randomizer.");

// Check if config exists and creates if not.
CheckConfigFile(configPath);

// Read the config file and get user defined values.
using FileStream openStream = File.OpenRead(configPath);
config = await JsonSerializer.DeserializeAsync<Config>(openStream);

// Define the HTTP Client.
using HttpClient client = new();

// Build Steam API call url.
steamLibCall = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={config!.API_Key}&steamid={config.Steam_ID}&include_appinfo=1&include_played_free_games={config.Include_Free}&format.json";
// Call Steam API to get a user's library.
steamData = await QueryLibraryAPI(client);
// Call function to select a random game.
SelectGame(steamData!.response.game_count);
// Prevent console window from auto-closing on program end.
Console.ReadKey();

// Task to call the Steam API to get a user's game library, and then output all the game IDs and names to the console.
// async Task<SteamResponse> QueryLibraryAPI(HttpClient client)
// {
//     await using Stream stream = await client.GetStreamAsync(steamLibCall) ?? throw new Exception("Invalid API response or no response from Steam API. Please ensure a valid connection to the internet and the config.json file has been configured correctly.");
//     return await JsonSerializer.DeserializeAsync<SteamResponse>(stream) ?? throw new InvalidOperationException();
//     gameList = steamData!.response.games;

//     Console.WriteLine("Games found: " + steamData.response.game_count);
//     Console.WriteLine("Length of Game List: " + steamData.response.games.Count);
//     foreach (var game in gameList ?? Enumerable.Empty<Game>())
//         Console.WriteLine(game.appid + " ..... " + game.name);

// }

async Task<SteamResponse> QueryLibraryAPI(HttpClient client)
{
    try
    {
        HttpResponseMessage response = await client.GetAsync(steamLibCall);

        // Throw error if API call is not successful.
        response.EnsureSuccessStatusCode();

        await using Stream stream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<SteamResponse>(stream) ?? throw new Exception("Failed to deserialize the Steam API Response.");
    }
    catch (HttpRequestException error) when ((error.StatusCode ?? 0) == System.Net.HttpStatusCode.Unauthorized)
    {

        throw new Exception($"Unauthorized: Invalid Steam API key or Steam ID. Please check your config.json file. ERROR: {error}");
    }
    catch (HttpRequestException error)
    {
        throw new Exception($"Failed to get a response from the Steam API. Please check your internet connection and the status of the steam API server before trying again. ERROR: {error}");
    }
    catch (Exception error)
    {
        throw new Exception($"An unexpected error occured while querying the Steam API. ERROR: {error}");
    }
}

void CheckConfigFile(string configPath)
{
    if (!File.Exists(configPath))
    {
        // Create config file.
        config = new Config();
        string defaultJson = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(defaultJson);
        File.WriteAllText(configPath, defaultJson);

        // End program to let user change values.
        Console.WriteLine("Config file created. Please edit the \"config.json\" file with your Steam API key and username, as well as any other settings you wish to change.");
        Console.WriteLine("Press any key to continue . . .");
        Console.ReadKey();
        return;
    }
}

// Simple method to select a random game from a user's library.
void SelectGame(int max)
{
    int selectedGameNum = rand.Next(max);
    Console.WriteLine("Randomly generated number: " + selectedGameNum);
    Console.WriteLine("Game selected: " + gameList[selectedGameNum].name);
}