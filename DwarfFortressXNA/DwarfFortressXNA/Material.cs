using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public string name;
        public string adj;
        public string plural;
        public string color_descriptor;
        public Dictionary<Environment, Dictionary<InclusionType, int>> environment;
        public Dictionary<string, Dictionary<InclusionType, int>> environmentSpec;
        public StateDescription()
        {

        }

        public StateDescription(StateDescription copy)
        {
            this.name = copy.name;
            this.adj = copy.adj;
            this.plural = copy.plural;
            this.color_descriptor = copy.color_descriptor;
        }
    }
    public class Material
    {
        public Dictionary<State, StateDescription> stateList;
        public List<ItemType> canBeMade;
        public MaterialType type = MaterialType.NULL;
        public ColorPair displayColor;
        public char tile = '█';
        public char itemSymbol = '•';

        public Dictionary<string, int> intProperties;

        public Material(List<string> tokenList)
        {
            this.stateList = new Dictionary<State, StateDescription>();
            this.intProperties = new Dictionary<string, int>();
            this.canBeMade = new List<ItemType>();
            InitDefaults();
            for(int i =0;i < tokenList.Count;i++)
            {
                if(!tokenList[i].StartsWith("[")) continue;
                if(RawFile.NumberOfTokens(tokenList[i]) > 1)
                {
                    List<string> multiple = tokenList[i].Split(new char[] { ']' }).ToList<string>();
                    multiple.Remove("");
                    for(int j = 0;j < multiple.Count; j++)
                    {
                        multiple[j] = multiple[j] + "]";
                    }
                    tokenList.Remove(tokenList[i]);
                    tokenList.InsertRange(i, multiple);
                    Console.WriteLine("END");
                    
                }
                if(tokenList[i].StartsWith("[USE_MATERIAL_TEMPLATE"))
                {
                    CopyFromTemplate(RawFile.StripTokenEnding(tokenList[i].Split(new char[] { ':' })[1]));
                }
                else if(tokenList[i].StartsWith("[STATE_COLOR"))
                {
                    State state;
                    if(!Enum.TryParse<State>(tokenList[i].Split(new char[] {':'})[1], out state)) throw new Exception("Bad state name " + tokenList[i].Split(new char[] {':'})[1] + "!");
                    string color = RawFile.StripTokenEnding(tokenList[i].Split(new char[] {':'})[2]);
                    if(!this.stateList.ContainsKey(state)) this.stateList.Add(state, new StateDescription());
                    this.stateList[state].color_descriptor = color;
                }
                else if(tokenList[i].StartsWith("[STATE_NAME"))
                {
                    State state;
                    if(!Enum.TryParse<State>(tokenList[i].Split(new char[] {':'})[1], out state)) throw new Exception("Bad state name " + tokenList[i].Split(new char[] {':'})[1] + "!");
                    string name = RawFile.StripTokenEnding(tokenList[i].Split(new char[] {':'})[2]);
                    if(!this.stateList.ContainsKey(state)) this.stateList.Add(state, new StateDescription());
                    this.stateList[state].name = name;
                }
                else if(tokenList[i].StartsWith("[STATE_ADJ"))
                {
                    State state;
                    if(!Enum.TryParse<State>(tokenList[i].Split(new char[] {':'})[1], out state)) throw new Exception("Bad state name " + tokenList[i].Split(new char[] {':'})[1] + "!");
                    string adj = RawFile.StripTokenEnding(tokenList[i].Split(new char[] {':'})[2]);
                    if(!this.stateList.ContainsKey(state)) this.stateList.Add(state, new StateDescription());
                    this.stateList[state].adj = adj;
                }
                else if(tokenList[i].StartsWith("[DISPLAY_COLOR"))
                {
                    string[] colorSplit = tokenList[i].Split(new char[] {':'});
                    int fg = Convert.ToInt32(colorSplit[1]);
                    int bg = Convert.ToInt32(colorSplit[2]);
                    int bt = Convert.ToInt32(colorSplit[3].Replace("]", ""));
                    if((fg < 0 || fg > 7) || (bg < 0 || bg > 7) || (bt < 0 || bt > 1)) throw new Exception("Bad color specification with " + tokenList[i] + "!");
                    this.displayColor = DwarfFortressMono.fontManager.dfColor.GetPairFromTriad(fg, bg, bt);
                }
                else if(tokenList[i].StartsWith("[TILE"))
                {
                    string strippedChar = RawFile.StripTokenEnding(tokenList[i].Split(new char[] {':'})[1]);
                    if(strippedChar.StartsWith("'"))
                    {
                        strippedChar = strippedChar.Replace("'","");
                        tile = strippedChar[0];
                    }
                    else tile = DwarfFortressMono.fontManager.codepage[GetIntFromToken(strippedChar)];
                }
                else if(tokenList[i].StartsWith("[IS_GEM"))
                {
                    string[] gemSplit = tokenList[i].Split(new char[] { ':' });
                    State state = State.SOLID;
                    //This is currently useless, figuring nothing else (AFAIK) writes to IS_GEM
                    if (RawFile.StripTokenEnding(gemSplit[3]) == "OVERWRITE_SOLID") state = State.ALL_SOLID;
                    this.stateList[state].name = gemSplit[1];
                    this.stateList[state].adj = gemSplit[1];
                    this.stateList[state].plural = gemSplit[2] == "STP" ? gemSplit[1] + "s" : gemSplit[2];
                    this.itemSymbol = '☼';
                    this.type = MaterialType.GEM;
                }
                else if(tokenList[i].StartsWith("[ITEM_SYMBOL"))
                {
                    string strippedChar = RawFile.StripTokenEnding(tokenList[i].Split(new char[] { ':' })[1]);
                    if (strippedChar.StartsWith("'"))
                    {
                        strippedChar = strippedChar.Replace("'", "");
                        itemSymbol = strippedChar[0];
                    }
                    else itemSymbol = DwarfFortressMono.fontManager.codepage[GetIntFromToken(strippedChar)];
                }
                else if(tokenList[i].StartsWith("[IS_"))
                {
                    string type = RawFile.StripTokenEnding(tokenList[i].Replace("[IS_", ""));
                    if (!Enum.TryParse<MaterialType>(type, out this.type)) throw new Exception("Bad MaterialType " + type + "!");
                }
                else if(tokenList[i].StartsWith("[ITEMS_"))
                {
                    string item = RawFile.StripTokenEnding(tokenList[i].Replace("[ITEMS_", ""));
                    ItemType itemType;
                    if (!Enum.TryParse<ItemType>(item, out itemType)) throw new Exception("Bad ItemType " + type + "!");
                    else this.canBeMade.Add(itemType);
                }
                else if(intProperties.ContainsKey(tokenList[i].Split(new char[] {':'})[0].Replace("[","")))
                {
                    intProperties[tokenList[i].Split(new char[] { ':' })[0].Replace("[", "")] = GetIntFromToken(RawFile.StripTokenEnding(tokenList[i].Split(new char[] { ':' })[1]));
                }
            }
        }

        public void InitDefaults()
        {
            intProperties.Add("MATERIAL_VALUE", 1);
            intProperties.Add("SPEC_HEAT", 0);
            intProperties.Add("IGNITE_POINT", 0);
            intProperties.Add("MELTING_POINT", 0);
            intProperties.Add("BOILING_POINT", 0);
            intProperties.Add("HEATDAM_POINT", 0);
            intProperties.Add("COLDDAM_POINT", 0);
            intProperties.Add("MAT_FIXED_TEMP", 0);
            intProperties.Add("SOLID_DENSITY", 0);
            intProperties.Add("LIQUIDD_ENSITY", 0);
            intProperties.Add("MOLAR_MASS", 0);
            intProperties.Add("IMPACT_YIELD", 10000);
            intProperties.Add("IMPACTF_RACTURE", 10000);
            intProperties.Add("IMPACT_STRAIN_AT_YIELD", 0);
            intProperties.Add("COMPRESSIVE_YIELD", 10000);
            intProperties.Add("COMPRESSIVEF_RACTURE", 10000);
            intProperties.Add("COMPRESSIVE_STRAIN_AT_YIELD", 0);
            intProperties.Add("TENSILE_YIELD", 10000);
            intProperties.Add("TENSILE_FRACTUE", 10000);
            intProperties.Add("TENSILE_STRAIN_ATY_IELD", 0);
            intProperties.Add("TORSION_YIELD", 10000);
            intProperties.Add("TORSION_FRACTURE", 10000);
            intProperties.Add("TORSION_STRAIN_AT_YIELD", 0);
            intProperties.Add("SHEAR_YIELD", 10000);
            intProperties.Add("SHEAR_FRACTURE", 10000);
            intProperties.Add("SHEAR_STRAIN_AT_YIELD", 0);
            intProperties.Add("BENDING_YIELD", 10000);
            intProperties.Add("BENDING_FRACTURE", 10000);
            intProperties.Add("BENDING_STRAIN_AT_YIELD", 0);
            intProperties.Add("MAX_EDGE", 10000);
            intProperties.Add("ABSORPTION", 0);
        }

        public int GetIntFromToken(string number)
        {
            if (number == "NONE") return 0;
            else return Convert.ToInt32(number);
        }

        public ColorPair GetItemDisplayColor()
        {
            return new ColorPair(this.displayColor.foreground, ColorManager.black);
        }

        public void CopyFromTemplate(string template)
        {
            if(!DwarfFortressMono.materialManager.materialTemplateList.ContainsKey(template)) throw new Exception("Bad material template requested: " + template);
            Material tempMaterial = DwarfFortressMono.materialManager.materialTemplateList[template];
            foreach(KeyValuePair<State, StateDescription> pair in tempMaterial.stateList)
            {
                if (!this.stateList.ContainsKey(pair.Key)) this.stateList.Add(pair.Key, new StateDescription(pair.Value));
                else this.stateList[pair.Key] = new StateDescription(pair.Value);
            }
            foreach(KeyValuePair<string, int> pair in tempMaterial.intProperties)
            {
                if (!this.intProperties.ContainsKey(pair.Key)) this.intProperties.Add(pair.Key, pair.Value);
                else this.intProperties[pair.Key] = pair.Value;
            }
            foreach(ItemType itemType in tempMaterial.canBeMade)
            {
                this.canBeMade.Add(itemType);
            }
            this.type = tempMaterial.type;
            this.tile = tempMaterial.tile;
            this.displayColor = tempMaterial.displayColor;
        }
    }
}
