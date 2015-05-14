using System;
using System.Collections.Generic;
using System.Linq;

namespace DwarfFortressXNA.Objects
{
    public enum BodyPartProperties
    {
        APERTURE,
        BREATHE,
        CIRCULATION,
        CONNECTOR,
        DIGIT,
        EMBEDDED,
        FLIER,
        GRASP,
        GUTS,
        HEAD,
        HEAR,
        INTERNAL,
        JOINT,
        LIMB,
        LOWERBODY,
        LEFT,
        MOUTH,
        NERVOUS,
        RIGHT,
        SKELETON,
        STANCE,
        SIGHT,
        SMELL,
        SMALL,
        SOCKET,
        THOUGHT,
        TOTEMABLE,
        UPPERBODY,
        UNDER_PRESSURE,
        VERMIN_BUTCHER_ITEM
    }
    public class BodyPart
    {
        public List<BodyPartProperties> BodyPartProprtiesList;
        public string Name;
        public string Plural;
        public string Category;
        public string Connection;
        public BodyPartProperties ConnectionType;
        public string ConnectionCategory;
        public int DefaultRelsize = 0;
        public int Number = 1;
        public List<string> IndividualNames;
        public List<string> IndividualPlurals; 
        public BodyPart(List<String> tokenList)
        {
            BodyPartProprtiesList = new List<BodyPartProperties>();
            var currentName = 0;
            for (var i = 0; i < tokenList.Count; i++)
            {
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
                if (tokenList[i].StartsWith("[BP"))
                {
                    var split = tokenList[i].Split(new[] {':'});
                    Name = split[2];
                    Plural = RawFile.StripTokenEnding(split[3]);
                    Plural = Plural == "NP" ? "" : Plural == "STP" ? Name + "s" : Plural;
                }
                else if (tokenList[i].StartsWith("[CATEGORY"))
                {
                    Category = RawFile.StripTokenEnding(tokenList[i].Split(new[] {':'})[1]);
                }
                else if (tokenList[i].StartsWith("[CON:"))
                {
                    Connection = RawFile.StripTokenEnding(tokenList[i].Remove(0, 5));
                }
                else if (tokenList[i].StartsWith("[CON_CAT:"))
                {
                    ConnectionCategory = RawFile.StripTokenEnding(tokenList[i].Remove(0, 9));
                }
                else
                {
                    BodyPartProperties propertyBuffer;
                    if (tokenList[i].StartsWith("[CONTYPE:"))
                    {
                        if (!Enum.TryParse(RawFile.StripTokenEnding(tokenList[i].Remove(0, 9)), out propertyBuffer)) throw new TokenParseException("Body Part", "Invalid connection type " + RawFile.StripTokenEnding(tokenList[i].Remove(0, 9)) + "!");
                        if (propertyBuffer != BodyPartProperties.UPPERBODY && propertyBuffer != BodyPartProperties.LOWERBODY && propertyBuffer != BodyPartProperties.HEAD && propertyBuffer != BodyPartProperties.GRASP && propertyBuffer != BodyPartProperties.STANCE) throw new TokenParseException("Body Part", "Bad connection type " + propertyBuffer + "!");
                        ConnectionType = propertyBuffer;
                    }
                    else if (tokenList[i].StartsWith("[DEFAULT_RELSIZE"))
                    {
                        DefaultRelsize = Convert.ToInt32(RawFile.StripTokenEnding(tokenList[i].Split(new[] {':'})[1]));
                    }
                    else if (tokenList[i].StartsWith("[NUMBER"))
                    {
                        Number = Convert.ToInt32(RawFile.StripTokenEnding(tokenList[i].Split(new[] { ':' })[1]));
                    }
                    else if (tokenList[i].StartsWith("[INDIVIDUAL_NAME"))
                    {
                        if (currentName == Number) throw new TokenParseException("Body Part", "Too many names defined! Only " + Number + " body parts!");
                        if (IndividualNames == null)
                        {
                            IndividualNames = new List<string>();
                            IndividualPlurals = new List<string>();
                        }
                        var name = tokenList[i].Split(new[] {':'})[1];
                        var plural = RawFile.StripTokenEnding(tokenList[i].Split(new[] { ':' })[2]);
                        plural = plural == "NP" ? "" : plural == "STP" ? name + "s" : plural;
                        IndividualNames.Add(name);
                        IndividualPlurals.Add(plural);
                        currentName++;
                    }
                    else if (Enum.TryParse(RawFile.StripTokenEnding(tokenList[i].Replace("[", "")), out propertyBuffer))
                    {
                        BodyPartProprtiesList.Add(propertyBuffer);
                    }
                }
            }
        }
    }
}
