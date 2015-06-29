using System.Collections.Generic;
using DwarfFortressXNA.Objects;

namespace DwarfFortressXNA.Managers
{
    public class CreatureManager : IObjectManager
    {
        /// <summary>
        /// List of creature definitions, indexed by their CREATURE token name.
        /// </summary>
        public Dictionary<string, Creature> CreatureList;
        public CreatureManager()
        {
            CreatureList = new Dictionary<string, Creature>();
        }

        /// <summary>
        /// Internal Function. Create a definition and add it to the CreatureList.
        /// </summary>
        /// <param name="currentBuffer">Token buffer for the definition.</param>
        public void AddToList(List<string> currentBuffer)
        {
            var name = RawFile.StripTokenEnding(currentBuffer[0].Remove(0, 10));
            var creature = new Creature(name, currentBuffer);
            CreatureList.Add(name, creature);
        }

        /// <summary>
        /// Internal Function. Initiates the appropriate parsing/adding process for the requested token buffer.
        /// </summary>
        /// <param name="tokens">Token buffer to parse.</param>
        public void ParseFromTokens(List<string> tokens)
        {
            var currentBuffer = new List<string>();
            foreach (var token in tokens)
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
