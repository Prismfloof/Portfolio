public class SteamResponse
{
    public required ResponseData response { get; set; }
}
public class ResponseData
{
    public int game_count { get; set; }
    public required List<Game> games { get; set; }
}
public class Game
{
    public required int appid { get; set; }
    public required string name { get; set; }
}