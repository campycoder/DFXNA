using System;
using System.Collections.Generic;

namespace DwarfFortressXNA
{
    public class LanguageManager
    {

        public Dictionary<string, Word> WordList;
        public Dictionary<string, List<string>> SymbolList;
        public Dictionary<string, Dictionary<string, string>> TranslationList;
        public LanguageManager()
        {
            WordList = new Dictionary<string, Word>();
            SymbolList = new Dictionary<string, List<string>>();
            TranslationList = new Dictionary<string, Dictionary<string, string>>();
        }

        //Direct to the proper function!
        public void ParseFromTokens(List<string> tokens)
        {
            switch(tokens[0][1])
            {
                case 'W':
                    ParseAsWords(tokens);
                    break;
                case 'T':
                    ParseAsTranslation(tokens);
                    break;
                case 'S':
                    ParseAsSymbols(tokens);
                    break;
            }
        }

        private void ParseAsWords(IList<string> words)
        {
            var currentBuffer = new List<string>();
            foreach (var t in words)
            {
                if(t.StartsWith("[WORD:") && currentBuffer.Count > 0)
                {
                    var word = new Word(currentBuffer);
                    WordList.Add(word.Id, word);
                    currentBuffer.Clear();
                    currentBuffer.Add(t);
                }
                else currentBuffer.Add(t);
            }
        }

        private void ParseAsTranslation(IList<string> translation)
        {
            var id = translation[0].Remove(0, 13).Replace("]", "");
            var translationDic = new Dictionary<string,string>();
            translation.Remove(translation[0]);
            foreach (var t in translation)
            {
                if (!t.StartsWith("[T_WORD:")) continue;
                var split = t.Split(new[] { ':' });
                var real = split[1];
                var trans = split[2].Replace("]", "");
                translationDic.Add(real, trans);
            }
            TranslationList.Add(id, translationDic);
        }

        private void ParseAsSymbols(IList<string> symbols)
        {
            var currentBuffer = new List<string>();
            foreach (var t in symbols)
            {
                if (t.StartsWith("[SYMBOL:") && currentBuffer.Count > 0)
                {
                    var id = currentBuffer[0].Remove(0, 8).Replace("]", "");
                    currentBuffer.Remove(currentBuffer[0]);
                    for(var j = 0; j < currentBuffer.Count;j++)
                    {
                        if (!currentBuffer[j].StartsWith("[S_WORD:"))
                        {
                            currentBuffer.Remove(currentBuffer[j]);
                            j = j - 1;
                        }
                        else currentBuffer[j] = currentBuffer[j].Remove(0, 8).Replace("]", "");
                    }
                    SymbolList.Add(id, currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(t);
                }
                else currentBuffer.Add(t);
            }
            Console.WriteLine("done!");
        }
        public string GetTranslation(string toTranslate, string language)
        {
            toTranslate = toTranslate.ToUpper();
            language = language.ToUpper();
            if (!TranslationList.ContainsKey(language))
            {
                throw new Exception("Bad language " + language + " requested!");
            }
            if (!TranslationList[language].ContainsKey(toTranslate))
            {
                throw new Exception("Nonexistant word " + toTranslate + " requested in language " + language + "!");
            }
            return TranslationList[language][toTranslate];
        }
    }
}
