using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DwarfFortressXNA
{
    public enum RawType
    {
        BODY,
        BODY_DETAIL_PLAN,
        BUILDING,
        CREATURE,
        CREATURE_VARIATION,
        DESCRIPTOR_COLOR,
        DESCRIPTOR_PATTERN,
        DESCRIPTOR_SHAPE,
        ENTITY,
        GRAPHICS,
        INTERACTION,
        INORGANIC,
        ITEM,
        LANGUAGE,
        MATERIAL_TEMPLATE,
        PLANT,
        REACTION,
        TISSUE_TEMPLATE
    }

    public class RawFile
    {
        public string filename;

        public RawType type;

        public List<string> tokensRaw;

        public RawFile(string path)
        {
            tokensRaw = new List<string>();
            string line = "";
            if (!File.Exists(path)) throw new Exception("Raw file " + path + " does not exist!");
            StreamReader file = new StreamReader(path, Encoding.UTF8, true);
            filename = file.ReadLine();
            file.ReadLine();
            string typeS = file.ReadLine();
            while (!typeS.StartsWith("[OBJECT") && !file.EndOfStream) typeS = file.ReadLine();
            if (!Enum.TryParse<RawType>(typeS.Substring(8, typeS.Length - 9), out type)) throw new Exception("Invalid object token " + typeS.Substring(8, typeS.Length - 9) + "!");
            file.ReadLine();
            while((line = file.ReadLine()) != null)
            {
                line = line.Replace("\t", "");
                if (line.Length > 0 && line[0] == '[') tokensRaw.Add(line);
            }
            ParseStringsIntoTokens();
        }

        //Seperating this out because god this is really ugly.
        public void ParseStringsIntoTokens()
        {
            switch(this.type)
            {
                case RawType.LANGUAGE:
                    DwarfFortressMono.languageManager.ParseFromTokens(tokensRaw);
                    break;
                case RawType.MATERIAL_TEMPLATE:
                case RawType.INORGANIC:
                case RawType.PLANT:
                    DwarfFortressMono.materialManager.ParseFromTokens(tokensRaw);
                    break;
                default:
                    break;
            }
        }

        public static string StripTokenEnding(string token)
        {
            for(int i = 0;i < token.Length;i++)
            {
                if (token[i] == ']')
                {
                    return token.Remove(i);
                }
            }
            return token;
        }

        public static int NumberOfTokens(string tokenLine)
        {
            int count = 0;
            for(int i = 0;i < tokenLine.Length;i++)
            {
                if (tokenLine[i] == '[') count++;
            }
            return count;
        }
       
    }
}
