public class Menu
{
    public string Title { get; set; }
    public List<string> Options { get; set; }

    public Menu(string title, List<string> options)
    {
        Title = title;
        Options = options;
    }

    public string GetDisplayText()
    {
        return $"{Title}\n" + string.Join("\n", Options);
    }
}