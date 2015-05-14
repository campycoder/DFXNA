using System.Collections.Generic;

namespace DwarfFortressXNA.Objects
{
    public class BodyGloss
    {
        public Dictionary<string, string> Glossary;

        public BodyGloss(string token)
        {
            Glossary = new Dictionary<string, string>();
            var tokenSplit = token.Split(new[] {':'});
            for (var i = 2; i < tokenSplit.Length; i+=2)
            {
                var originalName = tokenSplit[i];
                var newName = RawFile.StripTokenEnding(tokenSplit[i + 1]);
                Glossary.Add(originalName, newName);
            }
        }
    }
}
