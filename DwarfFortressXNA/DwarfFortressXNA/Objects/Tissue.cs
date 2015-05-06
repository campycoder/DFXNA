using System;
using System.Collections.Generic;
using System.Linq;

namespace DwarfFortressXNA.Objects
{
    public enum TissueProperties
    {
        THICKENS_ON_STRENGTH,
        THICKENS_ON_ENERGY_STORAGE,
        ARTERIES,
        SCARS,
        STRUCTURAL,
        CONNECTIVE_TISSUE_ANCHOR,
        SETTABLE,
        SPLINTABLE,
        FUNCTIONAL,
        NERVOUS,
        THOUGHT,
        MUSCULAR,
        SMELL,
        HEAR,
        FLIGHT,
        BREATHE,
        SIGHT,
        CONNECTS,
        MAJOR_ARTERIES,
        COSMETIC,
        STYLEABLE,
        TISSUE_LEAKS
    }

    public enum TissueShape
    {
        LAYER,
        STRANDS,
        FEATHERS,
        SCALES,
        CUSTOM
    }

    public class Tissue
    {
        public string Name;
        public string Plural;
        public string MaterialToken;
        public Material Material;
        public int RelativeThickness;
        public int HealingRate;
        public int Vascular;
        public int PainReceptors;
        public int Insulation;
        public string SubordinateTo;
        public State TissueMatState;
        public TissueShape TissueShape;
        public List<TissueProperties> TissuePropertyList;

        public Tissue(string template)
        {
            TissuePropertyList = new List<TissueProperties>();
            CopyFromTemplate(template);
        }
        public Tissue(List<string> tokenList)
        {
            TissuePropertyList = new List<TissueProperties>();
            for (var i = 0; i < tokenList.Count; i++)
            {
                if (!tokenList[i].StartsWith("[")) continue;
                if (RawFile.NumberOfTokens(tokenList[i]) > 1)
                {
                    var multiple = tokenList[i].Split(new[] {']'}).ToList();
                    multiple.Remove("");
                    for (var j = 0; j < multiple.Count; j++)
                    {
                        multiple[j] = multiple[j] + "]";
                    }
                    tokenList.Remove(tokenList[i]);
                    tokenList.InsertRange(i, multiple);
                }
                if (tokenList[i].StartsWith("[TISSUE_NAME"))
                {
                    Name = tokenList[i].Split(new[] {':'})[1];
                    
                    Plural = RawFile.StripTokenEnding(tokenList[i].Split(new[] {':'})[2]);
                    Plural = (Plural == "STP" ? Name + 's' : (Plural == "NP" ? "" : Plural));
                }
                else if (tokenList[i].StartsWith("[TISSUE_MATERIAL"))
                {
                    MaterialToken = RawFile.StripTokenEnding(tokenList[i].Remove(0,17));
                }
                else if (tokenList[i].StartsWith("[SUBORDINATE_TO_TISSUE"))
                {
                    SubordinateTo = RawFile.StripTokenEnding(tokenList[i].Split(new[] {':'})[1]);
                }
                else if (tokenList[i].StartsWith("[PAIN_RECEPTORS"))
                {
                    PainReceptors = Convert.ToInt32(RawFile.StripTokenEnding(tokenList[i].Split(new[] {':'})[1]));
                }
                else if (tokenList[i].StartsWith("[RELATIVE_THICKNESS"))
                {
                    RelativeThickness = Convert.ToInt32(RawFile.StripTokenEnding(tokenList[i].Split(new[] { ':' })[1]));
                }
                else if (tokenList[i].StartsWith("[HEALING_RATE"))
                {
                    HealingRate = Convert.ToInt32(RawFile.StripTokenEnding(tokenList[i].Split(new[] { ':' })[1]));
                }
                else if (tokenList[i].StartsWith("[VASCULAR"))
                {
                    Vascular = Convert.ToInt32(RawFile.StripTokenEnding(tokenList[i].Split(new[] { ':' })[1]));
                }
                else if (tokenList[i].StartsWith("[INSULATION"))
                {
                    Insulation = Convert.ToInt32(RawFile.StripTokenEnding(tokenList[i].Split(new[] { ':' })[1]));
                }
                else if (tokenList[i].StartsWith("[TISSUE_SHAPE"))
                {
                    TissueShape tissueShape;
                    if (!Enum.TryParse(RawFile.StripTokenEnding(tokenList[i].Split(new[] {':'})[1]), out tissueShape))
                        throw new TokenParseException("Tissue", "Tissue Shape " +
                                            RawFile.StripTokenEnding(tokenList[i].Split(new[] {':'})[1]) + " invalid!");
                    TissueShape = tissueShape;
                }
                else
                {
                    TissueProperties propertyBuffer;
                    if (Enum.TryParse(RawFile.StripTokenEnding(tokenList[i].Replace("[", "")), out propertyBuffer))
                    {
                        TissuePropertyList.Add(propertyBuffer);
                    }
                }
            }
        }

        public void CopyFromTemplate(string template)
        {
            if(!DwarfFortress.TissueManager.TissueTemplateList.ContainsKey(template)) throw new Exception("Invalid Tissue template name " + template + "!");
            var tissueTemplate = DwarfFortress.TissueManager.TissueTemplateList[template];
            Name = tissueTemplate.Name;
            Plural = tissueTemplate.Plural;
            MaterialToken = tissueTemplate.MaterialToken;
            RelativeThickness = tissueTemplate.RelativeThickness;
            HealingRate = tissueTemplate.HealingRate;
            Vascular = tissueTemplate.Vascular;
            PainReceptors = tissueTemplate.PainReceptors;
            Insulation = tissueTemplate.Insulation;
            SubordinateTo = tissueTemplate.SubordinateTo;
            TissueMatState = tissueTemplate.TissueMatState;
            TissueShape = tissueTemplate.TissueShape;
            foreach (var t in tissueTemplate.TissuePropertyList)
            {
                TissuePropertyList.Add(t);
            }
        }
    }
}
