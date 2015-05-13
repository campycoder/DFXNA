using System;
using System.Collections.Generic;
using System.Linq;
using DwarfFortressXNA.Managers;

namespace DwarfFortressXNA.Objects
{
    public enum Spheres
    {
        AGRICULTURE,
        ANIMALS,
        ART,
        BALANCE,
        BEAUTY,
        BIRTH,
        BLIGHT,
        BOUNDARIES,
        CAVERNS,
        CHAOS,
        CHARITY,
        CHILDREN,
        COASTS,
        CONSOLATION,
        COURAGE,
        CRAFTS,
        CREATION,
        DANCE,
        DARKNESS,
        DAWN,
        DAY,
        DEATH,
        DEFORMITY,
        DEPRAVITY,
        DISCIPLINE,
        DISEASE,
        DREAMS,
        DUSK,
        DUTY,
        EARTH,
        FAMILY,
        FAME,
        FATE,
        FERTILITY,
        FESTIVALS,
        FIRE,
        FISH,
        FISHING,
        FOOD,
        FORGIVENESS,
        FORTRESSES,
        FREEDOM,
        GAMBLING,
        GAMES,
        GENEROSITY,
        HAPPINESS,
        HEALING,
        HOSPITALITY,
        HUNTING,
        INSPIRATION,
        JEALOUSY,
        JEWELS,
        JUSTICE,
        LABOR,
        LAKES,
        LAWS,
        LIES,
        LIGHT,
        LIGHTNING,
        LONGEVITY,
        LOVE,
        LOYALTY,
        LUCK,
        LUST,
        MARRIAGE,
        MERCY,
        METALS,
        MINERALS,
        MISERY,
        MIST,
        MOON,
        MOUNTAINS,
        MUCK,
        MURDER,
        MUSIC,
        NATURE,
        NIGHT,
        NIGHTMARES,
        OATHS,
        OCEANS,
        ORDER,
        PAINTING,
        PEACE,
        PERSUASION,
        PLANTS,
        POETRY,
        PREGNANCY,
        RAIN,
        RAINBOWS,
        REBIRTH,
        REVELRY,
        REVENGE,
        RIVERS,
        RULERSHIP,
        RUMORS,
        SACRIFICE,
        SALT,
        SCHOLARSHIP,
        SEASONS,
        SILENCE,
        SKY,
        SONG,
        SPEECH,
        STARS,
        STORMS,
        STRENGTH,
        SUICIDE,
        SUN,
        THEFT,
        THRALLDOM,
        THUNDER,
        TORTURE,
        TRADE,
        TRAVELERS,
        TREACHERY,
        TREES,
        TRICKERY,
        TRUTH,
        TWILIGHT,
        VALOR,
        VICTORY,
        VOLCANOS,
        WAR,
        WATER,
        WEALTH,
        WEATHER,
        WIND,
        WISDOM,
        WRITING,
        YOUTH
    }
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
        public Dictionary<string, Caste> CasteList;
        public Dictionary<string, Material> MaterialList;
        public Dictionary<string, Tissue> TissueList; 
        public List<CreatureTags> TagList;
        public List<string> CurrentSelectedCastes;
        public List<string> CurrentSelectedMaterials;
        public List<string> CurrentSelectedTissues; 
        public Dictionary<ProfessionTags, KeyValuePair<string, string>> ProfessionList; 
        public char Tile;
        public char AltTile;
        public char SoldierTile;
        public List<BiomeToken> BiomeList;
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
        public bool Savage;
        public int SmellTrigger;
        public string Speech;
        public string SpeechMale;
        public string SpeechFemale;
        public List<Spheres> SphereList;
        public int TriggerableGroupMin;
        public int TriggerableGroupMax;
        public bool Ubiquitous;
        public int UndergroundDepthMin;
        public int UndergroundDepthMax;
        public bool VerminEater;
        public bool VerminFish;
        public bool VerminGrounder;
        public bool VerminRotter;
        public bool VerminSoil;
        public bool VerminSoilColony;
        public Creature(List<string> tokenList)
        {
            CasteList = new Dictionary<string, Caste>();
            MaterialList = new Dictionary<string, Material>();
            TissueList = new Dictionary<string, Tissue>();
            CurrentSelectedCastes = new List<string>();
            CurrentSelectedMaterials = new List<string>();
            CurrentSelectedTissues = new List<string>();
            GlobalCasteTokens = new List<string>();
            TagList = new List<CreatureTags>();
            SphereList = new List<Spheres>();
            ProfessionList = new Dictionary<ProfessionTags, KeyValuePair<string, string>>();
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
                if (tokenList[i].StartsWith("[ALTTILE:"))
                {
                    var strippedChar = RawFile.StripTokenEnding(split[1]);
                    if (strippedChar.StartsWith("'"))
                    {
                        strippedChar = strippedChar.Replace("'", "");
                        AltTile = strippedChar[0];
                    }
                    else AltTile = DwarfFortress.FontManager.Codepage[RawFile.GetIntFromToken(strippedChar)];
                }
                else if (tokenList[i].StartsWith("[BIOME:"))
                {
                    BiomeToken buffer;
                    if (!Enum.TryParse(tokenList[i].Split(new[] {':'})[1], out buffer))
                    {
                        //throw new TokenParseException("Creature", "Biome token " + tokenList[i] + " incorrect!");
                    }
                    else
                    {
                        BiomeList.Add(buffer);
                    }
                }
                else if (tokenList[i].StartsWith("[CASTE:"))
                {
                    var name = RawFile.StripTokenEnding(split[1]);
                    var caste = new Caste(name);
                    foreach (var t in GlobalCasteTokens)
                    {
                        caste.ParseToken(t, this);
                    }
                    foreach (var materialPair in MaterialList)
                    {
                        caste.MaterialList.Add(materialPair.Key, materialPair.Value);
                    }
                    foreach (var tissuePair in TissueList)
                    {
                        caste.TissueList.Add(tissuePair.Key, tissuePair.Value);
                    }
                    if (!CasteList.ContainsKey(name)) CasteList.Add(name, caste);
                }
                else if (tokenList[i].StartsWith("[CHANGE_FREQUENCY_PERC:"))
                {
                    Frequency *=
                        (RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[1])));
                }
                else if (tokenList[i].StartsWith("[CLUSTER_NUMBER:"))
                {
                    MinCluster = RawFile.GetIntFromToken(split[1]);
                    MaxCluster = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[2]));
                }
                else if (tokenList[i].StartsWith("[COLOR:"))
                {
                    var fore = RawFile.GetIntFromToken(split[1]);
                    var back = RawFile.GetIntFromToken(split[2]);
                    var bright = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[3]));
                    Color = DwarfFortress.FontManager.ColorManager.GetPairFromTriad(fore, back, bright);
                }
                else if (tokenList[i].StartsWith("[CREATURE_CLASS:"))
                {
                    Class = RawFile.StripTokenEnding(split[1]);
                }
                else if (tokenList[i].StartsWith("[CREATURE_SOLDIER_TILE:"))
                {
                    SoldierTile = DwarfFortress.FontManager.GetCharFromToken(tokenList[i]);
                }
                else if (tokenList[i].StartsWith("[CREATURE_TILE:"))
                {
                    Tile = DwarfFortress.FontManager.GetCharFromToken(tokenList[i]);
                }
                else if (tokenList[i] == "[DOES_NOT_EXIST]")
                {
                    Exists = false;
                }
                else if (tokenList[i].StartsWith("[FREQUENCY:"))
                {
                    var number = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[1]));
                    if (number > 100) throw new TokenParseException("Creature", "Bad frequency value " + number + "!");
                }
                else if (tokenList[i].StartsWith("[GENERAL_BABY_NAME:"))
                {
                    BabyNameSingular = split[1];
                    BabyNamePlural = RawFile.StripTokenEnding(split[2]);
                }
                else if (tokenList[i].StartsWith("[GENERAL_CHILD_NAME:"))
                {
                    ChildNameSingular = split[1];
                    ChildNamePlural = RawFile.StripTokenEnding(split[2]);
                }
                else if (tokenList[i].StartsWith("[GLOWCOLOR:"))
                {
                    var fore = RawFile.GetIntFromToken(split[1]);
                    var back = RawFile.GetIntFromToken(split[2]);
                    var bright = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[3]));
                    GlowColor = DwarfFortress.FontManager.ColorManager.GetPairFromTriad(fore, back, bright);
                }
                else if (tokenList[i].StartsWith("[GLOWTILE:"))
                {
                   GlowTile = DwarfFortress.FontManager.GetCharFromToken(tokenList[i]);
                }
                    //TODO: [HIVE_PRODUCT] on Item implementation
                else if (tokenList[i].StartsWith("[SELECT_MATERIAL:"))
                {
                    var materialName = RawFile.StripTokenEnding(split[1]);
                    if(!MaterialList.ContainsKey(materialName) && materialName != "ALL") throw new TokenParseException("Creature", "Material " + materialName + " does not exist in creature!");
                    CurrentSelectedMaterials.Clear();
                    CurrentSelectedMaterials.Add(materialName);
                }
                else if (tokenList[i].StartsWith("[PLUS_MATERIAL"))
                {
                    var materialName = RawFile.StripTokenEnding(split[1]);
                    if(materialName == "ALL") throw new TokenParseException("Creature", "PLUS_MATERIAL cannot add ALL!");
                    if(CurrentSelectedMaterials.Contains("ALL") || CurrentSelectedMaterials.Contains(materialName)) throw new TokenParseException("Creature", "Redundant PLUS_MATERIAL for " + materialName + ": you've already selected either ALL or the token!");
                    if (!MaterialList.ContainsKey(materialName)) throw new TokenParseException("Creature", "Material " + materialName + " does not exist in creature!");
                    CurrentSelectedMaterials.Add(materialName);
                }
                else if (tokenList[i].StartsWith("[REMOVE_MATERIAL:"))
                {
                    var materialName = RawFile.StripTokenEnding(split[1]);
                    if(!MaterialList.ContainsKey(materialName)) throw new TokenParseException("Creature", "Material " + materialName + " does not exist in creature!");
                    MaterialList.Remove(materialName);
                    if(CurrentSelectedMaterials.Contains(materialName)) throw new TokenParseException("Creature", "Bad remove: Material " + materialName + " is still selected!");
                }
                else if (tokenList[i].StartsWith("[MATERIAL:"))
                {
                    throw new Exception("This actually happened!");
                }
                else if (tokenList[i].StartsWith("[SELECT_TISSUE:"))
                {
                    var tissueName = RawFile.StripTokenEnding(split[1]);
                    if(!TissueList.ContainsKey(tissueName)) throw new TokenParseException("Creature", "Tissue " + tissueName + " does not exist in creature!");
                    CurrentSelectedTissues.Clear();
                    CurrentSelectedTissues.Add(tissueName);
                }
                else if (tokenList[i].StartsWith("[PLUS_TISSUE:"))
                {
                    var tissueName = RawFile.StripTokenEnding(split[1]);
                    if (tissueName == "ALL") throw new TokenParseException("Creature", "PLUS_TISSUE cannot add ALL!");
                    if(CurrentSelectedTissues.Contains("ALL") || CurrentSelectedTissues.Contains(tissueName)) throw new TokenParseException("Creature", "Redundant PLUS_TISSUE for " + tissueName + ": you've already selected either ALL or the token!");
                    if (!TissueList.ContainsKey(tissueName)) throw new TokenParseException("Creature", "Tissue " + tissueName + " does not exist in creature!");
                    CurrentSelectedTissues.Add(tissueName);
                }
                else if (tokenList[i].StartsWith("[REMOVE_TISSUE:"))
                {
                    var tissueName = RawFile.StripTokenEnding(split[1]);
                    if(!TissueList.ContainsKey(tissueName)) throw new TokenParseException("Creature", "Tissue " + tissueName + " does not exist in creature!");
                    TissueList.Remove(tissueName);
                    if(CurrentSelectedTissues.Contains(tissueName)) throw new TokenParseException("Creature", "Bad remove: Tissue " + tissueName + " is still selected!");
                }
                else if (tokenList[i].StartsWith("[TISSUE:"))
                {
                    throw new Exception("This actually happened!");
                }
                else if (tokenList[i].StartsWith("[NAME:"))
                {
                    NameSingular = split[1];
                    NamePlural = split[2];
                    NameAdjective = RawFile.StripTokenEnding(split[3]);
                }
                else if (tokenList[i].StartsWith("[POPULATION_NUMBER:"))
                {
                    PopulationMin = RawFile.GetIntFromToken(split[1]);
                    PopulationMax = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[2]));
                }
                else if (tokenList[i].StartsWith("[PREFSTRING:"))
                {
                    PrefString = RawFile.StripTokenEnding(split[1]);
                }
                else if (tokenList[i].StartsWith("[PROFESSION_NAME:"))
                {
                    ProfessionTags professionTags;
                    if (!Enum.TryParse(split[1], out professionTags)) return;
                    var single = split[2];
                    var plural = RawFile.StripTokenEnding(split[3]);
                    ProfessionList.Add(professionTags, new KeyValuePair<string, string>(single, plural));
                }
                else if (tokenList[i] == "[SAVAGE]")
                {
                    Savage = true;
                }
                else if (tokenList[i].StartsWith("[SMELL_TRIGGER:"))
                {
                    SmellTrigger = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[1]));
                    if (SmellTrigger > 10000) SmellTrigger = 10000;
                }
                else if (tokenList[i].StartsWith("[SOLDIER_ALTTILE:"))
                {
                    SoldierTile = DwarfFortress.FontManager.GetCharFromToken(RawFile.StripTokenEnding(split[1]));
                }
                else if (tokenList[i].StartsWith("[SPEECH:"))
                {
                    Speech = RawFile.StripTokenEnding(split[1]);
                }
                else if (tokenList[1].StartsWith("[SPEECH_FEMALE:"))
                {
                    SpeechFemale = RawFile.StripTokenEnding(split[1]);
                }
                else if (tokenList[1].StartsWith("[SPEECH_MALE:"))
                {
                    SpeechMale = RawFile.StripTokenEnding(split[1]);
                }
                else if (tokenList[i].StartsWith("[SPHERE:"))
                {
                    Spheres sphereBuffer;
                    if(!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out sphereBuffer)) throw new TokenParseException("Creature", "Sphere " + RawFile.StripTokenEnding(split[1]) + " is invalid!");
                    SphereList.Add(sphereBuffer);
                }
                else if (tokenList[i] == "[UBIQUITOUS]")
                {
                    Ubiquitous = true;
                }
                else if (tokenList[i].StartsWith("[UNDERGROUND_DEPTH:"))
                {
                    UndergroundDepthMin = RawFile.GetIntFromToken(split[1]);
                    if (UndergroundDepthMin < 0 && UndergroundDepthMin > 5) throw new TokenParseException("Creature","UndergroundDepthMin invalid: " + UndergroundDepthMin + "!");
                    UndergroundDepthMax = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[2]));
                    if(UndergroundDepthMax < 0 && UndergroundDepthMax > 5) throw new TokenParseException("Creature", "UndergroundDepthMax invalid: " + UndergroundDepthMax + "!");
                }
                //TODO: UseTissue,UseTissueTemplate
                else if (tokenList[i].StartsWith("[USE_MATERIAL:"))
                {
                    var oldMaterialName = RawFile.StripTokenEnding(split[2]);
                    if(!MaterialList.ContainsKey(oldMaterialName)) throw new TokenParseException("Creature", "Old material " + oldMaterialName + " does not exist!");
                    var newMaterialName = split[1];
                    if(MaterialList.ContainsKey(newMaterialName)) throw new TokenParseException("Creature", "Material " + newMaterialName + " has already been defined!");
                    Material newMaterial = MaterialList[oldMaterialName];
                    MaterialList.Add(newMaterialName, newMaterial);
                }
                else if (tokenList[i].StartsWith("[USE_MATERIAL_TEMPLATE:"))
                {
                    var materialTemplateName = RawFile.StripTokenEnding(split[2]);
                    if(!DwarfFortress.MaterialManager.MaterialTemplateList.ContainsKey(materialTemplateName)) throw new TokenParseException("Creature", "Material template " + materialTemplateName + " hasn't been defined or parsed!");
                    var newMaterialName = split[1];
                    if(MaterialList.ContainsKey(newMaterialName)) throw new TokenParseException("Creature", "Material " + newMaterialName + " has already been defined!");
                    Material newMaterial = DwarfFortress.MaterialManager.MaterialTemplateList[materialTemplateName];
                    MaterialList.Add(newMaterialName, newMaterial);
                }
                else if (tokenList[i] == "[USE_CASTE:")
                {
                    var oldCasteName = RawFile.StripTokenEnding(split[2]);
                    if(!CasteList.ContainsKey(oldCasteName)) throw new TokenParseException("Creature", "Bad old caste name " + oldCasteName + "!");
                    var newCasteName = split[1];
                    if(CasteList.ContainsKey(newCasteName)) throw new TokenParseException("Creature", "Caste " + newCasteName + " already exists!");
                    var newCaste = CasteList[oldCasteName];
                    CasteList.Add(newCasteName, newCaste);
                    CurrentSelectedCastes.Clear();
                    CurrentSelectedCastes.Add(newCasteName);
                }
                else if (tokenList[i] == "[VERMIN_EATER]")
                {
                    VerminEater = true;
                }
                else if (tokenList[i] == "[VERMIN_FISH]")
                {
                    VerminFish = true;
                }
                else if (tokenList[i] == "[VERMIN_GROUNDER")
                {
                    VerminGrounder = true;
                }
                else if (tokenList[i] == "[VERMIN_ROTTER]")
                {
                    VerminRotter = true;
                }
                else if (tokenList[i] == "[VERMIN_SOIL]")
                {
                    VerminSoil = true;
                }
                else if (tokenList[i] == "[VERMIN_SOIL_COLONY]")
                {
                    VerminSoilColony = true;
                }
                else if (tokenList[i].StartsWith("[SELECT_CASTE:"))
                {
                    CurrentSelectedCastes.Clear();
                    if(!CasteList.ContainsKey(RawFile.StripTokenEnding(split[1])) && RawFile.StripTokenEnding(split[1]) != "ALL") throw new TokenParseException("Creature", "Bad caste name " + RawFile.StripTokenEnding(split[1]) + "!");
                    CurrentSelectedCastes.Add(RawFile.StripTokenEnding(split[1]));
                }
                else if (tokenList[i].StartsWith("[SELECT_ADDITIONAL_CASTE:"))
                {
                    if(CurrentSelectedCastes.Count == 0) throw new TokenParseException("Creature","No cast defined for additional operation!");
                    var name = RawFile.StripTokenEnding(split[1]);
                    if (!CasteList.ContainsKey(name)) throw new TokenParseException("Creature", "Bad caste name " + RawFile.StripTokenEnding(split[1]) + "!");
                    CurrentSelectedCastes.Add(name);
                }
                else if (tokenList[i].StartsWith("[TRIGGERABLE_GROUP:"))
                {
                    TriggerableGroupMin = RawFile.GetIntFromToken(split[1]);
                    TriggerableGroupMax = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[2]));
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
                        if (CurrentSelectedCastes.Count == 0)
                        {
                            GlobalCasteTokens.Add(tokenList[i]);
                        }
                        else if (CurrentSelectedCastes.Contains("ALL"))
                        {
                            foreach (var caste in CasteList.Values)
                            {
                                caste.ParseToken(tokenList[i], this);
                            }
                        }
                        else
                        {
                            foreach (var name in CurrentSelectedCastes)
                            {
                                if (!CasteList.ContainsKey(name))
                                    throw new Exception("This caste name (" + name + ") shouldn't be here!");
                                CasteList[name].ParseToken(tokenList[i], this);
                            }
                        }
                    }
                }
            } 
        }
    }
}
