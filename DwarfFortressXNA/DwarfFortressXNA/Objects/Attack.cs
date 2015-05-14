using System.Collections.Generic;

namespace DwarfFortressXNA.Objects
{
    public class Attack
    {
        public string Name;
        public string BodypartToken;
        // ReSharper disable once UnusedParameter.Local
        public Attack(string name, string bodypartToken, List<string> tokenList)
        {
            Name = name;
            BodypartToken = bodypartToken;
        }
    }
}
