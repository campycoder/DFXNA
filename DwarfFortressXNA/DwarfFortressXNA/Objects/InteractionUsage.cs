using System;
using System.Collections.Generic;

namespace DwarfFortressXNA.Objects
{
    public enum InteractionUsageTargetType
    {
        LINE_OF_SIGHT,
        TOUCHABLE,
        DISTURBER_ONLY,
        SELF_ALLOWED,
        SELF_ONLY
    }

    /// <summary>
    /// Formal class for linking an Interaction to a Creature.
    /// When a creature defines CAN_DO_INTERACTION an instance of this class is placed into 
    /// a List inside the Creature and is modified based upon CDI tokens defined afterward.
    /// </summary>
    public class InteractionUsage
    {
        public string Interaction;
        public Dictionary<string, List<InteractionUsageTargetType>> Targets;
        public Dictionary<string, int> TargetRanges;
        public InteractionEffectLocation LocationHint;
        public InteractionUsageHint UsageHint;
        public string AdventureName;
        public Dictionary<string, int> MaxTargetNumbers;
        public int WaitPeriod;
        public bool Verbal;
        public string VerbalSpeech;
        public bool CanBeMutual;
        public bool FreeAction;
        public Tuple<string, string, string> Verbs;
        public Tuple<string, string> TargetVerbs;
        public string RequiredBodyPart;
        public InteractionBreathAttack BreathAttack;
        public Material BreathMaterial;
        public Dictionary<string, Material> ParentMaterialList; 

        public InteractionUsage(Dictionary<string, Material> parentList)
        {
            Targets = new Dictionary<string, List<InteractionUsageTargetType>>();
            TargetRanges = new Dictionary<string, int>();
            MaxTargetNumbers = new Dictionary<string, int>();
            ParentMaterialList = parentList;
        }

        /// <summary>
        /// Parsing CDI tokens to specify interaction usage.
        /// </summary>
        /// <param name="token">CDI token.</param>
        public void ParseToken(string token)
        {
            var split = token.Split(new[] {':'});
            var type = split[1];
            switch (type)
            {
                case "INTERACTION":
                {
                    Interaction = RawFile.StripTokenEnding(split[2]);
                    break;
                }
                case "TARGET":
                {
                    var targetName = split[2];
                    var targetTypes = new List<InteractionUsageTargetType>();
                    for (var i = 3; i < split.Length; i++)
                    {
                        InteractionUsageTargetType targetType;
                        if (!Enum.TryParse(RawFile.StripTokenEnding(split[i]), out targetType))
                            DwarfFortress.ThrowError("InteractionUsage",
                                "Bad InteractionUsageTargetType " + RawFile.StripTokenEnding(split[i]) + "!");
                        targetTypes.Add(targetType);
                    }
                    Targets.Add(targetName, targetTypes);
                    break;
                }
                case "TARGET_RANGE":
                {
                    var targetName = split[2];
                    var targetRange = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[3]));
                    TargetRanges.Add(targetName, targetRange);
                    break;
                }
                case "LOCATION_HINT":
                {
                    InteractionEffectLocation hint;
                    if (!Enum.TryParse(RawFile.StripTokenEnding(split[2]), out hint))
                        DwarfFortress.ThrowError("InteractionUsage",
                            "Bad InteractionEffectLocationHint " + RawFile.StripTokenEnding(split[2]) + "!");
                    LocationHint = hint;
                    break;
                }
                case "USAGE_HINT":
                {
                    InteractionUsageHint hint;
                    if (!Enum.TryParse(RawFile.StripTokenEnding(split[2]), out hint))
                        DwarfFortress.ThrowError("InteractionUsage",
                            "Bad InteractionUsageHint " + RawFile.StripTokenEnding(split[2]) + "!");
                    UsageHint = hint;
                    break;
                }
                case "ADV_NAME":
                {
                    AdventureName = RawFile.StripTokenEnding(split[2]);
                    break;
                }
                case "MAX_TARGET_NUMBER":
                {
                    var targetName = split[2];
                    var targetNumber = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[3]));
                    MaxTargetNumbers.Add(targetName, targetNumber);
                    break;
                }
                case "WAIT_PERIOD":
                {
                    WaitPeriod = RawFile.GetIntFromToken(RawFile.StripTokenEnding(split[2]));
                    break;
                }
                case "VERBAL":
                {
                    Verbal = true;
                    break;
                }
                case "VERBAL_SPEECH":
                {
                    VerbalSpeech = RawFile.StripTokenEnding(split[2]);
                    break;
                }
                case "CAN_BE_MUTUAL":
                {
                    CanBeMutual = true;
                    break;
                }
                case "FREE_ACTION":
                {
                    FreeAction = true;
                    break;
                }
                case "VERB":
                {
                    Verbs = new Tuple<string, string, string>(split[2], split[3], RawFile.StripTokenEnding(split[4]));
                    break;
                }
                case "TARGET_VERB":
                {
                    TargetVerbs = new Tuple<string, string>(split[2], RawFile.StripTokenEnding(split[3]));
                    break;
                }
                case "BP_REQUIRED":
                {
                    RequiredBodyPart = RawFile.StripTokenEnding(split[2]);
                    break;
                }
                case "FLOW":
                {
                    InteractionBreathAttack breath;
                    if (!Enum.TryParse(RawFile.StripTokenEnding(split[2]), out breath))
                        DwarfFortress.ThrowError("InteractionUsage",
                            "Bad InteractionBreathAttack " + RawFile.StripTokenEnding(split[2]) + "!");
                    BreathAttack = breath;
                    break;
                }
                case "MATERIAL":
                {
                    BreathMaterial = split[2] == "INORGANIC" ? DwarfFortress.MaterialManager.MaterialSearch(split[2], split[3]) : ParentMaterialList[split[3]];
                    InteractionBreathAttack breath;
                    if (!Enum.TryParse(RawFile.StripTokenEnding(split[4]), out breath))
                        DwarfFortress.ThrowError("InteractionUsage",
                            "Bad InteractionBreathAttack " + RawFile.StripTokenEnding(split[4]) + "!");
                    BreathAttack = breath;
                    break;
                }
            }
        }
    }
}
