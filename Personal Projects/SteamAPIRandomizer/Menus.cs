using System.Collections.Generic;

public static class ListMenuOptions
{
    public static Dictionary<string, Menu> All = new Dictionary<string, Menu>
    {
        ["Main Menu"] = new Menu("Main Menu", new List<String>
        {
            "[1]: Config file options.",
            "[2]: Get games from Steam API.",
            "[3]: List all owned games.",
            "[4]: Select random game."
        })
    };
}