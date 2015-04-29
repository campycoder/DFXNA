using System;
using System.Collections.Generic;
using System.Linq;

namespace DwarfFortressXNA
{
    public enum State
    {
        SOLID,
        LIQUID,
        GAS,
        POWDER,
        SOLID_POWDER,
        PASTE,
        SOLID_PASTE,
        PRESSED,
        SOLID_PRESSED,
        ALL_SOLID,
        ALL
    }

    public enum ItemType
    {
        METAL,
        BARRED,
        SCALED,
        LEATHER,
        SOFT,
        HARD,
        WEAPON,
        WEAPON_RANGED,
        ANVIL,
        AMMO,
        DIGGER,
        ARMOR,
        DELICATE,
        SIEGE_ENGINE,
        QUERN
    }

    public enum MaterialType
    {
        STONE,
        GEM,
        METAL,
        GLASS,
        NULL
    }

    public enum Environment
    {
        ALL_STONE,
        IGNEOUS_ALL,
        IGNEOUS_INTRUSIVE,
        IGNEOUS_EXTRUSIVE,
        SOIL,
        SOIL_OCEAN,
        SOIL_SAND,
        METAMORPHIC,
        SEDIMENTARY,
        ALLUVIAL
    }

    public enum InclusionType
    {
        CLUSTER,
        CLUSTER_SMALL,
        CLUSTER_ONE,
        VEIN
    }

    public class StateDescription
    {
        public string Name;
        public string Adj;
        public string Plural;
        public string ColorDescriptor;
        public Dictionary<Environment, Dictionary<InclusionType, int>> Environment;
        public Dictionary<string, Dictionary<InclusionType, int>> EnvironmentSpec;
        public StateDescription()
        {

        }

        public StateDescription(StateDescription copy)
        {
            Name = copy.Name;
            Adj = copy.Adj;
            Plural = copy.Plural;
            ColorDescriptor = copy.ColorDescriptor;
        }
    }
    public class Material
    {
        public Dictionary<State, StateDescription> StateList;
        public List<ItemType> CanBeMade;
        public MaterialType Type = MaterialType.NULL;
        public ColorPair DisplayColor;
        public char Tile = '█';
        public char ItemSymbol = '•';

        public Dictionary<string, int> IntProperties;

        public Material(List<string> tokenList)
        {
            StateList = new Dictionary<State, StateDescription>();
            IntProperties = new Dictionary<string, int>();
            CanBeMade = new List<ItemType>();
            InitDefaults();
            for(var i =0;i < tokenList.Count;i++)
            {
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
                    Console.WriteLine("END");
                    
                }
                if(tokenList[i].StartsWith("[USE_MATERIAL_TEMPLATE"))
                {
                    CopyFromTemplate(RawFile.StripTokenEnding(tokenList[i].Split(new[] { ':' })[1]));
                }
                else if(tokenList[i].StartsWith("[STATE_COLOR"))
                {
                    State state;
                    if(!Enum.TryParse(tokenList[i].Split(new[] {':'})[1], out state)) throw new Exception("Bad state name " + tokenList[i].Split(new[] {':'})[1] + "!");
                    var color = RawFile.StripTokenEnding(tokenList[i].Split(new[] {':'})[2]);
                    if(!StateList.ContainsKey(state)) StateList.Add(state, new StateDescription());
                    StateList[state].ColorDescriptor = color;
                }
                else if(tokenList[i].StartsWith("[STATE_NAME"))
                {
                    State state;
                    if(!Enum.TryParse(tokenList[i].Split(new[] {':'})[1], out state)) throw new Exception("Bad state name " + tokenList[i].Split(new[] {':'})[1] + "!");
                    var name = RawFile.StripTokenEnding(tokenList[i].Split(new[] {':'})[2]);
                    if(!StateList.ContainsKey(state)) StateList.Add(state, new StateDescription());
                    StateList[state].Name = name;
                }
                else if(tokenList[i].StartsWith("[STATE_ADJ"))
                {
                    State state;
                    if(!Enum.TryParse(tokenList[i].Split(new[] {':'})[1], out state)) throw new Exception("Bad state name " + tokenList[i].Split(new[] {':'})[1] + "!");
                    var adj = RawFile.StripTokenEnding(tokenList[i].Split(new[] {':'})[2]);
                    if(!StateList.ContainsKey(state)) StateList.Add(state, new StateDescription());
                    StateList[state].Adj = adj;
                }
                else if(tokenList[i].StartsWith("[DISPLAY_COLOR"))
                {
                    var colorSplit = tokenList[i].Split(new[] {':'});
                    var fg = Convert.ToInt32(colorSplit[1]);
                    var bg = Convert.ToInt32(colorSplit[2]);
                    var bt = Convert.ToInt32(colorSplit[3].Replace("]", ""));
                    if((fg < 0 || fg > 7) || (bg < 0 || bg > 7) || (bt < 0 || bt > 1)) throw new Exception("Bad color specification with " + tokenList[i] + "!");
                    DisplayColor = DwarfFortressMono.FontManager.DfColor.GetPairFromTriad(fg, bg, bt);
                }
                else if(tokenList[i].StartsWith("[TILE"))
                {
                    var strippedChar = RawFile.StripTokenEnding(tokenList[i].Split(new[] {':'})[1]);
                    if(strippedChar.StartsWith("'"))
                    {
                        strippedChar = strippedChar.Replace("'","");
                        Tile = strippedChar[0];
                    }
                    else Tile = DwarfFortressMono.FontManager.Codepage[GetIntFromToken(strippedChar)];
                }
                else if(tokenList[i].StartsWith("[IS_GEM"))
                {
                    var gemSplit = tokenList[i].Split(new[] { ':' });
                    var state = State.SOLID;
                    //This is currently useless, figuring nothing else (AFAIK) writes to IS_GEM
                    if (RawFile.StripTokenEnding(gemSplit[3]) == "OVERWRITE_SOLID") state = State.ALL_SOLID;
                    StateList[state].Name = gemSplit[1];
                    StateList[state].Adj = gemSplit[1];
                    StateList[state].Plural = gemSplit[2] == "STP" ? gemSplit[1] + "s" : gemSplit[2];
                    ItemSymbol = '☼';
                    Type = MaterialType.GEM;
                }
                else if(tokenList[i].StartsWith("[ITEM_SYMBOL"))
                {
                    var strippedChar = RawFile.StripTokenEnding(tokenList[i].Split(new[] { ':' })[1]);
                    if (strippedChar.StartsWith("'"))
                    {
                        strippedChar = strippedChar.Replace("'", "");
                        ItemSymbol = strippedChar[0];
                    }
                    else ItemSymbol = DwarfFortressMono.FontManager.Codepage[GetIntFromToken(strippedChar)];
                }
                else if(tokenList[i].StartsWith("[IS_"))
                {
                    var type = RawFile.StripTokenEnding(tokenList[i].Replace("[IS_", ""));
                    if (!Enum.TryParse(type, out Type)) throw new Exception("Bad MaterialType " + type + "!");
                }
                else if(tokenList[i].StartsWith("[ITEMS_"))
                {
                    var item = RawFile.StripTokenEnding(tokenList[i].Replace("[ITEMS_", ""));
                    ItemType itemType;
                    if (!Enum.TryParse(item, out itemType)) throw new Exception("Bad ItemType " + Type + "!");
                    CanBeMade.Add(itemType);
                }
                else if(IntProperties.ContainsKey(tokenList[i].Split(new[] {':'})[0].Replace("[","")))
                {
                    IntProperties[tokenList[i].Split(new[] { ':' })[0].Replace("[", "")] = GetIntFromToken(RawFile.StripTokenEnding(tokenList[i].Split(new[] { ':' })[1]));
                }
            }
        }

