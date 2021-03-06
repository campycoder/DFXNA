﻿using System;
using System.Collections.Generic;
using System.Linq;
using DwarfFortressXNA.Managers;

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
        CAN_LEARN,
        CAN_SPEAK,
        CANNOT_CLIMB,
        CANNOT_JUMP,
        CANNOT_UNDEAD,
        CANOPENDOORS,
        CARNIVORE,
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
        public string ParentName;
        public Dictionary<string, Attack> AttackList;
        public List<CasteFlags> CasteFlagList;
        public Dictionary<string, BodyPart> BodyPartList;
        public Dictionary<string, Dictionary<BodyAppearanceModifierState, int>> BodyAppearanceModifierList;
        public Dictionary<Tuple<int, int>, int> BodySizeList;
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
        public Material BloodMaterial;
        public State BloodState;
        public int BuildingDestroyer;
        public char AltTile;
        public ColorPair Color;
        public Dictionary<string, InteractionUsage> InteractionUsageList;
        public string SelectedInteractionUsage;
        public Caste(string name, string parentName)
        {
            Name = name;
            ParentName = parentName;
            AttackList = new Dictionary<string, Attack>();
            CasteFlagList = new List<CasteFlags>();
            BodyPartList = new Dictionary<string, BodyPart>();
            BodyAppearanceModifierList = new Dictionary<string, Dictionary<BodyAppearanceModifierState, int>>();
            BodySizeList = new Dictionary<Tuple<int, int>, int>();
            MaterialList = new Dictionary<string, Material>();
            TissueList = new Dictionary<string, Tissue>();
            InteractionUsageList = new Dictionary<string, InteractionUsage>();
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
                var category = split[3];
                var param = RawFile.StripTokenEnding(split[4]);
                var parts = BodyPartSearch(category, param);
                if(parts.Count == 0) DwarfFortress.ThrowError("Catse","Bad search: Category - " + category + " Token - " + param + ".");
                AttackList.Add(name, new Attack(name, parts));
                    
            }
            else if (token.StartsWith("[ATTACK_TRIGGER:"))
            {
                if(!CasteFlagList.Contains(CasteFlags.MEGABEAST)) DwarfFortress.ThrowError("Caste","Attack trigger was called without a [MEGABEAST] tag!");
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
                if(!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out Biome)) DwarfFortress.ThrowError("Caste", "Invalid biome token " + RawFile.StripTokenEnding(split[1]) + "!");
            }
            else if (token.StartsWith("[BLOOD:"))
            {
                var statePos = 2;
                if (split.Length == 3) BloodMaterial = DwarfFortress.MaterialManager.MaterialSearch(split[1], "NONE");
                else 
                {
                    BloodMaterial = DwarfFortress.MaterialManager.MaterialSearch(split[1], split[2], parent);
                    statePos++;
                }
                if (!Enum.TryParse(RawFile.StripTokenEnding(split[statePos]), out BloodState)) DwarfFortress.ThrowError("Caste", "Bad material state" + RawFile.StripTokenEnding(split[statePos]) + "!");
            }
            else if (token.StartsWith("[BODY:"))
            {
                foreach (var finalToken in from iToken in split where iToken != "[BODY" select iToken.Contains("]") ? RawFile.StripTokenEnding(iToken) : iToken)
                {
                    if(!DwarfFortress.BodyManager.BodyTemplateList.ContainsKey(finalToken)) DwarfFortress.ThrowError("Caste", "Body template " + finalToken + " hasn't been loaded yet or doesn't exist!");
                    foreach (var pair in DwarfFortress.BodyManager.BodyTemplateList[finalToken].BodyPartList)
                    {
                        if(BodyPartList.ContainsKey(pair.Key)) DwarfFortress.ThrowError("Caste","Body part " + pair.Key + " has already been added to caste!");
                        BodyPartList.Add(pair.Key, pair.Value);
                    }
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
                if (!DwarfFortress.BodyManager.BodyDetailPlanList.ContainsKey(planName)) DwarfFortress.ThrowError("Caste", "Body detail plan " + planName + " isn't defined or hasn't been parsed!");
                BodyDetailPlan plan = DwarfFortress.BodyManager.BodyDetailPlanList[planName];
                foreach (var material in plan.MaterialList)
                {
                    if (!DwarfFortress.MaterialManager.MaterialTemplateList.ContainsKey(material.Value)) DwarfFortress.ThrowError("Caste", "Material template " + material.Value + " does not exist!");
                    MaterialList.Add(material.Key, DwarfFortress.MaterialManager.MaterialTemplateList[material.Value]);
                }
                foreach (var tissue in plan.TissueList)
                {
                    if (!DwarfFortress.TissueManager.TissueTemplateList.ContainsKey(tissue.Value)) DwarfFortress.ThrowError("Caste", "Tissue template " + tissue.Value + " does not exist!");
                    TissueList.Add(tissue.Key, DwarfFortress.TissueManager.TissueTemplateList[tissue.Value]);
                }
            }
            else if (token.StartsWith("[BODY_SIZE:"))
            {
                var years = RawFile.GetIntFromToken(split[1]);
                var days = RawFile.GetIntFromToken(split[2]);
                var size = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[3]));
                BodySizeList.Add(new Tuple<int, int>(years, days), size);
            }
            else if (token.StartsWith("[BODYGLOSS:"))
            {
                var glossName = RawFile.StripTokenEnding(split[1]);
                if(!DwarfFortress.BodyManager.BodyGlossList.ContainsKey(glossName)) DwarfFortress.ThrowError("Caste","Bad BodyGloss " + glossName + "!");
                var gloss = DwarfFortress.BodyManager.BodyGlossList[glossName];
                foreach (var bodyPart in BodyPartList.Keys)
                {
                    if (BodyPartList[bodyPart].IndividualNames != null)
                    {
                        for (var i = 0; i < BodyPartList[bodyPart].IndividualNames.Count; i++)
                        {
                            BodyPartList[bodyPart].IndividualNames[i] = BodyPartList[bodyPart].IndividualNames[i].Replace(gloss.Singular.Item1, gloss.Singular.Item2);
                        } 
                    }
                    if (BodyPartList[bodyPart].IndividualPlurals != null)
                    {
                        for (var i = 0; i < BodyPartList[bodyPart].IndividualPlurals.Count; i++)
                        {
                            BodyPartList[bodyPart].IndividualPlurals[i] = BodyPartList[bodyPart].IndividualPlurals[i].Replace(gloss.Plural.Item1, gloss.Plural.Item2);
                        } 
                    }
                    BodyPartList[bodyPart].Name = BodyPartList[bodyPart].Name.Replace(gloss.Singular.Item1, gloss.Singular.Item2);
                    BodyPartList[bodyPart].Plural = BodyPartList[bodyPart].Plural.Replace(gloss.Plural.Item1, gloss.Plural.Item2);
                }
            }
            else if (token.StartsWith("[BP_ADD_TYPE:"))
            {
                //TODO: Figure this out
            }
            else if (token.StartsWith("[BP_APPEARANCE_MODIFIER:"))
            {
                //TODO: Figure this out
            }
            else if (token.StartsWith("[BUILDINGDESTROYER:"))
            {
                var destroyLevel = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[1]));
                if (destroyLevel > 2) destroyLevel = 2;
                if (destroyLevel < 0) destroyLevel = 1;
                BuildingDestroyer = destroyLevel;
            }
            else if (token.StartsWith("[CAN_DO_INTERACTION:"))
            {
                var interactionName = RawFile.StripTokenEnding(split[1]);
                if(!DwarfFortress.InteractionManager.InteractionList.ContainsKey(interactionName)) DwarfFortress.ThrowError("Caste", "Interaction " + interactionName + " doesn't exist or hasn't been loaded!");
                InteractionUsageList.Add(interactionName, new InteractionUsage(MaterialList));
                SelectedInteractionUsage = interactionName;
            }
            else if (token.StartsWith("[CDI:"))
            {
                InteractionUsageList[SelectedInteractionUsage].ParseToken(token);
            }
            else if (token.StartsWith("[CASTE_ALTTILE:"))
            {
                var strippedChar = RawFile.StripTokenEnding(split[1]);
                if (strippedChar.StartsWith("'"))
                {
                    strippedChar = strippedChar.Replace("'", "");
                    AltTile = strippedChar[0];
                }
                else AltTile = DwarfFortress.FontManager.Codepage[RawFile.GetIntFromToken(strippedChar)];
            }
            else if (token.StartsWith("[CASTE_COLOR:"))
            {
                var fore = RawFile.GetIntFromToken(split[1]);
                if (fore > 7 || fore < 0) DwarfFortress.ThrowError("Caste", "CASTE_COLOR component foreground invalid (Higher than seven or lower than zero): " + fore + "!");
                var back = RawFile.GetIntFromToken(split[2]);
                if (back > 7 || back < 0) DwarfFortress.ThrowError("Caste", "CASTE_COLOR component background invalid (Higher than seven or lower than zero): " + back + "!");
                var bright = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[3]));
                if(bright != 0 && bright != 1) DwarfFortress.ThrowError("Caste", "CASTE_COLOR component brightness invalid (Not one or zero): " + bright + "!");
                Color = DwarfFortress.FontManager.ColorManager.GetPairFromTriad(fore, back, bright);
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

        public List<string> BodyPartSearch(string type, string param)
        {
            var bodyPartReturn = new List<string>();
            switch (type)
            {
                case "BY_CATEGORY":
                    bodyPartReturn.AddRange(from pair in BodyPartList where pair.Value.Category == param select pair.Key);
                    break;
                case "BY_TYPE":
                    BodyPartProperties property;
                    if(!Enum.TryParse(param, out property)) DwarfFortress.ThrowError("Caste", "Bad BodyPartProperty " + param + "!");
                    bodyPartReturn.AddRange(from pair in BodyPartList where pair.Value.BodyPartProprtiesList.Contains(property) select pair.Key);
                    break;
                case "BY_TOKEN":
                    bodyPartReturn.Add(BodyPartList[param].Name);
                    break;
            }
            return bodyPartReturn;
        }
    }
}
