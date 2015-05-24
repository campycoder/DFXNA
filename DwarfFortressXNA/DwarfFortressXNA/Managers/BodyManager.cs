using System.Collections.Generic;
using DwarfFortressXNA.Objects;

namespace DwarfFortressXNA.Managers
{
    public class BodyManager : IObjectManager
    {
        public Dictionary<string, BodyDetailPlan> BodyDetailPlanList;
        public Dictionary<string, BodyTemplate> BodyTemplateList;
        public Dictionary<string, BodyGloss> BodyGlossList; 
        public BodyManager()
        {
            BodyDetailPlanList = new Dictionary<string, BodyDetailPlan>();
            BodyTemplateList = new Dictionary<string, BodyTemplate>();
            BodyGlossList = new Dictionary<string, BodyGloss>();
        }

        public void AddToList(List<string> currentBuffer)
        {
            if (currentBuffer[0].StartsWith("[BODY_DETAIL_PLAN:"))
            {
                var id = RawFile.StripTokenEnding(currentBuffer[0].Split(new[] { ':' })[1]);
                var detailPlan = new BodyDetailPlan(currentBuffer);
                BodyDetailPlanList.Add(id, detailPlan); 
            }
            else if (currentBuffer[0].StartsWith("[BODY:"))
            {
                var id = RawFile.StripTokenEnding(currentBuffer[0].Split(new[] { ':' })[1]);
                var bodyTemplate = new BodyTemplate(currentBuffer);
                BodyTemplateList.Add(id, bodyTemplate);
            }
            else if (currentBuffer[0].StartsWith("[BODYGLOSS:"))
            {
                var id = currentBuffer[0].Split(new[] {':'})[1];
                var bodyGloss = new BodyGloss(currentBuffer[0]);
                BodyGlossList.Add(id, bodyGloss);
            }
            
        }

        public void ParseFromTokens(List<string> tokenList)
        {
            var currentBuffer = new List<string>();
            foreach (var t in tokenList)
            {
                if (((t.StartsWith("[BODY_DETAIL_PLAN") || t.StartsWith("[BODY:") || t.StartsWith("[BODYGLOSS:")) && currentBuffer.Count > 0))
                {
                    AddToList(currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(t);
                }
                else currentBuffer.Add(t);
            }
        }
    }
}
