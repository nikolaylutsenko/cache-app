namespace CacheApp.Utils;

using static CrypticWizard.RandomWordGenerator.WordGenerator;

public class WordSource
{
    public static IEnumerable<string> Generate(
        bool addArticle = false, // determinatives (or determiners
        bool addAdjective = false, // a word used to modify or describe a noun or a pronoun
        bool addNoun = false, // a word for a person, place, thing, or idea
        bool addAdverb = false, // a word that modifies or describes a verb
        bool addVerb = false, // used to express a state or an action
        int amount = 0
    )
    {
        var wordGenerator = new CrypticWizard.RandomWordGenerator.WordGenerator();
        var pattern = new List<PartOfSpeech>();

        if (addArticle)
            pattern.Add(PartOfSpeech.art);
        if (addAdjective)
            pattern.Add(PartOfSpeech.adj);
        if (addNoun)
            pattern.Add(PartOfSpeech.noun);
        if (addAdverb)
            pattern.Add(PartOfSpeech.adv);
        if (addVerb)
            pattern.Add(PartOfSpeech.verb);

        var patterns = wordGenerator.GetPatterns(pattern, ' ', amount);

        return patterns;
    }
}
