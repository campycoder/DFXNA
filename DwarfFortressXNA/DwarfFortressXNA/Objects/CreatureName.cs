using System;
using System.Linq;

namespace DwarfFortressXNA.Objects
{
    public class CreatureName
    {
        Word one;
        Word two;
        Noun.NounUsage twoIntendedNoun = Noun.NounUsage.NULL;
        Prefix.PrefixUsage twoIntendedPref = Prefix.PrefixUsage.NULL;
        Word three;
        Noun.NounUsage threeIntendedNoun = Noun.NounUsage.NULL;
        Adjective.AdjectiveUsage threeIntendedAdj = Adjective.AdjectiveUsage.NULL;
        string intendedLanguage;
        bool isBad = true;

        public CreatureName(string intendedLanguage)
        {
            one = null;
            two = null;
            three = null;
            while(isBad) RegenerateCreatureNameObject(intendedLanguage);
        }
        public string AskForString(bool humanReadable)
        {
            if (isBad) return "BAD STRING";
            if (!humanReadable)
                return (((int) DwarfFortress.LanguageManager.TranslationList[intendedLanguage][one.Id][0] >=
                         0x20 &&
                         (int) DwarfFortress.LanguageManager.TranslationList[intendedLanguage][one.Id][0] <=
                         0x80)
                    ? DwarfFortress.LanguageManager.TranslationList[intendedLanguage][one.Id].Substring(0, 1)
                        .ToUpper()
                    : DwarfFortress.LanguageManager.TranslationList[intendedLanguage][one.Id].Substring(0, 1)) +
                       DwarfFortress.LanguageManager.TranslationList[intendedLanguage][one.Id].Substring(1) +
                       " " +
                       (((int) DwarfFortress.LanguageManager.TranslationList[intendedLanguage][two.Id][0] >=
                         0x20 &&
                         (int) DwarfFortress.LanguageManager.TranslationList[intendedLanguage][two.Id][0] <=
                         0x80)
                           ? DwarfFortress.LanguageManager.TranslationList[intendedLanguage][two.Id].Substring(
                               0, 1).ToUpper() +
                             DwarfFortress.LanguageManager.TranslationList[intendedLanguage][two.Id].Substring(1)
                           : DwarfFortress.LanguageManager.TranslationList[intendedLanguage][two.Id]) +
                       DwarfFortress.LanguageManager.TranslationList[intendedLanguage][three.Id];
            var final = "";
            final += (((int)DwarfFortress.LanguageManager.TranslationList[intendedLanguage][one.Id][0] >= 0x20 && (int)DwarfFortress.LanguageManager.TranslationList[intendedLanguage][one.Id][0] <= 0x80) ? DwarfFortress.LanguageManager.TranslationList[intendedLanguage][one.Id].Substring(0, 1).ToUpper() : DwarfFortress.LanguageManager.TranslationList[intendedLanguage][one.Id].Substring(0, 1)) + DwarfFortress.LanguageManager.TranslationList[intendedLanguage][one.Id].Substring(1) + " ";
            final += (twoIntendedNoun == Noun.NounUsage.NULL ? (twoIntendedPref == Prefix.PrefixUsage.NULL ? (((int)two.AdjForm.Adj[0] >= 0x20 && (int)two.AdjForm.Adj[0] <= 0x80) ? two.AdjForm.Adj.Substring(0,1).ToUpper()  + two.AdjForm.Adj.Substring(1) : two.AdjForm.Adj) : ((int)two.PrefixForm.Pref[0] >= 0x20 && (int)two.PrefixForm.Pref[0] <= 0x80) ? two.PrefixForm.Pref.Substring(0,1).ToUpper() + two.PrefixForm.Pref.Substring(1) : two.PrefixForm.Pref) : (twoIntendedNoun == Noun.NounUsage.FRONT_COMPOUND_NOUN_SING ? ((int)two.NounForm.Singular[0] >= 0x20 && (int)two.NounForm.Singular[0] <= 0x80) ? two.NounForm.Singular.Substring(0,1).ToUpper() + two.NounForm.Singular.Substring(1) : two.NounForm.Singular : ((int)two.NounForm.Plural[0] >= 0x20 && (int)two.NounForm.Plural[0] <= 0x80) ? two.NounForm.Plural.Substring(0,1).ToUpper() + two.NounForm.Plural.Substring(1) : two.NounForm.Plural));
            final += (threeIntendedNoun == Noun.NounUsage.NULL ? (threeIntendedAdj == Adjective.AdjectiveUsage.NULL ? three.VerbForm.PresentFirst : three.AdjForm.Adj) : (threeIntendedNoun == Noun.NounUsage.REAR_COMPOUND_NOUN_SING ? three.NounForm.Singular : three.NounForm.Plural));
            return final;
        }

