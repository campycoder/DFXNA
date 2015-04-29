using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfFortressXNA
{
    public class CreatureName
    {
        Word one;
        Word two;
        Noun.NounUsage two_intended_noun = Noun.NounUsage.NULL;
        Prefix.PrefixUsage two_intended_pref = Prefix.PrefixUsage.NULL;
        Adjective.AdjectiveUsage two_intended_adj = Adjective.AdjectiveUsage.NULL;
        Word three;
        Noun.NounUsage three_intended_noun = Noun.NounUsage.NULL;
        Adjective.AdjectiveUsage three_intended_adj = Adjective.AdjectiveUsage.NULL;
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
            else
            {
                if(humanReadable)
                {
                    string final = "";
                    final += (((int)DwarfFortressMono.languageManager.translationList[intendedLanguage][one.id][0] >= 0x20 && (int)DwarfFortressMono.languageManager.translationList[intendedLanguage][one.id][0] <= 0x80) ? DwarfFortressMono.languageManager.translationList[intendedLanguage][one.id].Substring(0, 1).ToUpper() : DwarfFortressMono.languageManager.translationList[intendedLanguage][one.id].Substring(0, 1)) + DwarfFortressMono.languageManager.translationList[intendedLanguage][one.id].Substring(1) + " ";
                    final += (two_intended_noun == Noun.NounUsage.NULL ? (two_intended_pref == Prefix.PrefixUsage.NULL ? (((int)two.AdjForm.adj[0] >= 0x20 && (int)two.AdjForm.adj[0] <= 0x80) ? two.AdjForm.adj.Substring(0,1).ToUpper()  + two.AdjForm.adj.Substring(1) : two.AdjForm.adj) : ((int)two.PrefixForm.pref[0] >= 0x20 && (int)two.PrefixForm.pref[0] <= 0x80) ? two.PrefixForm.pref.Substring(0,1).ToUpper() + two.PrefixForm.pref.Substring(1) : two.PrefixForm.pref) : (two_intended_noun == Noun.NounUsage.FRONT_COMPOUND_NOUN_SING ? ((int)two.NounForm.singular[0] >= 0x20 && (int)two.NounForm.singular[0] <= 0x80) ? two.NounForm.singular.Substring(0,1).ToUpper() + two.NounForm.singular.Substring(1) : two.NounForm.singular : ((int)two.NounForm.plural[0] >= 0x20 && (int)two.NounForm.plural[0] <= 0x80) ? two.NounForm.plural.Substring(0,1).ToUpper() + two.NounForm.plural.Substring(1) : two.NounForm.plural));
                    final += (three_intended_noun == Noun.NounUsage.NULL ? (three_intended_adj == Adjective.AdjectiveUsage.NULL ? three.VerbForm.present_first : three.AdjForm.adj) : (three_intended_noun == Noun.NounUsage.REAR_COMPOUND_NOUN_SING ? three.NounForm.singular : three.NounForm.plural));
                    return final;
                }
                else
                {
                    return (((int)DwarfFortressMono.languageManager.translationList[intendedLanguage][one.id][0] >= 0x20 && (int)DwarfFortressMono.languageManager.translationList[intendedLanguage][one.id][0] <= 0x80) ? DwarfFortressMono.languageManager.translationList[intendedLanguage][one.id].Substring(0, 1).ToUpper() : DwarfFortressMono.languageManager.translationList[intendedLanguage][one.id].Substring(0, 1)) + DwarfFortressMono.languageManager.translationList[intendedLanguage][one.id].Substring(1) + " " + (((int)DwarfFortressMono.languageManager.translationList[intendedLanguage][two.id][0] >= 0x20 && (int)DwarfFortressMono.languageManager.translationList[intendedLanguage][two.id][0] <= 0x80) ? DwarfFortressMono.languageManager.translationList[intendedLanguage][two.id].Substring(0, 1).ToUpper() + DwarfFortressMono.languageManager.translationList[intendedLanguage][two.id].Substring(1) : DwarfFortressMono.languageManager.translationList[intendedLanguage][two.id]) + DwarfFortressMono.languageManager.translationList[intendedLanguage][three.id];
                }
            }
        }

        public void RegenerateCreatureNameObject(string intendedLanguage)
        {
            if (!DwarfFortressMono.languageManager.translationList.ContainsKey(intendedLanguage)) throw new Exception("Bad language " + intendedLanguage + " requested for name object!");
            this.intendedLanguage = intendedLanguage;
            Word one = DwarfFortressMono.languageManager.wordList.Values.ToList<Word>()[DwarfFortressMono.random.Next(DwarfFortressMono.languageManager.wordList.Values.Count)];
            Word two = DwarfFortressMono.languageManager.wordList.Values.ToList<Word>()[DwarfFortressMono.random.Next(DwarfFortressMono.languageManager.wordList.Values.Count)];
            Word three = DwarfFortressMono.languageManager.wordList.Values.ToList<Word>()[DwarfFortressMono.random.Next(DwarfFortressMono.languageManager.wordList.Values.Count)];
            if (WordsFitCriteria(one, two, three))
            {
                this.isBad = false;
                this.one = one;
                this.two = two;
                if (two.NounForm != null && (two.NounForm.usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_SING) || two.NounForm.usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR)))
                {
                    if (two.NounForm.usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_SING) && !two.NounForm.usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR)) this.two_intended_noun = Noun.NounUsage.FRONT_COMPOUND_NOUN_SING;
                    else if (two.NounForm.usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR) && !two.NounForm.usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_SING)) this.two_intended_noun = Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR;
                    else this.two_intended_noun = (DwarfFortressMono.random.Next(100) % 2 == 0 ? Noun.NounUsage.FRONT_COMPOUND_NOUN_SING : Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR);
                }
                else if(two.PrefixForm != null && two.PrefixForm.usages.Contains(Prefix.PrefixUsage.FRONT_COMPOUND_PREFIX))
                {
                    this.two_intended_pref = Prefix.PrefixUsage.FRONT_COMPOUND_PREFIX;
                }
                else this.two_intended_adj = Adjective.AdjectiveUsage.FRONT_COMPOUND_ADJ;
                this.three = three;
                if (three.NounForm != null && (three.NounForm.usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_SING) || three.NounForm.usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR)))
                {
                    if (three.NounForm.usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_SING) && !three.NounForm.usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR)) this.three_intended_noun = Noun.NounUsage.REAR_COMPOUND_NOUN_SING;
                    else if (three.NounForm.usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR) && !three.NounForm.usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_SING)) this.three_intended_noun = Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR;
                    else this.three_intended_noun = DwarfFortressMono.random.Next(100) % 2 == 0 ? Noun.NounUsage.REAR_COMPOUND_NOUN_SING : Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR;
                }
                else if(three.AdjForm != null && three.AdjForm.usages.Contains(Adjective.AdjectiveUsage.REAR_COMPOUND_ADJ)) 
                {
                   this.three_intended_adj = Adjective.AdjectiveUsage.REAR_COMPOUND_ADJ;
                }
            }
            else this.isBad = true;
            
        }

        public bool WordsFitCriteria(Word one, Word two, Word three)
        {
            bool oneValid = one.NounForm != null;
            bool twoValid_noun = two.NounForm != null && (two.NounForm.usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_SING) || two.NounForm.usages.Contains(Noun.NounUsage.FRONT_COMPOUND_NOUN_PLUR));
            bool twoValid_prefix = two.PrefixForm != null && two.PrefixForm.usages.Contains(Prefix.PrefixUsage.FRONT_COMPOUND_PREFIX);
            bool twoValid_adj = two.AdjForm != null && two.AdjForm.usages.Contains(Adjective.AdjectiveUsage.FRONT_COMPOUND_ADJ);
            bool threeValid_noun = three.NounForm != null && (three.NounForm.usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_SING) || three.NounForm.usages.Contains(Noun.NounUsage.REAR_COMPOUND_NOUN_PLUR));
            bool threeValid_adj = three.AdjForm != null && three.AdjForm.usages.Contains(Adjective.AdjectiveUsage.REAR_COMPOUND_ADJ);
            bool threeValid_verb = three.VerbForm != null && three.VerbForm.standard;
            return oneValid && (twoValid_noun || twoValid_prefix || twoValid_adj) && (threeValid_noun || threeValid_adj || threeValid_verb);
        }

        public bool IsBad()
        {
            return isBad;
        }
    }
}
