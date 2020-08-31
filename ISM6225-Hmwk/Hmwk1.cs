using System;
using System.Collections.Generic;
using System.Linq;

namespace ISM6225_Hmwk
{
    class Hmwk1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Question 1:");
            int n = 5;
            PrintTriangle(n);
            Console.WriteLine();

            Console.WriteLine("Question 2:");
            int n2 = 5;
            PrintSeriesSum(n2);
            Console.WriteLine();

            Console.WriteLine("Question 3:");
            int[] A = new int[] { 1, 2, 2, 6 };
            bool check = MonotonicCheck(A);
            Console.Write("The given array is");
            if (!check)
                Console.Write(" not");
            Console.WriteLine(" monotonic.\n");

            Console.WriteLine("Question 4:");
            int[] nums = new int[] { 3, 1, 4, 1, 5 };
            int k = 2;
            int pairs = DiffPairs(nums, k);
            Console.WriteLine(String.Format("The number of {0}-diff pairs in the array is: {1}\n", k, pairs));

            Console.WriteLine("Question 5:");
            string keyboard = "abcdefghijklmnopqrstuvwxyz";
            string word = "dis";
            int time = BullsKeyboard(keyboard, word);
            Console.WriteLine(String.Format("Word \"{0}\" using keyboard \"{1}\" takes total time: {2}\n", word, keyboard, time));

            Console.WriteLine("Question 6:");
            //string str1 = "goulls";
            //string str2 = "gobulls";
            string str1 = "sunday";
            string str2 = "saturday";
            int minEdits = StringEdit(str1, str2);
            Console.WriteLine(String.Format("Converting \"{0}\" to \"{1}\" requires cost: {2}", str1, str2, minEdits));
        }

        private static void PrintTriangle(int n)
        {
            int bottomLineLength = 2 * n - 1;
            string[] outputArray = new string[n];
            for (int i = 1; i <= n; ++i)
            {
                int starLength = 2 * i - 1;
                int spacesEachSide = (bottomLineLength - starLength) / 2;
                string spaces = System.String.Empty;
                for (int j = 0; j < spacesEachSide; ++j)
                    spaces += ' ';

                string stars = "*";
                for (int j = 1; j < starLength; ++j)
                    stars += '*';

                outputArray[i - 1] = spaces + stars + spaces;
            }

            foreach (string s in outputArray)
                Console.WriteLine(s);
        }

        private static void PrintSeriesSum(int n)
        {
            int[] odds = new int[n];
            for (int i = 1; i <= n; ++i)
                odds[i - 1] = 2 * i - 1;

            Console.Write("The odd numbers are: ");
            for (int i = 0; i < n - 1; ++i)
                Console.Write(String.Format("{0}, ", odds[i]));
            Console.WriteLine(odds[n - 1]);
            Console.WriteLine(String.Format("The sum is: {0}", odds.Sum()));
        }

        public static bool MonotonicCheck(int[] A)
        {
            if (A.Length <= 1)
                return true;

            bool increasing = false;
            bool firstNonEqualFound = false;
            for (int i = 0; i < A.Length - 1; ++i)
            {
                if (A[i] == A[i + 1])
                    continue;

                if (A[i] < A[i + 1])
                {
                    if (firstNonEqualFound && !increasing)
                    {
                        return false;
                    }
                    else
                    {
                        firstNonEqualFound = true;
                        increasing = true;
                    }
                }
                else
                {
                    if (firstNonEqualFound && increasing)
                        return false;
                    else
                        firstNonEqualFound = true;
                }
            }

            return true;
        }

        public static int DiffPairs(int[] nums, int k)
        {
            var pairs = new HashSet<(int, int)>();
            for (int i = 0; i < nums.Length; ++i)
            {
                for (int j = i + 1; j < nums.Length; ++j)
                {
                    var orderedPair = (Math.Min(nums[i], nums[j]), Math.Max(nums[i], nums[j]));
                    pairs.Add(orderedPair);
                }
            }

            var numPairs = 0;
            foreach (var x in pairs)
                if (x.Item2 - x.Item1 == k)
                    ++numPairs;

            return numPairs;
        }

        public static int BullsKeyboard(string keyboard, string word)
        {
            var keyboardMap = new Dictionary<char, int>();
            for (int i = 0; i < keyboard.Length; ++i)
                keyboardMap.Add(keyboard[i], i);

            int pos = 0;
            int time = 0;
            foreach (char c in word)
            {
                int newPos = keyboardMap[c];
                time += Math.Abs(newPos - pos);
                pos = newPos;
            }

            return time;
        }

        public static int StringEdit(string str1, string str2)
        {
            int m = str1.Length;
            int n = str2.Length;
            var table = new int[m + 1, n + 1];
            for (int i = 0; i < m + 1; ++i)
                table[i, 0] = i;
            for (int i = 0; i < n + 1; ++i)
                table[0, i] = i;

            for (int i = 1; i <= str1.Length; ++i)
            {
                for (int j = 1; j <= str2.Length; ++j)
                {
                    int insertScore = table[i, j - 1];
                    int removeScore = table[i - 1, j];
                    int replaceScore = table[i - 1, j - 1];
                    int minScore = Math.Min(Math.Min(insertScore, removeScore), replaceScore);
                    int scoreAddition = str1[i - 1] == str2[j - 1] ? 0 : 1;
                    table[i, j] = minScore + scoreAddition;
                }
            }

            return table[m, n];
        }
    }
}
