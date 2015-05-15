using System.Collections.Generic;

namespace DwarfFortressXNA.Objects
{
    public class Attack
    {
        public string Name;
        public List<string> BodyPartRefs; 
        // ReSharper disable once UnusedParameter.Local
        public Attack(string name, List<string> bodyPartRefs)
        {
            Name = name;
            BodyPartRefs = bodyPartRefs;
        }
    }
}
