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
            try
            {
                int bottomLineLength = 2 * n - 1; // determine length of bottom side of triangle
                string[] outputArray = new string[n]; // array of strings for output
                for (int i = 1; i <= n; ++i)
                {
                    int starLength = 2 * i - 1; // determine number of stars on this line
                    int spacesEachSide = (bottomLineLength - starLength) / 2; // determine number of spaces on each side of stars
                    string spaces = String.Empty;
                    for (int j = 0; j < spacesEachSide; ++j) // build spaces string
                        spaces += ' ';

                    string stars = "*";
                    for (int j = 1; j < starLength; ++j) // build stars string
                        stars += '*';

                    outputArray[i - 1] = spaces + stars + spaces; // combine stars and spaces strings with spaces on either side of stars
                }

                foreach (string s in outputArray) // print contents of output array
                    Console.WriteLine(s);
            }
            catch (OverflowException oe)
            {
                Console.WriteLine("Invalid input provided to PrintTriangle method. Integer value must be nonnegative.");
                Console.WriteLine(oe.Message);
            }
        }

        private static void PrintSeriesSum(int n)
        {
            try
            {
                int[] odds = new int[n];
                for (int i = 1; i <= n; ++i) // build array of odd numbers
                    odds[i - 1] = 2 * i - 1; // the i-th odd number = 2 * i - 1

                Console.Write("The odd numbers are: ");
                for (int i = 0; i < n - 1; ++i)
                    Console.Write(String.Format("{0}, ", odds[i]));
                Console.WriteLine(odds[n - 1]);
                Console.WriteLine(String.Format("The sum is: {0}", odds.Sum()));
            } 
            catch (OverflowException oe)
            {
                Console.WriteLine("Invalid input provided to PrintTriangle method. Integer value must be nonnegative.");
                Console.WriteLine(oe.Message);
            }
        }

        public static bool MonotonicCheck(int[] A)
        {
            try
            {
                if (A.Length <= 1) // the array is trivially monotonic
                    return true;

                bool increasing = false; // is the array increasing or decreasing?
                bool firstNonEqualFound = false; // has the first element been found that is unequal to the previous element(s)?
                for (int i = 0; i < A.Length - 1; ++i)
                {
                    if (A[i] == A[i + 1]) // if element is equal to the next element, do nothing
                        continue;

                    if (A[i] < A[i + 1]) // these two elements are monotonically increasing
                    {
                        if (firstNonEqualFound && !increasing) // if the array has been monotonically decreasing so far, return false
                        {
                            return false;
                        }
                        else // the array has either been monotonically increasing so far or all previous elements have been equal
                        {
                            firstNonEqualFound = true; // these values might already be set to true, but no harm in setting them again
                            increasing = true;
                        }
                    }
                    else // these two elements are monotonically decreasing
                    {
                        if (firstNonEqualFound && increasing) // if the array has been monotonically increasing so far, return false
                            return false;
                        else
                            firstNonEqualFound = true; // this value might already be set to true, but no harm in setting it again
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something strange happened in the MonotonicCheck method that is unrecoverable.");
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static int DiffPairs(int[] nums, int k)
        {
            try
            {
                if (k < 0) // k must be nonnegative
                    throw new ArgumentException("Invalid input provided to DiffPairs method. Integer value must be nonnegative.");

                var pairs = new HashSet<(int, int)>(); // use a set of tuples to automatically eliminate duplicates
                for (int i = 0; i < nums.Length; ++i) // for every element in the array
                {
                    for (int j = i + 1; j < nums.Length; ++j) // for every element in the array at a higher index
                    {
                        var orderedPair = (Math.Min(nums[i], nums[j]), Math.Max(nums[i], nums[j])); // ensure pairs are always in increasing order
                        pairs.Add(orderedPair);
                    }
                }

                var numPairs = 0;
                foreach (var x in pairs) // count the number of pairs where the difference is equal to k
                    if (x.Item2 - x.Item1 == k)
                        ++numPairs;

                return numPairs;
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return 0;
            }
        }

        public static int BullsKeyboard(string keyboard, string word)
        {
            const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";

            try
            {
                foreach (var letter in ALPHABET)
                {
                    if (!keyboard.Contains(letter))
                    {
                        throw new ArgumentException("Keyboard provided to BullsKeyboard method does not contain required letter: " + letter);
                    }
                }

                foreach (var letter in word)
                {
                    if (!keyboard.Contains(letter))
                    {
                        throw new ArgumentException("Word provided to BullsKeyboard method contains letter not present in keyboard: " + letter);
                    }
                }

                var keyboardMap = new Dictionary<char, int>(); // put the index for each character in a dictionary for O(1) lookup
                for (int i = 0; i < keyboard.Length; ++i)
                    keyboardMap.Add(keyboard[i], i);

                int pos = 0;
                int time = 0;
                foreach (char c in word)
                {
                    int newPos = keyboardMap[c];
                    time += Math.Abs(newPos - pos); // the time equals the distance from the previous position
                    pos = newPos; // update the position
                }

                return time;
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return 0;
            }
        }

        public static int StringEdit(string str1, string str2)
        {
            int m = str1.Length;
            int n = str2.Length;
            var table = new int[m + 1, n + 1]; // a table to hold the scores
            /* The following two for loops populate the first row and first column of the
             * table, respectively. These represent the costs of performing multiple insertions
             * or deletions at the beginning of the word, which are clearly equal to the number
             * of insertions or deletions made multiplied by the insertion/deletion cost. Since
             * the insertion/deletion cost is 1, the cost for that cell is equal to the row
             * index in the case of the first column or the column index in the case of the first
             * row. */
            for (int i = 0; i < m + 1; ++i)
                table[i, 0] = i;
            for (int i = 0; i < n + 1; ++i)
                table[0, i] = i;

            /* Start at index 1,1 of the table and iterate through every remaining empty cell.
             * For each one, the trick is find the minimum cost out of the following three adjacent
             * cells:
             * 1. the one immediately above - this means the current operation should be an insertion
             * 2. the one to the immediately left - this means the current operation should be a deletion
             * 3. the one diagonally up and to the left - this means the current operation should be a
             *    substitution if the two characters being compared are not equivalent and a no-op if they
             *    are equivalent.
             * In the case of (3), no score will be added if the two letters are equal.
             * The last step is to assign the score for the current cell as the minimum score plus the
             * score addition. */
            for (int i = 1; i <= str1.Length; ++i)
            {
                for (int j = 1; j <= str2.Length; ++j)
                {
                    int insertScore = table[i, j - 1];
                    int removeScore = table[i - 1, j];
                    int replaceScore = table[i - 1, j - 1];
                    int minScore = Math.Min(Math.Min(insertScore, removeScore), replaceScore); // find the minimum of the three
                    int scoreAddition = str1[i - 1] == str2[j - 1] ? 0 : 1; // only add to the score if the characters are not equivalent
                    table[i, j] = minScore + scoreAddition; // calculate the score for the new cell
                }
            }

            /* return the score for the cell in the bottom right of the table, which represents the cost of editing
             * the entire string. */
            return table[m, n];
        }
    }
}
