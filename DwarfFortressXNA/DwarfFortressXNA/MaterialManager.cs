using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfFortressXNA
{
    public class MaterialManager
    {
        public Dictionary<string, Material> materialTemplateList;
        public Dictionary<string, Material> inorganicMaterialList;

        public MaterialManager()
        {
            this.materialTemplateList = new Dictionary<string,Material>();
            this.inorganicMaterialList = new Dictionary<string,Material>();
        }

        public void AddToList(List<string> currentBuffer)
        {
            if (currentBuffer[0].StartsWith("[MATERIAL_TEMPLATE:"))
            {
                Material material = new Material(currentBuffer);
                this.materialTemplateList.Add(RawFile.StripTokenEnding(currentBuffer[0].Remove(0, 19)), material);
            }
            else if (currentBuffer[0].StartsWith("[INORGANIC:"))
            {
                Material material = new Material(currentBuffer);
                this.inorganicMaterialList.Add(RawFile.StripTokenEnding(currentBuffer[0].Remove(0, 11)), material);
            }
        }

        public void ParseFromTokens(List<string> tokens)
        {
            string currentId;
            List<string> currentBuffer = new List<string>();
            for(int i = 0;i < tokens.Count;i++)
            {
                if (tokens[i].StartsWith("[MATERIAL_TEMPLATE:") && currentBuffer.Count > 0)
                {
                    AddToList(currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(tokens[i]);
                }
                else if (tokens[i].StartsWith("[INORGANIC:") && currentBuffer.Count > 0)
                {
                    AddToList(currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(tokens[i]);
                }
                else currentBuffer.Add(tokens[i]);
            }
            AddToList(currentBuffer);
            Console.WriteLine("Done!");
        }
    }
}
