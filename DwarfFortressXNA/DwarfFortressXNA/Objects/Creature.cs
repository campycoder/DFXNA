using System;
using System.Collections.Generic;
using System.Linq;
using DwarfFortressXNA.Managers;

namespace DwarfFortressXNA.Objects
{
    public enum ProfessionTags
    {
        MINER,
        WOODWORKER,
        CARPENTER,
        BOWYER,
        WOODCUTTER,
        STONEWORKER,
        ENGRAVER,
        MASON,
        RANGER,
        ANIMAL_CARETAKER,
        ANIMAL_TRAINER,
        HUNTER,
        TRAPPER,
        ANIMAL_DISSECTOR,
        METALSMITH,
        FURNACE_OPERATOR,
        WEAPONSMITH,
        ARMORER,
        BLACKSMITH,
        METALCRAFTER,
        JEWELER,
        GEM_CUTTER,
        GEM_SETTER,
        CRAFTSMAN,
        WOODCRAFTER,
        STONECRAFTER,
        LEATHERWORKER,
        BONE_CARVER,
        WEAVER,
        CLOTHIER,
        GLASSMAKER,
        POTTER,
        GLAZER,
        WAX_WORKER,
        STRAND_EXTRACTOR,
        FISHERY_WORKER,
        FISHERMAN,
        FISH_DISSECTOR,
        FISH_CLEANER,
        FARMER,
        CHEESE_MAKER,
        MILKER,
        COOK,
        THRESHER,
        MILLER,
        BUTCHER,
        TANNER,
        DYER,
        PLANTER,
        HERBALIST,
        BREWER,
        SOAP_MAKER,
        POTASH_MAKER,
        LYE_MAKER,
        WOOD_BURNER,
        SHEARER,
        SPINNER,
        PRESSER,
        BEEKEEPER,
        ENGINEER,
        MECHANIC,
        SIEGE_ENGINEER,
        SIEGE_OPERATOR,
        PUMP_OPERATOR,
        CLERK,
        ADMINISTRATOR,
        TRADER,
        ARCHITECT,
        ALCHEMIST,
        DOCTOR,
        DIAGNOSER,
        BONE_SETTER,
        SUTURER,
        SURGEON,
        MERCHANT,
        HAMMERMAN,
        MASTER_HAMMERMAN,
        SPEARMAN,
        MASTER_SPEARMAN,
        CROSSBOWMAN,
        MASTER_CROSSBOWMAN,
        WRESTLER,
        MASTER_WRESTLER,
        AXEMAN,
        MASTER_AXEMAN,
        SWORDSMAN,
        MASTER_SWORDSMAN,
        MACEMAN,
        MASTER_MACEMAN,
        PIKEMAN,
        MASTER_PIKEMAN,
        BOWMAN,
        MASTER_BOWMAN,
        BLOWGUNMAN,
        MASTER_BLOWGUNMAN,
        LASHER,
        MASTER_LASHER,
        RECRUIT,
        TRAINED_HUNTER,
        TRAINED_WAR,
        MASTER_THIEF,
        THIEF,
        STANDARD,
        CHILD,
        BABY,
        DRUNK,
        MONSTER_SLAYER,
        SCOUT,
        BEAST_HUNTER,
        SNATCHER,
        MERCENARY
    }
    public enum CreatureTags
    {
        ARTIFICAL_HIVEABLE,
        EQUIPMENT_WAGON,
        DOES_NOT_EXIST,
        EVIL,
        FANCIFUL,
        GENERATED,
        GOOD,
        LARGE_ROAMING,
        LOOSE_CLUSTERS,
        MUNDANE,
        SAVAGE,

    }
    /// <summary>
    /// The primary templating for every creature - not to be confused with CreatureInstance - a planned (2015-05-05) class
    /// which will handle ACTIVE INSTANCES of these creatures. All of this information is to template the actual objects, and
    /// provide them with a frame of reference - the actual instance will be a handle to this data.
    /// </summary>
    public class Creature
    {
        public List<string> GlobalCasteTokens;
        public List<string> CastesSelected;
        public Dictionary<string, Caste> CasteList;
        public List<CreatureTags> TagList;
        public Dictionary<ProfessionTags, KeyValuePair<string, string>> ProfessionList; 
        public char Tile;
        public char AltTile;
        public char SoldierTile;
        public BiomeToken Biome;
        public int Frequency = 50;
        public int MinCluster;
        public int MaxCluster;
        public ColorPair Color;
        public string Class;
        public bool Exists = true;
        public string BabyNameSingular;
        public string BabyNamePlural;
        public string ChildNameSingular;
        public string ChildNamePlural;
        public string NameSingular = "nothing";
        public string NamePlural = "nothings";
        public string NameAdjective = "nothing";
        public ColorPair GlowColor;
        public char GlowTile;
        public int Hfid;
        public int PopulationMin;
        public int PopulationMax;
        public string PrefString;
        public Creature(List<string> tokenList)
        {
            CasteList = new Dictionary<string, Caste>();
            GlobalCasteTokens = new List<string>();
            //Here it is.
            //The big one.
            //Three hundred and some odd tokens to be parsed.
            //Started 2015-05-04
            for (var i = 0; i < tokenList.Count; i++)
            {
                var split = tokenList[i].Split(new[] {':'});
                if(!tokenList[i].StartsWith("[")) continue;
                if(RawFile.NumberOfTokens(tokenList[i]) > 1)
                {
                    var multiple = tokenList[i].Split(new[] { ']' }).ToList();
                    multiple.Remove("");
                    for(var j = 0;j < multiple.Count; j++)
                    {
                        multiple[j] = multiple[j] + "]";
                    }
                    tokenList.Remove(tokenList[i]);
                    tokenList.InsertRange(i, multiple);
                }
                if (tokenList[i].StartsWith("[ALTTILE"))
                {
                    var strippedChar = RawFile.StripTokenEnding(split[1]);
                    if (strippedChar.StartsWith("'"))
                    {
                        strippedChar = strippedChar.Replace("'", "");
                        AltTile = strippedChar[0];
                    }
                    else AltTile = DwarfFortress.FontManager.Codepage[RawFile.GetIntFromToken(strippedChar)];
                }
                else if (tokenList[i].StartsWith("[BIOME"))
                {
                    if (!Enum.TryParse(tokenList[i].Split(new[] { ':' })[1], out Biome)) throw new TokenParseException("Creature", "Biome token " + tokenList[i] + " incorrect!");
                }
                else if (tokenList[i].StartsWith("[CASTE"))
                {
                    var name = RawFile.StripTokenEnding(split[1]);
                    var caste = new Caste(name);
                    foreach (var t in GlobalCasteTokens)
                    {
                        caste.ParseToken(t);
                    }
                    if (!CasteList.ContainsKey(name)) CasteList.Add(name, caste);
                }
                else if (tokenList[i].StartsWith("[CHANGE_FREQUENCY_PERC"))
                {
                    Frequency *=
                        (RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[1])));
                }
                else if (tokenList[i].StartsWith("[CLUSTER_NUMBER"))
                {
                    MinCluster = RawFile.GetIntFromToken(split[1]);
                    MaxCluster = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[2]));
                }
                else if (tokenList[i].StartsWith("[COLOR"))
                {
                    var fore = RawFile.GetIntFromToken(split[1]);
                    var back = RawFile.GetIntFromToken(split[2]);
                    var bright = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[3]));
                    Color = DwarfFortress.FontManager.ColorManager.GetPairFromTriad(fore, back, bright);
                }
                else if (tokenList[i].StartsWith("[CREATURE_CLASS"))
                {
                    Class = RawFile.StripTokenEnding(split[1]);
                }
                else if (tokenList[i].StartsWith("[CREATURE_SOLDIER_TILE"))
                {
                    SoldierTile = DwarfFortress.FontManager.GetCharFromToken(tokenList[i]);
                }
                else if (tokenList[i].StartsWith("[CREATURE_TILE"))
                {
                    Tile = DwarfFortress.FontManager.GetCharFromToken(tokenList[i]);
                }
                else if (tokenList[i] == "[DOES_NOT_EXIST]")
                {
                    Exists = false;
                }
                else if (tokenList[i].StartsWith("[FREQUENCY"))
                {
                    var number = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[1]));
                    if (number > 100) throw new TokenParseException("Creature", "Bad frequency value " + number + "!");
                }
                else if (tokenList[i].StartsWith("[GENERAL_BABY_NAME"))
                {
                    BabyNameSingular = split[1];
                    BabyNamePlural = RawFile.StripTokenEnding(split[2]);
                }
                else if (tokenList[i].StartsWith("[GENERAL_CHILD_NAME"))
                {
                    ChildNameSingular = split[1];
                    ChildNamePlural = RawFile.StripTokenEnding(split[2]);
                }
                else if (tokenList[i].StartsWith("[GLOWCOLOR"))
                {
                    var fore = RawFile.GetIntFromToken(split[1]);
                    var back = RawFile.GetIntFromToken(split[2]);
                    var bright = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[3]));
                    GlowColor = DwarfFortress.FontManager.ColorManager.GetPairFromTriad(fore, back, bright);
                }
                else if (tokenList[i].StartsWith("[GLOWTILE"))
                {
                   GlowTile = DwarfFortress.FontManager.GetCharFromToken(tokenList[i]);
                }
                    //TODO: [HIVE_PRODUCT] on Item implementation
                    //TODO: SelectMaterial, PlusMaterial
                    //TODO: RemoveMaterial, RemoveTissue
                else if (tokenList[i].StartsWith("[NAME"))
                {
                    NameSingular = split[1];
                    NamePlural = split[2];
                    NameAdjective = RawFile.StripTokenEnding(split[3]);
                }
                else if (tokenList[i].StartsWith("[POPULATION_NUMBER"))
                {
                    PopulationMin = RawFile.GetIntFromToken(split[1]);
                    PopulationMax = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[2]));
                }
                else if (tokenList[i].StartsWith("PREFSTRING"))
                {
                    PrefString = RawFile.StripTokenEnding(split[1]);
                }
                else if (tokenList[i].StartsWith("[PROFESSION_NAME"))
                {
                    ProfessionTags professionTags;
                    if (!Enum.TryParse(split[1], out professionTags)) return;
                    var single = split[2];
                    var plural = RawFile.StripTokenEnding(split[3]);
                    ProfessionList.Add(professionTags, new KeyValuePair<string, string>(single, plural));
                }
                else if (tokenList[i].StartsWith("[SELECT_CASTE"))
                {
                    CastesSelected.Clear();
                    if(!CasteList.ContainsKey(RawFile.StripTokenEnding(split[1]))) throw new TokenParseException("Creature", "Bad caste name " + RawFile.StripTokenEnding(split[1]) + "!");
                    CastesSelected.Add(RawFile.StripTokenEnding(split[1]));
                }
                else if (tokenList[i].StartsWith("[SELECT_ADDITIONAL_CASTE"))
                {
                    if(CastesSelected.Count == 0) throw new TokenParseException("Creature","No cast defined for additional operation!");
                    var name = RawFile.StripTokenEnding(split[1]);
                    if (!CasteList.ContainsKey(name)) throw new TokenParseException("Creature", "Bad caste name " + RawFile.StripTokenEnding(split[1]) + "!");
                    CastesSelected.Add(name);
                }
                else
                {
                    CreatureTags tagBuffer;
                    if (Enum.TryParse(RawFile.StripTokenEnding(tokenList[i].Replace("[", "")), out tagBuffer))
                    {
                        TagList.Add(tagBuffer);
                    }
                    else
                    {
                        if (CastesSelected.Count == 0)
                        {
                            GlobalCasteTokens.Add(tokenList[i]);
                        }
                        else if (CastesSelected.Contains("ALL"))
                        {
                            foreach (var caste in CasteList.Values)
                            {
                                caste.ParseToken(tokenList[i]);
                            }
                        }
                        else
                        {
                            foreach (var name in CastesSelected)
                            {
                                if (!CasteList.ContainsKey(name))
                                    throw new Exception("This caste name (" + name + ") shouldn't be here!");
                                CasteList[name].ParseToken(tokenList[i]);
                            }
                        }
                    }
                }
            } 
        }
    }
}