        public void RegenerateCreatureNameObject(string requestedLanguage)
        {
            if (!DwarfFortress.LanguageManager.TranslationList.ContainsKey(requestedLanguage)) throw new Exception("Bad language " + requestedLanguage + " requested for name object!");
            intendedLanguage = requestedLanguage;
            var newOne = DwarfFortress.LanguageManager.WordList.Values.ToList()[DwarfFortress.Random.Next(DwarfFortress.LanguageManager.WordList.Values.Count)];
            var newTwo = DwarfFortress.LanguageManager.WordList.Values.ToList()[DwarfFortress.Random.Next(DwarfFortress.LanguageManager.WordList.Values.Count)];
            var newThree = DwarfFortress.LanguageManager.WordList.Values.ToList()[DwarfFortress.Random.Next(DwarfFortress.LanguageManager.WordList.Values.Count)];
            if (WordsFitCriteria(newOne, newTwo, newThree))
            {
                isBad = false;
                one = newOne;
                two = newTwo;
                if (newTwo.NounForm != null && (newTwo.NounForm.Usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_SING) || newTwo.NounForm.Usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR)))
                {
                    if (newTwo.NounForm.Usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_SING) && !newTwo.NounForm.Usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR)) twoIntendedNoun = Noun.NounUsage.FRONT_COMPOUND_NOUN_SING;
                    else if (newTwo.NounForm.Usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR) && !newTwo.NounForm.Usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_SING)) twoIntendedNoun = Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR;
                    else twoIntendedNoun = (DwarfFortress.Random.Next(100) % 2 == 0 ? Noun.NounUsage.FRONT_COMPOUND_NOUN_SING : Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR);
                }
                else if(newTwo.PrefixForm != null && newTwo.PrefixForm.Usages.Contains(Prefix.PrefixUsage.FRONT_COMPOUND_PREFIX))
                {
                    twoIntendedPref = Prefix.PrefixUsage.FRONT_COMPOUND_PREFIX;
                }
                three = newThree;
                if (newThree.NounForm != null && (newThree.NounForm.Usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_SING) || newThree.NounForm.Usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR)))
                {
                    if (newThree.NounForm.Usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_SING) && !newThree.NounForm.Usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR)) threeIntendedNoun = Noun.NounUsage.REAR_COMPOUND_NOUN_SING;
                    else if (newThree.NounForm.Usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR) && !newThree.NounForm.Usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_SING)) threeIntendedNoun = Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR;
                    else threeIntendedNoun = DwarfFortress.Random.Next(100) % 2 == 0 ? Noun.NounUsage.REAR_COMPOUND_NOUN_SING : Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR;
                }
                else if(newThree.AdjForm != null && newThree.AdjForm.Usages.Contains(Adjective.AdjectiveUsage.REAR_COMPOUND_ADJ)) 
                {
                   threeIntendedAdj = Adjective.AdjectiveUsage.REAR_COMPOUND_ADJ;
                }
            }
            else isBad = true;
            
        }

        public bool WordsFitCriteria(Word newOne, Word newTwo, Word newThree)
        {
            var oneValid = newOne.NounForm != null;
            var twoValidNoun = newTwo.NounForm != null && (newTwo.NounForm.Usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_SING) || newTwo.NounForm.Usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR));
            var twoValidPrefix = newTwo.PrefixForm != null && newTwo.PrefixForm.Usages.Contains(Prefix.PrefixUsage.FRONT_COMPOUND_PREFIX);
            var twoValidAdj = newTwo.AdjForm != null && newTwo.AdjForm.Usages.Contains(Adjective.AdjectiveUsage.FRONT_COMPOUND_ADJ);
            var threeValidNoun = newThree.NounForm != null && (newThree.NounForm.Usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_SING) || newThree.NounForm.Usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR));
            var threeValidAdj = newThree.AdjForm != null && newThree.AdjForm.Usages.Contains(Adjective.AdjectiveUsage.REAR_COMPOUND_ADJ);
            var threeValidVerb = newThree.VerbForm != null && newThree.VerbForm.Standard;
            return oneValid && (twoValidNoun || twoValidPrefix || twoValidAdj) && (threeValidNoun || threeValidAdj || threeValidVerb);
        }

        public bool IsBad()
        {
            return isBad;
        }
    }
}
