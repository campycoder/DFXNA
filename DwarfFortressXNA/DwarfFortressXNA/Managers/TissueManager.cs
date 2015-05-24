using System.Collections.Generic;
using DwarfFortressXNA.Objects;

namespace DwarfFortressXNA.Managers
{
    public class TissueManager : IObjectManager
    {
        public Dictionary<string, Tissue> TissueTemplateList;

        public TissueManager()
        {
            TissueTemplateList =new Dictionary<string, Tissue>();
        }

        public void AddToList(List<string> currentBuffer)
        {
            if (!currentBuffer[0].StartsWith("[TISSUE_TEMPLATE")) return;
            var tissue = new Tissue(currentBuffer);
            TissueTemplateList.Add(RawFile.StripTokenEnding(currentBuffer[0].Remove(0,17)),tissue);
        }

        public void ParseFromTokens(List<string> tokenList)
        {
            var currentBuffer = new List<string>();
            foreach (var t in tokenList)
            {
                if (!t.StartsWith("[TISSUE_TEMPLATE") || currentBuffer.Count == 0) currentBuffer.Add(t);
                else
                {
                    AddToList(currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(t); 
                }
                
            }
            AddToList(currentBuffer);
        }
    }
}
