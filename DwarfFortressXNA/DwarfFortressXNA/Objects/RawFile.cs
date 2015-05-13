using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DwarfFortressXNA.Objects
{
    public class TokenParseException : Exception
    {
        public TokenParseException(string parserName, string message) :base("Token Parser " + parserName + " threw this exception: " + message)
        {
            
        }
    }
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
        public string Filename;

        public RawType Type;

        public List<string> TokensRaw;

        public RawFile(string path)
        {
            TokensRaw = new List<string>();
            string line;
            if (!File.Exists(path)) throw new Exception("Raw file " + path + " does not exist!");
            var file = new StreamReader(path, Encoding.UTF8, true);
            Filename = file.ReadLine();
            file.ReadLine();
            var typeS = file.ReadLine();
            while (typeS != null && (!typeS.StartsWith("[OBJECT") && !file.EndOfStream)) typeS = file.ReadLine();
            if (typeS != null && !Enum.TryParse(typeS.Substring(8, typeS.Length - 9), out Type)) throw new Exception("Invalid object token " + typeS.Substring(8, typeS.Length - 9) + "!");
            file.ReadLine();
            while((line = file.ReadLine()) != null)
            {
                line = line.Replace("\t", "");
                if (line.Length > 0 && line[0] == '[') TokensRaw.Add(line);
            }
            ParseStringsIntoTokens();
        }

        //Seperating this out because god this is really ugly.
        public void ParseStringsIntoTokens()
        {
            switch(Type)
            {
                case RawType.LANGUAGE:
                    DwarfFortress.LanguageManager.ParseFromTokens(TokensRaw);
                    break;
                case RawType.MATERIAL_TEMPLATE:
                case RawType.INORGANIC:
                case RawType.PLANT:
                    DwarfFortress.MaterialManager.ParseFromTokens(TokensRaw);
                    break;
                case RawType.TISSUE_TEMPLATE:
                    DwarfFortress.TissueManager.ParseFromTokens(TokensRaw);
                    break;
                case RawType.BODY:
                case RawType.BODY_DETAIL_PLAN:
                    DwarfFortress.BodyManager.ParseFromTokens(TokensRaw);
                    break;
                case RawType.CREATURE:
                    DwarfFortress.CreatureManager.ParseFromTokens(TokensRaw);
                    break;
            }
        }

        public static int GetIntFromToken(string number)
        {
            if (number == "NONE") return 0;
            return Convert.ToInt32(number);
        }

        public static string StripTokenEnding(string token)
        {
            for(var i = 0;i < token.Length;i++)
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
            return tokenLine.Count(t => t == '[');
        }
    }
}
