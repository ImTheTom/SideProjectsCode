using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

In English, we have a concept called root, which can be followed by some other words to form another longer word
let's call this word successor. For example, the root an, followed by other, which can form another word another.
Now, given a dictionary consisting of many roots and a sentence.You need to replace all the successor in the sentence with
the root forming it.If a successor has many roots can form it, replace it with the root with the shortest length.
You need to output the sentence after the replacement.

https://leetcode.com/problems/replace-words/description/

*/

namespace ReplaceWords {
    class Program {
        static void Main(string[] args) {
            List<string> inputArg = new List<string>();
            inputArg.Add("cat");
            inputArg.Add("bat");
            inputArg.Add("rat");
            string sentence = "the cattle was rattled by the battery";
            string output = ReplaceWords(inputArg, sentence);
            Console.WriteLine(output);
            Console.ReadKey();
        }

        static public string ReplaceWords(IList<string> dict, string sentence) {
            string[] stringArray = sentence.Split(new string[] { " " }, StringSplitOptions.None);
            for(int i = 0; i < stringArray.Length; i++) {
                for(int j=0; j<dict.Count(); j++) {
                    if (stringArray[i].Length > dict[j].Length) {
                        string current = stringArray[i].Substring(0, dict[j].Length);
                        if(current == dict[j]) {
                            stringArray[i] = dict[j];
                        }
                    }
                }
            }
            string seperator = " ";
            return String.Join(seperator, stringArray);
        }
    }
}
