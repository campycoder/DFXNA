using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfFortressXNA
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

        public string singular, plural;
        public List<NounUsage> usages;
        public Noun(string singular, string plural, List<NounUsage> usages)
        {
            this.singular = singular;
            this.plural = plural;
            this.usages = usages;
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
        public string adj;
        public List<AdjectiveUsage> usages;
        public Adjective(string adj, List<AdjectiveUsage> usages)
        {
            this.adj = adj;
            this.usages = usages;
        }
    }

    public class Verb
    {
        public string present_first, present_third, preterite, past_part, present_part;
        public bool standard;
        public Verb(string present_first, string present_third, string preterite, string past_part, string present_part, bool standard)
        {
            this.present_first = present_first;
            this.present_third = present_third;
            this.preterite = preterite;
            this.past_part = past_part;
            this.present_part = present_part;
            this.standard = standard;
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
        public string pref;
        public List<PrefixUsage> usages;
        public Prefix(string pref, List<PrefixUsage> usages)
        {
            this.pref = pref;
            this.usages = usages;
        }
    }

    public class Word
    {
        public string id;
        public Noun NounForm { get; private set; }
        public Adjective AdjForm { get; private set; }
        public Verb VerbForm { get; private set; }
        public Prefix PrefixForm { get; private set; }
        public Word(List<string> tokenList)
        {
            this.id = tokenList[0].Remove(0, 6).Replace("]", "");
            tokenList.Remove(tokenList[0]);
            List<string> currentBuffer = new List<string>();
            for(int i = 0;i < tokenList.Count;i++)
            {
                if ((tokenList[i].StartsWith("[NOUN:") || tokenList[i].StartsWith("[ADJ:") || tokenList[i].StartsWith("[VERB:") || tokenList[i].StartsWith("[PREFIX:")) && currentBuffer.Count > 0)
                {
                    SelectParsingFunction(currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(tokenList[i]);
                }
                else currentBuffer.Add(tokenList[i]);
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
            Noun noun;
            string singular = "", plural = "";
            string[] nounToken = tokenList[0].Split(new char[] { ':' });
            singular = nounToken[1];
            plural = nounToken[2].Replace("]", "");
            tokenList.Remove(tokenList[0]);
            List<Noun.NounUsage> nounUsages = new List<Noun.NounUsage>();
            for(int i = 0; i < tokenList.Count;i++)
            {
                Noun.NounUsage usageBuffer;
                if(Enum.TryParse<Noun.NounUsage>(tokenList[i].Replace("[", "").Replace("]", ""), out usageBuffer)) nounUsages.Add(usageBuffer);
            }
            noun = new Noun(singular, plural, nounUsages);
            this.NounForm = noun;
        }
        public void ParseAdjective(List<string> tokenList)
        {
            Adjective adj;
            string adj_string = tokenList[0].Split(new char[] { ':' })[1].Replace("]","");
            tokenList.Remove(tokenList[0]);
            List<Adjective.AdjectiveUsage> adjectiveUsages = new List<Adjective.AdjectiveUsage>();
            for(int i = 0; i < tokenList.Count;i++)
            {
                Adjective.AdjectiveUsage usageBuffer;
                if (Enum.TryParse<Adjective.AdjectiveUsage>(tokenList[i].Replace("[", "").Replace("]", ""), out usageBuffer)) adjectiveUsages.Add(usageBuffer);
            }
            adj = new Adjective(adj_string, adjectiveUsages);
            this.AdjForm = adj;
        }

        public void ParseVerb(List<string> tokenList)
        {
            Verb verb;
            string[] verbStripped = tokenList[0].Split(new char[] { ':' });
            string present_first = verbStripped[1], present_third = verbStripped[2], preterite = verbStripped[3], past_part = verbStripped[4], present_part = verbStripped[5].Replace("]","");
            tokenList.Remove(tokenList[0]);
            bool standard = (tokenList.Count != 0);
            verb = new Verb(present_first, present_third, preterite, past_part, present_part, standard);
            this.VerbForm = verb;
        }

        public void ParsePrefix(List<string> tokenList)
        {
            Prefix prefix;
            string pref_string = tokenList[0].Split(new char[] { ':' })[1].Replace("]", "");
            tokenList.Remove(tokenList[0]);
            List<Prefix.PrefixUsage> prefixUsages = new List<Prefix.PrefixUsage>();
            for(int i =0;i<tokenList.Count;i++)
            {
                Prefix.PrefixUsage usageBuffer;
                if (Enum.TryParse<Prefix.PrefixUsage>(tokenList[i].Replace("[", "").Replace("]", ""), out usageBuffer)) prefixUsages.Add(usageBuffer);
            }
            prefix = new Prefix(pref_string, prefixUsages);
            this.PrefixForm = prefix;
        }
    }
}
