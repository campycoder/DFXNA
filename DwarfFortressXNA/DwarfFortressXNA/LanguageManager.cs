using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfFortressXNA
{
    public class LanguageManager
    {

        public Dictionary<string, Word> wordList;
        public Dictionary<string, List<string>> symbolList;
        public Dictionary<string, Dictionary<string, string>> translationList;
        public LanguageManager()
        {
            wordList = new Dictionary<string, Word>();
            symbolList = new Dictionary<string, List<string>>();
            translationList = new Dictionary<string, Dictionary<string, string>>();
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

        private void ParseAsWords(List<string> words)
        {
            List<string> currentBuffer = new List<string>();
            for(int i = 0; i < words.Count;i++)
            {
                if(words[i].StartsWith("[WORD:") && currentBuffer.Count > 0)
                {
                    Word word = new Word(currentBuffer);
                    this.wordList.Add(word.id, word);
                    currentBuffer.Clear();
                    currentBuffer.Add(words[i]);
                }
                else currentBuffer.Add(words[i]);
            }
        }

        private void ParseAsTranslation(List<string> translation)
        {
            string id = translation[0].Remove(0, 13).Replace("]", "");
            Dictionary<string, string> translationDic = new Dictionary<string,string>();
            translation.Remove(translation[0]);
            for(int i = 0;i < translation.Count;i++)
            {
                if (!translation[i].StartsWith("[T_WORD:")) continue;
                string[] split = translation[i].Split(new char[] { ':' });
                string real = split[1];
                string trans = split[2].Replace("]", "");
                translationDic.Add(real, trans);
            }
            translationList.Add(id, translationDic);
        }

        private void ParseAsSymbols(List<string> symbols)
        {
            List<string> currentBuffer = new List<string>();
            for (int i = 0; i < symbols.Count; i++)
            {
                if (symbols[i].StartsWith("[SYMBOL:") && currentBuffer.Count > 0)
                {
                    string id = currentBuffer[0].Remove(0, 8).Replace("]", "");
                    currentBuffer.Remove(currentBuffer[0]);
                    for(int j = 0; j < currentBuffer.Count;j++)
                    {
                        if (!currentBuffer[j].StartsWith("[S_WORD:"))
                        {
                            currentBuffer.Remove(currentBuffer[j]);
                            j = j - 1;
                        }
                        else currentBuffer[j] = currentBuffer[j].Remove(0, 8).Replace("]", "");
                    }
                    this.symbolList.Add(id, currentBuffer);
                    currentBuffer.Clear();
                    currentBuffer.Add(symbols[i]);
                }
                else currentBuffer.Add(symbols[i]);
            }
            Console.WriteLine("done!");
        }
        public string GetTranslation(string toTranslate, string language)
        {
            toTranslate = toTranslate.ToUpper();
            language = language.ToUpper();
            if (!translationList.ContainsKey(language))
            {
                throw new Exception("Bad language " + language + " requested!");
                return "";
            }
            else
            {
                if (!translationList[language].ContainsKey(toTranslate))
                {
                    throw new Exception("Nonexistant word " + toTranslate + " requested in language " + language + "!");
                    return "";
                }
                else return translationList[language][toTranslate];
            }
        }
    }
}
