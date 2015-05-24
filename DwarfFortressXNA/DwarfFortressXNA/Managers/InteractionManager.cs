using System.Collections.Generic;
using DwarfFortressXNA.Objects;

namespace DwarfFortressXNA.Managers
{
    public class InteractionManager : IObjectManager
    {
        public Dictionary<string, Interaction> InteractionList; 
        public InteractionManager()
        {
            InteractionList = new Dictionary<string, Interaction>();
        }

        public void AddToList(List<string> currentBuffer)
        {
            var name = RawFile.StripTokenEnding(currentBuffer[0].Remove(0, 13));
            var interaction = new Interaction(currentBuffer);
            InteractionList.Add(name, interaction);
        }

        public void ParseFromTokens(List<string> tokens)
        {
            var currentBuffer = new List<string>();
            foreach (var token in tokens)
            {
                if (token.StartsWith("[INTERACTION:") && currentBuffer.Count != 0)
                {
                    AddToList(currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(token);
                }
                else currentBuffer.Add(token);
            }
            AddToList(currentBuffer);
        }
    }
}
