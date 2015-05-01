using System;
using System.Collections.Generic;

namespace DwarfFortressXNA.Objects
{
    public class Noun
    {
        public enum NounUsage
        {
            THE_NOUN_SING,
            THE_NOUN_PLUR,
            THE_COMPOUND_NOUN_SING,
            THE_COMPOUND_NOUN_PLUR,
            OF_NOUN_SING,
            OF_NOUN_PLUR,
            FRONT_COMPOUND_NOUN_SING,
            FRONT_COMPOUND_NOUN_PLUR,
            REAR_COMPOUND_NOUN_SING,
            REAR_COMPOUND_NOUN_PLUR,
            NULL
        }

        public string Singular, Plural;
        public List<NounUsage> Usages;
        public Noun(string singular, string plural, List<NounUsage> usages)
        {
            Singular = singular;
            Plural = plural;
            Usages = usages;
        }
    }

    public class Adjective
    {
        public enum AdjectiveUsage
        {
            THE_COMPOUND_ADJ,
            FRONT_COMPOUND_ADJ,
            REAR_COMPOUND_ADJ,
            NULL
        }
        public string Adj;
        public List<AdjectiveUsage> Usages;
        public Adjective(string adj, List<AdjectiveUsage> usages)
        {
            Adj = adj;
            Usages = usages;
        }
    }

    public class Verb
    {
        public string PresentFirst, PresentThird, Preterite, PastPart, PresentPart;
        public bool Standard;
        public Verb(string presentFirst, string presentThird, string preterite, string pastPart, string presentPart, bool standard)
        {
            PresentFirst = presentFirst;
            PresentThird = presentThird;
            Preterite = preterite;
            PastPart = pastPart;
            PresentPart = presentPart;
            Standard = standard;
        }
    }

    public class Prefix
    {
        public enum PrefixUsage
        {
            FRONT_COMPOUND_PREFIX,
            THE_COMPOUND_PREFIX,
            NULL
        }
        public string Pref;
        public List<PrefixUsage> Usages;
        public Prefix(string pref, List<PrefixUsage> usages)
        {
            Pref = pref;
            Usages = usages;
        }
    }

    public class Word
    {
        public string Id;
        public Noun NounForm { get; private set; }
        public Adjective AdjForm { get; private set; }
        public Verb VerbForm { get; private set; }
        public Prefix PrefixForm { get; private set; }
        public Word(List<string> tokenList)
        {
            Id = RawFile.StripTokenEnding(tokenList[0].Remove(0, 6));
            tokenList.Remove(tokenList[0]);
            var currentBuffer = new List<string>();
            foreach (var t in tokenList)
            {
                if ((t.StartsWith("[NOUN:") || t.StartsWith("[ADJ:") || t.StartsWith("[VERB:") || t.StartsWith("[PREFIX:")) && currentBuffer.Count > 0)
                {
                    SelectParsingFunction(currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(t);
                }
                else currentBuffer.Add(t);
            }
            if (currentBuffer.Count == 0) return;
            SelectParsingFunction(currentBuffer);
            
        }

        public void SelectParsingFunction(List<string> currentBuffer)
        {
            switch (currentBuffer[0][1])
            {
                case 'N':
                    ParseNoun(currentBuffer);
                    break;
                case 'A':
                    ParseAdjective(currentBuffer);
                    break;
                case 'V':
                    ParseVerb(currentBuffer);
                    break;
                case 'P':
                    ParsePrefix(currentBuffer);
                    break;
            }
        }

        public void ParseNoun(List<string> tokenList)
        {
            var nounToken = tokenList[0].Split(new[] { ':' });
            var singular = nounToken[1];
            var plural = RawFile.StripTokenEnding(nounToken[2]);
            tokenList.Remove(tokenList[0]);
            var nounUsages = new List<Noun.NounUsage>();
            foreach (var t in tokenList)
            {
                Noun.NounUsage usageBuffer;
                if(Enum.TryParse(RawFile.StripTokenEnding(t.Replace("[", "")), out usageBuffer)) nounUsages.Add(usageBuffer);
            }
            var noun = new Noun(singular, plural, nounUsages);
            NounForm = noun;
        }
        public void ParseAdjective(List<string> tokenList)
        {
            var adjString = tokenList[0].Split(new[] { ':' })[1].Replace("]","");
            tokenList.Remove(tokenList[0]);
            var adjectiveUsages = new List<Adjective.AdjectiveUsage>();
            foreach (var t in tokenList)
            {
                Adjective.AdjectiveUsage usageBuffer;
                if (Enum.TryParse(RawFile.StripTokenEnding(t.Replace("[", "")), out usageBuffer)) adjectiveUsages.Add(usageBuffer);
            }
            var adj = new Adjective(adjString, adjectiveUsages);
            AdjForm = adj;
        }

        public void ParseVerb(List<string> tokenList)
        {
            var verbStripped = tokenList[0].Split(new[] { ':' });
            string presentFirst = verbStripped[1],
                presentThird = verbStripped[2],
                preterite = verbStripped[3],
                pastPart = verbStripped[4],
                presentPart = RawFile.StripTokenEnding(verbStripped[5]);
            tokenList.Remove(tokenList[0]);
            var standard = (tokenList.Count != 0);
            var verb = new Verb(presentFirst, presentThird, preterite, pastPart, presentPart, standard);
            VerbForm = verb;
        }

        public void ParsePrefix(List<string> tokenList)
        {
            var prefString = RawFile.StripTokenEnding(tokenList[0].Split(new[] {':'})[1]);
            tokenList.Remove(tokenList[0]);
            var prefixUsages = new List<Prefix.PrefixUsage>();
            foreach (var t in tokenList)
            {
                Prefix.PrefixUsage usageBuffer;
                if (Enum.TryParse(RawFile.StripTokenEnding(t.Replace("[", "")), out usageBuffer)) prefixUsages.Add(usageBuffer);
            }
            var prefix = new Prefix(prefString, prefixUsages);
            PrefixForm = prefix;
        }
    }
}
