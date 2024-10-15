using System.Text.RegularExpressions;

namespace Core;

public static class Tokenizer
{
    private static Regex AnyWord { get; } = new("\\b[A-Za-zА-Яа-я0-9]+\\b", RegexOptions.Compiled);
    
    public static IEnumerable<string> TokenizeText(this string text)
    {
        return AnyWord.Matches(text).Select(match => match.Value.ToLower());
    }
}