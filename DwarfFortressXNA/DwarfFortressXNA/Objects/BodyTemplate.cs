using System.Collections.Generic;

namespace DwarfFortressXNA.Objects
{
    public class BodyTemplate
    {
        public Dictionary<string, BodyPart> BodyPartList;
        public BodyTemplate(List<string> tokenList)
        {
            BodyPartList = new Dictionary<string, BodyPart>();
            var currentBuffer = new List<string>();
            tokenList.RemoveAt(0);
            foreach (var token in tokenList)
            {
                if (token.StartsWith("[BP") && currentBuffer.Count > 0)
                {
                    AddBodyPart(currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(token);
                }
                else currentBuffer.Add(token);
            }    
            if(currentBuffer.Count != 0) AddBodyPart(currentBuffer);
        }

        public void AddBodyPart(List<string> currentBuffer)
        {
            var part = new BodyPart(currentBuffer);
            var id = currentBuffer[0].Split(new[] { ':' })[1];
            BodyPartList.Add(id, part);
        }
    }
}
