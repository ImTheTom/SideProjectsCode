using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
TinyURL is a URL shortening service where you enter a URL such as https://leetcode.com/problems/design-tinyurl and it returns a short URL such as http://tinyurl.com/4e9iAk.

Design the encode and decode methods for the TinyURL service. There is no restriction on how your encode/decode algorithm should work.
You just need to ensure that a URL can be encoded to a tiny URL and the tiny URL can be decoded to the original URL.

https://leetcode.com/problems/encode-and-decode-tinyurl/description/

*/

namespace TinyURL {
    class Program {

        static List<string> keys = new List<string>();
        static List<string> URLS = new List<string>();

        static char[] base64 = new char[64] {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P',
                                             'Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d','e','f','g',
                                             'h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w', 'x',
                                             'y','z','0','1','2','3','4','5','6','7','8','9','+','/'};

        static void Main(string[] args) {
            string input = "https://leetcode.com/problems/design-tinyurl";
            string output = encode(input);
            Console.WriteLine("From that input we created this key" + output);
            string decoded = decode(output);
            Console.WriteLine("From that input we found this URL" + decoded);
            Console.ReadLine();
        }
        // Encodes a URL to a shortened URL
        public static string encode(string longUrl) {
            bool newKey = false;
            string key = "";
            do {
                key = CreateNewKey();
                int index = keys.IndexOf(key);
                if(index == -1) {
                    newKey = true;
                }
            } while (!newKey);
            keys.Add(key);
            URLS.Add(longUrl);
            return key;
        }

        public static string CreateNewKey() {
            string key = "";
            Random random = new Random();
            for (int i=0; i < 7; i++) {
                int current = random.Next(1, 64);
                key += base64[current].ToString();
            }
            return key;

        }

        // Decodes a shortened URL to its original URL.
        public static string decode(string shortUrl) {
            int index = keys.IndexOf(shortUrl);
            if(index == -1) {
                return "Can't find that code";
            } else {
                return URLS[index];
            }
        }
    }
}
