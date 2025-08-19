public class Menu
{
    public string Title { get; set; }
    public List<MenuOption> Options { get; set; }

    public Menu(string title, List<MenuOption> options)
    {
        Title = title;
        Options = options;
    }

    public string GetDisplayText()
    {
        return $"{Title}\n" + string.Join("\n", Options.Select(o => o.Label));
    }

    public MenuOption? GetOption(string input)
    {
        return Options.FirstOrDefault(o => o.Command.Equals(input.Trim(), StringComparison.OrdinalIgnoreCase));
    }
}

public class MenuOption
{
    public string Label { get; set; }
    public string Command { get; set; }
    public string? TargetMenu { get; set; }

    public MenuOption(string label, string command, string? targetMenu = null)
    {
        Label = label;
        Command = command.ToLower();
        TargetMenu = targetMenu;
    }
}