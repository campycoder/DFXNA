using System.Collections.Generic;
using DwarfFortressXNA.Objects;

namespace DwarfFortressXNA.Managers
{
    public class CreatureManager
    {
        public Dictionary<string, Creature> CreatureList;
        public CreatureManager()
        {
            CreatureList = new Dictionary<string, Creature>();
        }

        public void AddToList(List<string> currentBuffer)
        {
            var creature = new Creature(currentBuffer);
            CreatureList.Add(RawFile.StripTokenEnding(currentBuffer[0].Remove(0,10)), creature);
        }

        public void ParseFromTokens(List<string> tokens)
        {
            var currentBuffer = new List<string>();
            foreach (string token in tokens)
            {
                if (token.StartsWith("[CREATURE:") && currentBuffer.Count != 0)
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
