using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaAPI.Data
{
    public class BoyerMoore
    {
        private static async Task<int[]> BuildBadCharTable(char[] needle)
        {
            int[] badShift = new int[256];
            for (int i = 0; i < 256; i++)
            {
                badShift[i] = needle.Length;
            }
            int last = needle.Length - 1;
            for (int i = 0; i < last; i++)
            {
                badShift[(int)needle[i]] = last - i;
            }
            return badShift;
        }

        public static async Task<int> BoyerMooreHorsepool(String pattern, String text)
        {
            char[] needle = pattern.ToCharArray();
            char[] haystack = text.ToCharArray();

            if (needle.Length > haystack.Length)
            {
                return -1;
            }
            int[] badShift = await BuildBadCharTable(needle);
            int offset = 0;
            int scan = 0;
            int last = needle.Length - 1;
            int maxoffset = haystack.Length - needle.Length;
            while (offset <= maxoffset)
            {
                for (scan = last; (needle[scan] == haystack[scan + offset]); scan--)
                {
                    if (scan == 0)
                    { //Match found
                        return offset;
                    }
                }
                offset += badShift[(int)haystack[offset + last]];
            }
            return -1;
        }
    }
}
