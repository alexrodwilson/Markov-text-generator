using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{

    public class Generator
    {
        private IEnumerable<string>_sentences;
        private Dictionary<string, IEnumerable<string>> everythingTable;
        private Dictionary<string, IEnumerable<string>> beginningsTable;

        public Generator(IEnumerable<String> sentences, int order)
        {
            _sentences = sentences;
            everythingTable = new Dictionary<string, IEnumerable<string>>();
            beginningsTable = new Dictionary<string, IEnumerable<string>>();
            MakeTables(order, _sentences);
        }


        public string MakeMarkovSentence(Random random)
        {
            var nGrams = new List<string>();
            string currentKey = chooseRandom(beginningsTable.Keys, random);
            nGrams.Add(currentKey);
            while (nGrams.Count < 50 && (! isTerminal(currentKey)))
            {
                IEnumerable<String> possValues;
                if(everythingTable.TryGetValue(currentKey, out possValues))
                {
                    currentKey = chooseRandom(possValues, random);
                }
                else
                {
                    nGrams.Add(currentKey);
                    return String.Join(" ", nGrams);
                }
                nGrams.Add(currentKey);
            }

            return String.Join(" ",nGrams);
        }

        private static bool isTerminal(string s)
        {
            if (s == "") { return true; }
            char lastChar = s.Last();
            return lastChar == '!' || lastChar == '?' || lastChar == '.';
        }

        private static T chooseRandom<T>(IEnumerable<T> ienumerable, Random rand)
        {
            int choice = rand.Next(ienumerable.Count());
            return ienumerable.ToList().ElementAt(choice);
        }

        private void MakeTables(int order, IEnumerable<String> sentences)
        {

            foreach (string sentence in sentences)
            {
                processSentence(sentence, order);
            }
            
        }

         private void processSentence(String sentence, int order)
        {
            List<string> words = sentence.Split(' ').ToList();
            for (int i = 0;  i + order <= words.Count; i++)
            {
                string key = String.Join(" ", words.GetRange(i, order).ToArray());
                string value = (words.Count - (i + order) == 0) ? "" : String.Join(" ", words.GetRange(i + order, Math.Min(order, words.Count - (i + order))).ToArray());
                IEnumerable<string> tempVal;
                if (! everythingTable.TryGetValue(key, out tempVal))
                {
                    everythingTable.Add(key, new List<string> { value });
                    if (i == 0)
                    {
                        beginningsTable.Add(key, new List<string> { value });
                    }  
                }
                else
                {
                    var tempValList = tempVal.ToList();
                    tempValList.Add(value);
                    everythingTable.Remove(key);
                    everythingTable.Add(key, tempValList);
                    if (i == 0)
                    {
                        beginningsTable.Remove(key);
                        beginningsTable.Add(key, tempValList);
                    }
                }
            }
        }
    }
}
