using System;
using System.Collections.Generic;
using DwarfFortressXNA.Objects;

namespace DwarfFortressXNA.Managers
{
    public class MaterialManager : IObjectManager
    {
        public Dictionary<string, Material> MaterialTemplateList;
        public Dictionary<string, Material> InorganicMaterialList;

        public MaterialManager()
        {
            MaterialTemplateList = new Dictionary<string,Material>();
            InorganicMaterialList = new Dictionary<string,Material>();
        }

        public void AddToList(List<string> currentBuffer)
        {
            if (currentBuffer[0].StartsWith("[MATERIAL_TEMPLATE:"))
            {
                var material = new Material(currentBuffer);
                MaterialTemplateList.Add(RawFile.StripTokenEnding(currentBuffer[0].Remove(0, 19)), material);
            }
            else if (currentBuffer[0].StartsWith("[INORGANIC:"))
            {
                var material = new Material(currentBuffer);
                InorganicMaterialList.Add(RawFile.StripTokenEnding(currentBuffer[0].Remove(0, 11)), material);
            }
        }

        public void ParseFromTokens(List<string> tokens)
        {
            var currentBuffer = new List<string>();
            foreach (var t in tokens)
            {
                if (t.StartsWith("[MATERIAL_TEMPLATE:") && currentBuffer.Count > 0)
                {
                    AddToList(currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(t);
                }
                else if (t.StartsWith("[INORGANIC:") && currentBuffer.Count > 0)
                {
                    AddToList(currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(t);
                }
                else currentBuffer.Add(t);
            }
            AddToList(currentBuffer);
        }

        public Material MaterialSearch(string type, string param, object parent = null)
        {
            switch (type)
            {
                case "INORGANIC":
                    if (!InorganicMaterialList.ContainsKey(param)) throw new TokenParseException("Caste", "Bad inorganic material " + param + "!");
                    return InorganicMaterialList[param];
                case "LOCAL_CREATURE_MAT":
                    if (parent == null) throw new Exception("Parent wasn't passed to MaterialSearch on LocalCreatureMat!");
                    var parentObject = (Creature)parent;
                    if (!parentObject.MaterialList.ContainsKey(param)) throw new TokenParseException("Caste", "Bad LocalCreature material " + param + "!");
                    return parentObject.MaterialList[param];
                default:
                    return null;
            }
        }
    }
}
