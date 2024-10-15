namespace Core;

public static class Analyzer
{
    public static Dictionary<string, int> AnalyzeText(string text)
    {
        var frequency = new Dictionary<string, int>();
        var stemmedWords = text.TokenizeText().Filter().Stem();

        foreach (var word in stemmedWords)
        {
            if (!frequency.TryAdd(word, 1))
            {
                frequency[word]++;
            }
        }

        return frequency;
    } 
}