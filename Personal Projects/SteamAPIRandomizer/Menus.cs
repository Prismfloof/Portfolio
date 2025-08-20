public static class Menus
{
    public static string CurrentMenu = "Main Menu";
    public static Dictionary<string, Menu> All = new Dictionary<string, Menu>
    {
        ["Main Menu"] = new Menu("Main Menu", new List<MenuOption>
        {
            new("[1]: Config file options.", "1", "Config Menu"),
            new("[2]: Get games from Steam API.", "2"),
            new("[3]: List all owned games.", "3"),
            new("[4]: Select random game.", "4")
        }),
        ["Config Menu"] = new Menu("Config Menu", new List<MenuOption>
        {
            new("[1]: List current config options and values.", "1"),
            new("[2]: Edit values in config file.", "2")
        })
    };
}