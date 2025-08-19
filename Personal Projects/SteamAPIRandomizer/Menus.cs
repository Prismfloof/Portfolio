using System.Collections.Generic;

public static class Menus
{
    public static string CurrentMenu;
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
        }),
        ["About Menu"] = new Menu("About Menu", new List<MenuOption>
        {
            "Version: 0.0.2"),
            "Current build date: 8/19/2025",
            "Created by: Caleb Tapley, A.K.A. Prismfloof.",
            "Licensed under MIT License.",
            "Please add issues or PRs to the github, or reach out to me at calebtapley28@gmail.com"
        })
    };
}