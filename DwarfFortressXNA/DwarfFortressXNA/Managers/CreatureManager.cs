using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DwarfFortressXNA.Objects;

namespace DwarfFortressXNA.Managers
{
    public class CreatureManager
    {
        public CreatureManager()
        {
            
        }

        public void ParseFromTokens(List<string> tokenList)
        {
            for (int i = 0; i < tokenList.Count; i++)
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
            }
            
        }


    }
}
