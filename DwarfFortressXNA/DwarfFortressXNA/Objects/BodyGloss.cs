using System;

namespace DwarfFortressXNA.Objects
{
    public class BodyGloss
    {
        public Tuple<string, string> Singular;
        public Tuple<string, string> Plural; 

        public BodyGloss(string token)
        {
            var tokenSplit = token.Split(new[] {':'});
            Singular = new Tuple<string, string>(tokenSplit[2],tokenSplit[3]);
            Plural = new Tuple<string, string>(tokenSplit[4], RawFile.StripTokenEnding(tokenSplit[5]));
        }
    }
}
