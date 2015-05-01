using System.Collections.Generic;
using System.Linq;

namespace DwarfFortressXNA.Objects
{
    public class BodyDetailPlan
    {
        public Dictionary<string, string> MaterialList;
        public Dictionary<string, string> TissueList; 
 
        public BodyDetailPlan(List<string> tokenList)
        {
            MaterialList = new Dictionary<string, string>();
            TissueList = new Dictionary<string, string>();
            for (var index = 0; index < tokenList.Count; index++)
            {
                var token = tokenList[index];
                if (RawFile.NumberOfTokens(token) > 1)
                {
                    var multiple = token.Split(new[] {']'}).ToList();
                    multiple.Remove("");
                    for (var j = 0; j < multiple.Count; j++)
                    {
                        multiple[j] = multiple[j] + "]";
                    }
                    tokenList.Remove(token);
                    tokenList.InsertRange(index, multiple);
                }
                if (token.StartsWith("[ADD_MATERIAL"))
                {
                    var split = token.Split(new[] {':'});
                    var id = split[1];
                    var template = RawFile.StripTokenEnding(split[2]);
                    MaterialList.Add(id, template);
                }
                else if (token.StartsWith("[ADD_TISSUE"))
                {
                    var split = token.Split(new[] {':'});
                    var id = split[1];
                    var template = RawFile.StripTokenEnding(split[2]);
                    TissueList.Add(id, template);
                }
            }
        }
    }
}
