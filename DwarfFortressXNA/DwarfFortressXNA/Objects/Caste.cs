using System;
using System.Collections.Generic;
using System.Linq;

namespace DwarfFortressXNA.Objects
{
    public enum CasteFlags
    {
        ADOPTS_OWNER,
        ALCHOHOL_DEPENDANT,
        ALL_ACTIVE,
        AMBUSHPREDATOR,
        AMPHIBIOUS,
        AQUATIC,
        ARENA_RESTRICTED,
        AT_PEACE_WITH_WILDLIFE,
        BENIGN,
        BLOODSUCKER,
        BONECARN,
        MEGABEAST
    }

    public enum BodyAppearanceModifierState
    {
        LOWEST,
        LOWER,
        LOW,
        MEDIAN,
        HIGH,
        HIGHER,
        HIGHEST
    }
    public class Caste
    {
        public string Name;
        public Dictionary<string, Attack> AttackList;
        public List<CasteFlags> CasteFlagList;
        public List<BodyTemplate> BodyTemplateList;
        public Dictionary<string, Dictionary<BodyAppearanceModifierState, int>> BodyAppearanceModifierList;
        public Dictionary<KeyValuePair<int, int>, int> BodySizeList;
        public Dictionary<string, Material> MaterialList;
        public Dictionary<string, Tissue> TissueList; 
        public int AttackPop;
        public int AttackCWealth;
        public int AttackEWealth;
        public int BabyDuration;
        public string BabyNameSingular;
        public string BabyNamePlural;
        public int BeachFrequency;
        public BiomeToken Biome;
        public string BloodToken;
        public State BloodState;
        public Caste(string name)
        {
            Name = name;
            AttackList = new Dictionary<string, Attack>();
            CasteFlagList = new List<CasteFlags>();
            BodyTemplateList = new List<BodyTemplate>();
            BodyAppearanceModifierList = new Dictionary<string, Dictionary<BodyAppearanceModifierState, int>>();
            BodySizeList = new Dictionary<KeyValuePair<int, int>, int>();
            MaterialList = new Dictionary<string, Material>();
            TissueList = new Dictionary<string, Tissue>();
        }

