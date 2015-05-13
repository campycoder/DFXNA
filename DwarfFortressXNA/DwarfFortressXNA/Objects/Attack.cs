using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfFortressXNA.Objects
{
    public class Attack
    {
        public string Name;
        public string BodypartToken;
        public Attack(string name, string bodypartToken, List<string> tokenList)
        {
            Name = name;
            BodypartToken = bodypartToken;
        }
    }
}
