using System.Collections.Generic;

namespace DwarfFortressXNA.Objects
{
    public class BodyGloss
    {
        public KeyValuePair<string, string> Singular;
        public KeyValuePair<string, string> Plural; 

        public BodyGloss(string token)
        {
            var tokenSplit = token.Split(new[] {':'});
            Singular = new KeyValuePair<string, string>(tokenSplit[2],tokenSplit[3]);
            Plural = new KeyValuePair<string, string>(tokenSplit[4], RawFile.StripTokenEnding(tokenSplit[5]));
        }
    }
}