        public void ParseToken(string token, object parent)
        {
            var split = token.Split(new[] { ':' });
            if (!token.StartsWith("[")) return;
            if (RawFile.NumberOfTokens(token) > 1)
            {
                var multiple = token.Split(new[] {']'}).ToList();
                multiple.Remove("");
                for (var j = 0; j < multiple.Count; j++)
                {
                    multiple[j] = multiple[j] + "]";
                    ParseToken(multiple[j], parent);
                }
            }
            if (token.StartsWith("[ATTACK:"))
                {
                    var name = split[1];
                    var finalPartToken = "";
                    for (int i = 2; i < split.Length; i++)
                    {
                        finalPartToken += finalPartToken + ":";
                    }
                    finalPartToken = RawFile.StripTokenEnding(finalPartToken);
                    //TODO: BodyPart/Tissue search
                }
                else if (token.StartsWith("[ATTACK_TRIGGER:"))
                {
                    if(!CasteFlagList.Contains(CasteFlags.MEGABEAST)) throw new TokenParseException("Caste","Attack trigger was called without a [MEGABEAST] tag!");
                    AttackPop = RawFile.GetIntFromToken(split[1]);
                    AttackEWealth = RawFile.GetIntFromToken(split[2]);
                    AttackCWealth = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[3]));
                }
                else if (token.StartsWith("[BABY:"))
                {
                    BabyDuration = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[1]));
                }
                else if (token.StartsWith("[BABYNAME:"))
                {
                    BabyNameSingular = split[1];
                    BabyNamePlural = RawFile.StripTokenEnding(split[2]);
                }
                else if (token.StartsWith("[BEACH_FREQUENCY:"))
                {
                    BeachFrequency = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[1]));
                }
                else if (token.StartsWith("[BIOME:"))
                {
                    if(!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out Biome)) throw new TokenParseException("Caste", "Invalid biome token " + RawFile.StripTokenEnding(split[1]) + "!");
                }
                else if (token.StartsWith("[BLOOD:"))
                {
                    //TODO:Parse out MaterialTokens and look for other usages to justify creating a function
                    //For now we'll just assume all MaterialTokens are either 1 or 2 strings long and concatenate if they're 2
                    var statePos = 2;
                    if (split.Length == 3) BloodToken = split[1];
                    else
                    {
                        BloodToken = split[1] + ':' + split[2];
                        statePos++;
                    }
                    if (!Enum.TryParse(RawFile.StripTokenEnding(split[statePos]), out BloodState)) throw new TokenParseException("Caste", "Bad material state" + RawFile.StripTokenEnding(split[statePos]) + "!");
                }
                else if (token.StartsWith("[BODY:"))
                {
                    foreach (var finalToken in from iToken in split where iToken != "[BODY" select iToken.Contains("]") ? RawFile.StripTokenEnding(iToken) : iToken)
                    {
                        if(!DwarfFortress.BodyManager.BodyTemplateList.ContainsKey(finalToken)) throw new TokenParseException("Caste", "Body template " + finalToken + " hasn't been loaded yet or doesn't exist!");
                        BodyTemplateList.Add(DwarfFortress.BodyManager.BodyTemplateList[finalToken]);
                    }
                }
                else if (token.StartsWith("[BODY_APPEARANCE_MODIFIER:"))
                {
                    var id = split[1];
                    BodyAppearanceModifierList[id] = new Dictionary<BodyAppearanceModifierState, int>();
                    BodyAppearanceModifierList[id][BodyAppearanceModifierState.LOWEST] =
                        RawFile.GetIntFromToken(split[2]);
                    BodyAppearanceModifierList[id][BodyAppearanceModifierState.LOWER] =
                        RawFile.GetIntFromToken(split[3]);
                    BodyAppearanceModifierList[id][BodyAppearanceModifierState.LOW] =
                        RawFile.GetIntFromToken(split[4]);
                    BodyAppearanceModifierList[id][BodyAppearanceModifierState.MEDIAN] =
                        RawFile.GetIntFromToken(split[5]);
                    BodyAppearanceModifierList[id][BodyAppearanceModifierState.HIGH] =
                        RawFile.GetIntFromToken(split[6]);
                    BodyAppearanceModifierList[id][BodyAppearanceModifierState.HIGHER] =
                        RawFile.GetIntFromToken(split[7]);
                    BodyAppearanceModifierList[id][BodyAppearanceModifierState.HIGHEST] =
                        RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[8]));
                }
                else if (token.StartsWith("[BODY_DETAIL_PLAN:"))
                {
                    var planName = RawFile.StripTokenEnding(split[1]);
                    if (!DwarfFortress.BodyManager.BodyDetailPlanList.ContainsKey(planName)) throw new TokenParseException("Caste", "Body detail plan " + planName + " isn't defined or hasn't been parsed!");
                    BodyDetailPlan plan = DwarfFortress.BodyManager.BodyDetailPlanList[planName];
                    foreach (var material in plan.MaterialList)
                    {
                        if (!DwarfFortress.MaterialManager.MaterialTemplateList.ContainsKey(material.Value)) throw new TokenParseException("Caste", "Material template " + material.Value + " does not exist!");
                        MaterialList.Add(material.Key, DwarfFortress.MaterialManager.MaterialTemplateList[material.Value]);
                    }
                    foreach (var tissue in plan.TissueList)
                    {
                        if (!DwarfFortress.TissueManager.TissueTemplateList.ContainsKey(tissue.Value)) throw new TokenParseException("Caste", "Tissue template " + tissue.Value + " does not exist!");
                        TissueList.Add(tissue.Key, DwarfFortress.TissueManager.TissueTemplateList[tissue.Value]);
                    }
                }
                else if (token.StartsWith("[BODY_SIZE:"))
                {
                    var years = RawFile.GetIntFromToken(split[1]);
                    var days = RawFile.GetIntFromToken(split[2]);
                    var size = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[3]));
                    BodySizeList.Add(new KeyValuePair<int, int>(years, days), size);
                }
                else if (token.StartsWith("[BODYGLOSS:"))
                {
                    //TODO: Figure this out
                }
                else if (token.StartsWith("[BP_ADD_TYPE:"))
                {
                    //TODO: Figure this out
                }

                else
                {
                    CasteFlags flagBuffer;
                    if (!Enum.TryParse(RawFile.StripTokenEnding(token.Replace("[", "")), out flagBuffer))
                        return;
                    CasteFlagList.Add(flagBuffer);
                }
            }
            //TODO: AppMod Series

        }
    }
