using System;
using System.Collections.Generic;
using System.Linq;

namespace DwarfFortressXNA.Objects
{
    public enum InteractionSourceType
    {
        NULL,
        REGION,
        SECRET,
        DISTURBANCE,
        DEITY,
        ATTACK,
        INGESTION,
        CREATURE_ACTION,
        UNDERGROUND_SPECIAL
    }

    public enum InteractionRegion
    {
        NULL,
        ANY,
        ANY_TERRAIN,
        NORMAL_ALLOWED,
        EVIL_ALLOWED,
        GOOD_ALLOWED,
        SAVAGE_ALLOWED,
        EVIL_ONLY,
        GOOD_ONLY,
        SAVAGE_ONLY,
        SWAMP,
        DESERT,
        FOREST,
        MOUNTAINS,
        OCEAN,
        LAKE,
        GLACIER,
        TUNDRA,
        GRASSLAND,
        HILLS
    }

    public enum InteractionSecretGoal
    {
        NULL,
        STAY_ALIVE,
        MAINTAIN_ENTITY_STATUS,
        START_A_FAMILY,
        RULE_THE_WORLD,
        CREATE_A_GREAT_WORK_OF_ART,
        CRAFT_A_MASTERWORK,
        BRING_PEACE_TO_THE_WORLD,
        BECOME_A_LEGENDARY_WARRIOR,
        MASTER_A_SKILL,
        FALL_IN_LOVE,
        SEE_THE_GREAT_NATURAL_SITES,
        IMMORTALITY
    }

    public enum InteractionSecretMethod
    {
        NULL,
        SUPERNATURAL_LEARNING_POSSIBLE,
        MUNDANE_RESEARCH_POSSIBLE,
        MUNDANE_TEACHING_POSSIBLE,
        MUNDANE_RECORDING_POSSIBLE
    }

    public enum InteractionUsageHint
    {
        NULL,
        MAJOR_CURSE,
        GREETING,
        CLEAN_SELF,
        CLEAN_FRIEND,
        ATTACK,
        FLEEING,
        NEGATIVE_SOCIAL_RESPONSE,
        TORMENT
    }

    public enum InteractionTargetType
    {
        NULL,
        CORPSE,
        CREATURE,
        MATERIAL,
        LOCATION
    }

    public enum InteractionTargetLocation
    {
        NULL,
        CONTEXT_REGION,
        CONTEXT_CREATURE,
        CONTEXT_CREATURE_OR_LOCATION,  
        CONTEXT_ITEM,
        CONTEXT_BP,
        CONTEXT_LOCATION
    }

    public enum InteractionCreatureRequirement
    {
        NULL,
        FIT_FOR_ANIMATION,
        FIT_FOR_RESURRECTION,
        HAS_BLOOD,
        MORTAL,
        NO_AGING,
        STERILE,
        BLOODSUCKER,
        CAN_LEARN,
        CAN_SPEAK,
        CRAZED,
        EXTRAVISION,
        LIKES_FIGHTING,
        MISCHIEVOUS,
        NO_CONNECTIONS_FOR_MOVEMENT,
        NO_DIZZINESS,
        NO_DRINK,
        NO_EAT,
        NO_FEVERS,
        NO_PHYS_ATT_GAIN,
        NO_PHYS_ATT_RUST,
        NO_SLEEP,
        NO_THOUGHT_CENTER_FOR_MOVEMENT,
        NOBREATHE,
        NOEMOTION,
        NOEXERT,
        NOFEAR,
        NONAUSEA,
        NOPAIN,
        NOSTUN,
        NOT_LIVING,
        NOTHOUGHT,
        OPPOSED_TO_LIFE,
        PARALYZEIMMUNE,
        SUPERNATURAL,
        TRANCES,
        UTTERANCES
    }

