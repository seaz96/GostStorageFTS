namespace Core.Analyzer;

public static class StopWordsFilter
{
    private static readonly HashSet<string> TrashWords =
        ["и", "в", "не", "на", "я", "у", "он", "с", "а", "по", "это", "к", "но", "из", "который", "то", "за", "для", "от"];
    
    public static IEnumerable<string> Filter(this IEnumerable<string> text)
    {
        return text.AsParallel().Where(word => !TrashWords.Contains(word));
    }
}