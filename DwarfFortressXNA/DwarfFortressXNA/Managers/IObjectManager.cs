using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DwarfFortressXNA.Managers
{
    public interface IObjectManager
    {
        void AddToList(List<string> currentBuffer);
        void ParseFromTokens(List<string> tokens);
    }
}