        public void InitDefaults()
        {
            IntProperties.Add("MATERIAL_VALUE", 1);
            IntProperties.Add("SPEC_HEAT", 0);
            IntProperties.Add("IGNITE_POINT", 0);
            IntProperties.Add("MELTING_POINT", 0);
            IntProperties.Add("BOILING_POINT", 0);
            IntProperties.Add("HEATDAM_POINT", 0);
            IntProperties.Add("COLDDAM_POINT", 0);
            IntProperties.Add("MAT_FIXED_TEMP", 0);
            IntProperties.Add("SOLID_DENSITY", 0);
            IntProperties.Add("LIQUIDD_ENSITY", 0);
            IntProperties.Add("MOLAR_MASS", 0);
            IntProperties.Add("IMPACT_YIELD", 10000);
            IntProperties.Add("IMPACTF_RACTURE", 10000);
            IntProperties.Add("IMPACT_STRAIN_AT_YIELD", 0);
            IntProperties.Add("COMPRESSIVE_YIELD", 10000);
            IntProperties.Add("COMPRESSIVEF_RACTURE", 10000);
            IntProperties.Add("COMPRESSIVE_STRAIN_AT_YIELD", 0);
            IntProperties.Add("TENSILE_YIELD", 10000);
            IntProperties.Add("TENSILE_FRACTUE", 10000);
            IntProperties.Add("TENSILE_STRAIN_ATY_IELD", 0);
            IntProperties.Add("TORSION_YIELD", 10000);
            IntProperties.Add("TORSION_FRACTURE", 10000);
            IntProperties.Add("TORSION_STRAIN_AT_YIELD", 0);
            IntProperties.Add("SHEAR_YIELD", 10000);
            IntProperties.Add("SHEAR_FRACTURE", 10000);
            IntProperties.Add("SHEAR_STRAIN_AT_YIELD", 0);
            IntProperties.Add("BENDING_YIELD", 10000);
            IntProperties.Add("BENDING_FRACTURE", 10000);
            IntProperties.Add("BENDING_STRAIN_AT_YIELD", 0);
            IntProperties.Add("MAX_EDGE", 10000);
            IntProperties.Add("ABSORPTION", 0);
        }

        public int GetIntFromToken(string number)
        {
            if (number == "NONE") return 0;
            return Convert.ToInt32(number);
        }

        public ColorPair GetItemDisplayColor()
        {
            return new ColorPair(DisplayColor.Foreground, ColorManager.Black);
        }

        public void CopyFromTemplate(string template)
        {
            if(!DwarfFortressMono.MaterialManager.MaterialTemplateList.ContainsKey(template)) throw new Exception("Bad material template requested: " + template);
            var tempMaterial = DwarfFortressMono.MaterialManager.MaterialTemplateList[template];
            foreach(var pair in tempMaterial.StateList)
            {
                if (!StateList.ContainsKey(pair.Key)) StateList.Add(pair.Key, new StateDescription(pair.Value));
                else StateList[pair.Key] = new StateDescription(pair.Value);
            }
            foreach(var pair in tempMaterial.IntProperties)
            {
                if (!IntProperties.ContainsKey(pair.Key)) IntProperties.Add(pair.Key, pair.Value);
                else IntProperties[pair.Key] = pair.Value;
            }
            foreach(var itemType in tempMaterial.CanBeMade)
            {
                CanBeMade.Add(itemType);
            }
            Type = tempMaterial.Type;
            Tile = tempMaterial.Tile;
            DisplayColor = tempMaterial.DisplayColor;
        }
    }
}