    public enum InteractionBreathAttack
    {
        NULL,
        TRAILING_DUST_FLOW,
        TRAILING_VAPOR_FLOW,
        TRAILING_GAS_FLOW,
        TRAILING_ITEM_FLOW, //TODO: Item token upon Item implementation!
        SOLID_GLOB,
        LIQUID_GLOB,
        SPATTER_POWDER,
        SPATTER_LIQUID,
        UNDIRECTED_GAS,
        UNDIRECTED_VAPOR,
        UNDIRECTED_DUST,
        UNDIRECTED_ITEM_CLOUD, //TODO: See Above
        WEB_SPRAY,
        DRAGONFIRE,
        FIREJET,
        FIREBALL,
        WEATHER_CREEPING_GAS,
        WEATHER_CREEPING_VAPOR,
        WEATHER_CREEPING_DUST,
        WEATHER_FALLING_MATERIAL
    }

    public enum InteractionEffectType
    {
        NULL,
        ANIMATE,
        ADD_SYNDROME,
        RESURRECT,
        CLEAN,
        CONTACT,
        MATERIAL_EMISSION,
        HIDE
    }

    public enum InteractionEffectFrequency
    {
        NULL,
        WEEKLY,
        MONTHLY
    }

    public enum InteractionEffectLocation
    {
        NULL,
        IN_WATER,
        IN_MAGMA,
        NO_WATER,
        NO_MAGMA
    }

    public class InteractionSource
    {
        public InteractionSourceType Type;
        public string HistoryStringOne;
        public string HistoryStringTwo;
        public int Frequency;
        public string Name;
        public List<InteractionRegion> RegionList;
        public Spheres SecretSphere;
        public InteractionSecretGoal SecretGoal;
        public InteractionSecretMethod SecretMethod;
        public InteractionUsageHint DeityUsageHint;

        public InteractionSource(InteractionSourceType type)
        {
            Type = type;
            RegionList = new List<InteractionRegion>();
        }

        public void ParseToken(string token)
        {
            var split = token.Split(new[] { ':' });
            if (token.StartsWith("[IS_HIST_STRING_1:")) 
                HistoryStringOne = RawFile.StripTokenEnding(split[1]);
            else if (token.StartsWith("[IS_HIST_STRING_2:")) 
                HistoryStringTwo = RawFile.StripTokenEnding(split[1]);
            else if (token.StartsWith("[IS_FREQUENCY:"))
                Frequency = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[1]));
            else if (token.StartsWith("[IS_NAME:"))
                Name = RawFile.StripTokenEnding(split[1]);
            else if (token.StartsWith("[IS_REGION:"))
            {
                if(Type != InteractionSourceType.REGION) throw new TokenParseException("InteractionSource", "IS_REGION can't be parsed in a " + Type + "InteractionSource!");
                InteractionRegion region;
                if(!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out region)) throw new TokenParseException("InteractionSource", "Bad InteractionRegion " + RawFile.StripTokenEnding(split[1]) + "!");
                RegionList.Add(region);
            }
            else if(token.StartsWith("[IS_SPHERE:"))
            {
                if(Type != InteractionSourceType.SECRET) throw new TokenParseException("InteractionSource", "IS_SPHERE can't be parsed in a " + Type + " InteractionSource!");
                Spheres sphere;
                if(!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out sphere)) throw new TokenParseException("InteractionSource", "Bad Sphere " + RawFile.StripTokenEnding(split[1]) + "!");
                SecretSphere = sphere;
            }
            else if (token.StartsWith("[IS_SECRET_GOAL:"))
            {
                if (Type != InteractionSourceType.SECRET) throw new TokenParseException("InteractionSource", "IS_SECRET_GOAL can't be parsed in a " + Type + " InteractionSource!");
                InteractionSecretGoal goal;
                if(!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out goal)) throw new TokenParseException("InteractionSource", "Bad InteractionSecretGoal " + RawFile.StripTokenEnding(split[1]) + "!");
                SecretGoal = goal;
            }
            else if (token.StartsWith("[IS_SECRET:"))
            {
                if (Type != InteractionSourceType.SECRET) throw new TokenParseException("InteractionSource", "IS_SECRET can't be parsed in a " + Type + " InteractionSource!");
                InteractionSecretMethod method;
                if(!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out method)) throw new TokenParseException("InteractionSource", "Bad InteractionSecretMethod " + RawFile.StripTokenEnding(split[1]) + "!");
                SecretMethod = method;
            }
            else if (token.StartsWith("[IS_USAGE_HINT:"))
            {
                if (Type != InteractionSourceType.DEITY) throw new TokenParseException("InteractionSource", "IS_USAGE_HINT can't be parsed in a " + Type + " InteractionSource!");
                InteractionUsageHint hint;
                if (!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out hint)) throw new TokenParseException("InteractionSource", "Bad InteractionUsageHint " + RawFile.StripTokenEnding(split[1]) + "!");
                DeityUsageHint = hint;
            }
            else throw new TokenParseException("InteractionSource", "Bad token " + split[1].Remove(0,1));
        }
    }

    public class InteractionTarget
    {
        public InteractionTargetType Type;
        public InteractionTargetLocation Location;
        public string ManualInput;
        public Tuple<string, string> AffectedCreature;
        public string AffectedClass;
        public Tuple<string, string> ImmuneCreature;
        public string ImmuneClass;
        public InteractionCreatureRequirement CreatureRequirement;
        public InteractionCreatureRequirement CreatureForbidden;
        public bool CannotTargetIfAlreadyAffected;
        public string CannotHaveSyndromeClass;
        public InteractionBreathAttack BreathAttack;
        public Material Material;
        public bool UseContextMaterial;

        public InteractionTarget(InteractionTargetType type)
        {
            Type = type;
        }

        public void ParseToken(string token)
        {
            var split = token.Split(new[] { ':' });
            if (token.StartsWith("[IT_LOCATION:"))
            {
                InteractionTargetLocation location;
                if(!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out location)) throw new TokenParseException("InteractionTarget", "Bad InteractionTargetLocation " + RawFile.StripTokenEnding(split[1]));
                Location = location;
            }
            else if (token.StartsWith("[IT_MANUAL_INPUT:"))
            {
                ManualInput = RawFile.StripTokenEnding(split[1]);
            }
            else if (token.StartsWith("[IT_AFFECTED_CREATURE:"))
            {
                if(Type != InteractionTargetType.CREATURE && Type != InteractionTargetType.CORPSE) throw new TokenParseException("InteractionTarget", "IT_AFFECTED_CREATURE can't be parsed in a " + Type + " InteractionTarget!");
                AffectedCreature = new Tuple<string, string>(split[1], RawFile.StripTokenEnding(split[2]));
            }
            else if (token.StartsWith("[IT_AFFECTED_CLASS:"))
            {
                if (Type != InteractionTargetType.CREATURE && Type != InteractionTargetType.CORPSE) throw new TokenParseException("InteractionTarget", "IT_AFFECTED_CLASS can't be parsed in a " + Type + " InteractionTarget!");
                AffectedClass = RawFile.StripTokenEnding(split[1]);
            }
            else if (token.StartsWith("[IT_IMMUNE_CREATURE:"))
            {
                if (Type != InteractionTargetType.CREATURE && Type != InteractionTargetType.CORPSE) throw new TokenParseException("InteractionTarget", "IT_IMMUNE_CREATURE can't be parsed in a " + Type + " InteractionTarget!");
                ImmuneCreature = new Tuple<string, string>(split[1], RawFile.StripTokenEnding(split[2]));
            }
            else if (token.StartsWith("[IT_IMMUNE_CLASS:"))
            {
                if (Type != InteractionTargetType.CREATURE && Type != InteractionTargetType.CORPSE) throw new TokenParseException("InteractionTarget", "IT_IMMUNE_CLASS can't be parsed in a " + Type + " InteractionTarget!");
                ImmuneClass = RawFile.StripTokenEnding(split[1]);
            }
            else if (token.StartsWith("[IT_REQUIRES:"))
            {
                InteractionCreatureRequirement requirement;
                if(!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out requirement)) throw new TokenParseException("InteractionTarget", "Bad InteractionCreatureRequirement " + RawFile.StripTokenEnding(split[1]) + "!");
                CreatureRequirement = requirement;
            }
            else if (token.StartsWith("[IT_FORBIDDEN:"))
            {
                InteractionCreatureRequirement forbidden;
                if (!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out forbidden)) throw new TokenParseException("InteractionTarget", "Bad InteractionCreatureRequirement " + RawFile.StripTokenEnding(split[1]) + "!");
                CreatureForbidden = forbidden;
            }
            else if (token == "[IT_CANNOT_TARGET_IF_ALREADY_AFFECTED]")
            {
                CannotTargetIfAlreadyAffected = true;
            }
            else if (token.StartsWith("[IT_CANNOT_HAVE_SYNDROME_CLASS:"))
            {
                //TODO: Fix on syndrome implementation
                CannotHaveSyndromeClass = RawFile.StripTokenEnding(split[1]);
            }
            else if (token.StartsWith("[IT_MATERIAL"))
            {
                switch (RawFile.StripTokenEnding(split[1]))
                {
                    case "FLOW":
                    {
                        InteractionBreathAttack breathAttack;
                        if (!Enum.TryParse(RawFile.StripTokenEnding(split[2]), out breathAttack))
                            throw new TokenParseException("InteractionTarget",
                                "Bad InteractionBreathAttack " + RawFile.StripTokenEnding(split[2]) + "!");
                        BreathAttack = breathAttack;
                        break;
                    } 
                    case "MATERIAL":
                    {
                        Material = DwarfFortress.MaterialManager.MaterialSearch(split[1], split[2]);
                        InteractionBreathAttack breathAttackType;
                        if (!Enum.TryParse(RawFile.StripTokenEnding(split[3]), out breathAttackType))
                            throw new TokenParseException("InteractionTarget",
                                "Bad InteractionBreathAttack " + RawFile.StripTokenEnding(split[3]) + "!");
                        BreathAttack = breathAttackType;
                        break;
                    }
                    case "CONTEXT_MATERIAL":
                    {
                        UseContextMaterial = true;
                        break;
                    }
                }
            }
        }
    }

    public class InteractionEffect
    {
        public InteractionEffectType Type;
        public List<string> Targets;
        public InteractionEffectFrequency Frequency;
        public bool Immediate;
        public InteractionEffectLocation Location;
        public string ArenaName;
        public int GrimeLevel;
        public string SyndromeTag;

        public InteractionEffect(InteractionEffectType type)
        {
            Type = type;
            Targets = new List<string>();
        }

        public void ParseToken(string token)
        {
            var split = token.Split(new[] { ':' });
            if (token.StartsWith("[IE_TARGET:"))
            {
                Targets.Add(RawFile.StripTokenEnding(split[1]));
            }
            else if (token.StartsWith("[IE_INTERMITTENT:"))
            {
                InteractionEffectFrequency freq;
                if (!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out freq))
                    throw new TokenParseException("InteractionEffect",
                        "Bad InteractionEffectFrequency " + RawFile.StripTokenEnding(split[1]) + "!");
                Frequency = freq;
            }
            else if (token == "[IE_IMMEDIATE]")
            {
                Immediate = true;
            }
            else if (token.StartsWith("[IE_LOCATION:"))
            {
                InteractionEffectLocation loc;
                if (!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out loc))
                    throw new TokenParseException("InteractionEffect",
                        "Bad InteractionEffectLocation " + RawFile.StripTokenEnding(split[1]) + "!");
                Location = loc;
            }
            else if (token.StartsWith("[IE_ARENA_NAME:"))
            {
                ArenaName = RawFile.StripTokenEnding(split[1]);
            }
            else if (token.StartsWith("[IE_GRIME_LEVEL:"))
            {
                GrimeLevel = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[1]));
            }
            else if (token.StartsWith("[IE_SYNDROME_TAG:"))
            {
                //TODO:Fix on Syndrome implementation
                SyndromeTag = RawFile.StripTokenEnding(split[1]);
            }
        }
    }

    public class Interaction
    {
        public List<InteractionSource> InteractionSources;
        public Dictionary<string, InteractionTarget> InteractionTargets;
        public List<InteractionEffect> InteractionEffects; 
        public string SelectedTarget;
        public bool Generated;

        public Interaction(List<string> tokenList)
        {
            InteractionSources = new List<InteractionSource>();
            InteractionTargets = new Dictionary<string, InteractionTarget>();
            InteractionEffects = new List<InteractionEffect>();
            for (var i = 0; i < tokenList.Count; i++)
            {
                var split = tokenList[i].Split(new[] { ':' });
                if (!tokenList[i].StartsWith("[")) continue;
                if (RawFile.NumberOfTokens(tokenList[i]) > 1)
                {
                    var multiple = tokenList[i].Split(new[] { ']' }).ToList();
                    multiple.Remove("");
                    for (var j = 0; j < multiple.Count; j++)
                    {
                        multiple[j] = multiple[j] + "]";
                    }
                    tokenList.Remove(tokenList[i]);
                    tokenList.InsertRange(i, multiple);
                }
                if (tokenList[i].StartsWith("[I_SOURCE:"))
                {
                    InteractionSourceType type;
                    if (!Enum.TryParse(RawFile.StripTokenEnding(split[1]), out type)) throw new TokenParseException("Interaction", "Bad InteractionSourceType " + RawFile.StripTokenEnding(split[1]) + "!");
                    InteractionSources.Add(new InteractionSource(type));
                }
                else if (tokenList[i].StartsWith("[IS_HIST_STRING_1:") || tokenList[i].StartsWith("[IS_HIST_STRING_2:") ||
                         tokenList[i].StartsWith("[IS_FREQUENCY:") || tokenList[i].StartsWith("[IS_NAME:") ||
                         tokenList[i].StartsWith("[IS_REGION:") || tokenList[i].StartsWith("[IS_SPHERE:") ||
                         tokenList[i].StartsWith("[IS_SECRET_GOAL:") || tokenList[i].StartsWith("[IS_SECRET:") ||
                         tokenList[i].StartsWith("[IS_USAGE_HINT"))
                {
                    InteractionSources.Last().ParseToken(tokenList[i]);
                }
                else if (tokenList[i].StartsWith("[I_TARGET:"))
                {
                    InteractionTargetType type;
                    var name = split[1];
                    if (!Enum.TryParse(RawFile.StripTokenEnding(split[2]), out type)) throw new TokenParseException("Interaction", "Bad InteractionTargetType " + RawFile.StripTokenEnding(split[2]) + "!");
                    InteractionTargets.Add(name, new InteractionTarget(type));
                    SelectedTarget = name;
                }
                else if (tokenList[i].StartsWith("[IT_LOCATION:") || tokenList[i].StartsWith("[IT_MANUAL_INPUT:") ||
                         tokenList[i].StartsWith("[IT_AFFECTED_CREATURE:") ||
                         tokenList[i].StartsWith("[IT_AFFECTED_CLASS:") ||
                         tokenList[i].StartsWith("[IT_IMMUNE_CREATURE:") || tokenList[i].StartsWith("[IT_IMMUNE_CLASS:") ||
                         tokenList[i].StartsWith("[IT_REQUIRES:") || tokenList[i].StartsWith("[IT_FORBIDDEN:") ||
                         tokenList[i].StartsWith("[IT_CANNOT_TARGET_IF_ALREADY_AFFECTED") ||
                         tokenList[i].StartsWith("[IT_CANNOT_HAVE_SYNDROME_CLASS:") ||
                         tokenList[i].StartsWith("[IT_MATERIAL:"))
                {
                    InteractionTargets[SelectedTarget].ParseToken(tokenList[i]);
                }
                else if (tokenList[i].StartsWith("[I_EFFECT:"))
                {
                    InteractionEffectType type;
                    var name = RawFile.StripTokenEnding(split[1]);
                    if (!Enum.TryParse(name, out type)) throw new TokenParseException("Interaction", "Bad InteractionEffectType " + name + "!");
                    InteractionEffects.Add(new InteractionEffect(type));
                }
                else if (tokenList[i].StartsWith("[IE_TARGET:") || tokenList[i].StartsWith("[IE_INTERMITTENT:") ||
                         tokenList[i].StartsWith("[IE_IMMEDIATE:") || tokenList[i].StartsWith("[IE_LOCATION:") ||
                         tokenList[i].StartsWith("[IE_ARENA_NAME:") || tokenList[i].StartsWith("[IE_GRIME_LEVEL:") ||
                         tokenList[i].StartsWith("[IE_SYNDROME_TAG:"))
                {
                    InteractionEffects[InteractionEffects.Count - 1].ParseToken(tokenList[i]);
                }
            }
        }
    }
}
