using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ISM6225_Hmwk
{
    class Hmwk2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Question 1:");
            int n = 7;
            PrintPatternAnyComplexity(n);
            Console.WriteLine();
            PrintPatternLinearComplexity(n);

            Console.WriteLine("\nQuestion 2:");
            int[] array1 = new int[] { 1, 3, 5, 4, 7 };
            int result = LongestSubSeq(array1);
            Console.WriteLine("The length of the longest continuous increasing subsequence: " + result);

            Console.WriteLine("\nQuestion 3");
            int[] array2 = new int[] { 1, 2, 3, 4, 5, 5 };
            PrintTwoParts(array2);

            Console.WriteLine("\nQuestion 4:");
            int[] array3 = new int[] { -4, -1, 0, 3, 10 };
            int[] result2 = SortedSquares(array3);
            prettyPrintIntArray(result2, 0, result2.Length - 1);

            Console.WriteLine("\nQuestion 5:");
            int[] nums1 = { 4, 2, 2, 4 };
            int[] nums2 = { 2, 2 };
            int[] intersect1 = Intersect(nums1, nums2);
            prettyPrintIntArray(intersect1, 0, intersect1.Length - 1);

            Console.WriteLine("\nQuestion 6:");
            int[] arr = new int[] { 1, 2, 2, 1, 1, 3 };
            Console.WriteLine(UniqueOccurrences(arr));

            Console.WriteLine("\nQuestion 7:");
            int[] numbers = { 0, 1, 3, 50, 75 };
            int lower = 0;
            int upper = 99;
            var resultList = Ranges(numbers, lower, upper);
            prettyPrintStringArray(resultList.ToArray(), 0, resultList.Count() - 1);

            Console.WriteLine("\nQuestion 8:");
            string[] names = new string[] { "pes", "fifa", "gta", "pes(2019)" };
            string[] namesResult = UniqFolderNames(names);
            prettyPrintStringArray(namesResult, 0, namesResult.Length - 1);
        }

        public static void PrintPatternAnyComplexity(int n)
        {
            for (int i = 1; i <= n; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    Console.Write('*');
                }

                Console.WriteLine();
            }
        }
        public static void PrintPatternLinearComplexity(int n)
        {
            var previousString = "";
            for (int i = 0; i < n; ++i)
            {
                var newString = previousString + '*';
                Console.WriteLine(newString);
                previousString = newString;
            }
        }

        public static int LongestSubSeq(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }

            int currentSubSeq = 1;
            int longestSubSeq = 1;
            for (int i = 0; i < nums.Length - 1; ++i)
            {
                if (nums[i] < nums[i + 1])
                {
                    ++currentSubSeq;
                    if (currentSubSeq > longestSubSeq)
                    {
                        longestSubSeq = currentSubSeq;
                    }
                }
                else
                {
                    currentSubSeq = 1;
                }
            }

            return longestSubSeq;
        }

        public static void PrintTwoParts(int[] arr1)
        {
            if (arr1.Length <= 1)
            {
                Console.WriteLine("False");
                return;
            }

            int head = 0;
            int tail = arr1.Length - 1;
            int headTotal = arr1[head];
            int tailTotal = arr1[tail];
            while (tail - 1 > head)
            {
                if (headTotal < tailTotal)
                {
                    ++head;
                    headTotal += arr1[head];
                }
                else
                {
                    --tail;
                    tailTotal += arr1[tail];
                }
            }

            if (headTotal == tailTotal)
            {
                prettyPrintIntArray(arr1, 0, head);
                prettyPrintIntArray(arr1, tail, arr1.Length - 1);
            }
            else
            {
                Console.WriteLine("False");
            }
        }

        public static int[] SortedSquares(int[] A)
        {
            // find index of first nonnegative
            int firstNonNegative = 0;
            for (int i = 0; i < A.Length; ++i)
            {
                if (A[i] >= 0)
                {
                    firstNonNegative = i;
                    break;
                }
            }

            // merge values into new array
            var squares = new int[A.Length];
            int head = firstNonNegative - 1;
            int tail = firstNonNegative;
            int index = 0;
            for (; head > -1 && tail < A.Length; ++index)
            {
                if (Math.Abs(A[head]) <= A[tail])
                {
                    squares[index] = A[head] * A[head];
                    --head;
                }
                else
                {
                    squares[index] = A[tail] * A[tail];
                    ++tail;
                }
            }

            if (head > -1)
            {
                for (; head > -1; ++index, --head)
                    squares[index] = A[head] * A[head];
            }
            else
            {
                for (; index < A.Length; ++index)
                    squares[index] = A[index] * A[index];
            }

            return squares;
        }

        public static int[] Intersect(int[] nums1, int[] nums2)
        {
            // build dictionary for first array
            var valuesDict = new Dictionary<int, int> { };
            foreach (int num in nums1)
            {
                if (valuesDict.ContainsKey(num))
                {
                    ++valuesDict[num];
                }
                else
                {
                    valuesDict[num] = 1;
                }
            }

            // iterate through second array
            var intersection = new List<int> { };
            foreach (int num in nums2)
            {
                if (valuesDict.ContainsKey(num) && valuesDict[num] > 0)
                {
                    intersection.Add(num);
                    --valuesDict[num];
                }
            }

            return intersection.ToArray();
        }

        public static bool UniqueOccurrences(int[] arr)
        {
            var occurrences = new Dictionary<int, int> { };
            foreach (int num in arr)
            {
                if (occurrences.ContainsKey(num))
                {
                    ++occurrences[num];
                }
                else
                {
                    occurrences[num] = 1;
                }
            }

            var occurrencesOfOccurrences = new HashSet<int> { };
            foreach (var pair in occurrences)
            {
                if (occurrencesOfOccurrences.Contains(pair.Value))
                {
                    return false;
                }
                else
                {
                    occurrencesOfOccurrences.Add(pair.Value);
                }
            }

            return true;
        }

        public static List<String> Ranges(int[] nums, int lower, int upper)
        {
            var ranges = new List<String> { };
            if (nums.Length == 0)
            {
                AddRange(ranges, lower - 1, upper + 1);
                return ranges;
            }

            if (nums[0] - lower >= 2)
            {
                AddRange(ranges, lower - 1, nums[0]);
            } else if (nums[0] - lower == 1) {
                ranges.Add(lower.ToString());
            }

            for (int i = 0; i < nums.Length - 1; ++i)
            {
                if (nums[i + 1] - nums[i] >= 2)
                {
                    AddRange(ranges, nums[i], nums[i + 1]);
                }
            }

            if (upper - nums[nums.Length - 1] >= 2)
            {
                AddRange(ranges, nums[nums.Length - 1], upper + 1);
            } else if (upper - nums[nums.Length - 1] == 1)
            {
                ranges.Add(upper.ToString());
            }

            return ranges;
        }

        public static void AddRange(List<String> ranges, int lower, int upper)
        {
            var stringToAdd = upper - lower == 2 ? (lower + 1).ToString() : String.Format("{0}->{1}", lower + 1, upper - 1);
            ranges.Add(stringToAdd);
        }

        public static string[] UniqFolderNames(string[] names)
        {
            var uniqueNames = new Dictionary<string, int> { };
            foreach (var name in names)
            {
                var matchCollection = Regex.Matches(name, @"^(.*)\((\d+)\)$");
                string originalName = null;
                if (matchCollection.Count() > 0)
                {
                    var match = matchCollection[0];
                    var baseName = match.Groups[1].ToString();
                    int k = int.Parse(match.Groups[2].ToString());
                    if (uniqueNames.ContainsKey(baseName))
                    {
                        originalName = k == uniqueNames[baseName] ? baseName : name;
                    }
                    else
                    {
                        originalName = name;
                    }
                }
                else
                {
                    originalName = name;
                }

                if (uniqueNames.ContainsKey(originalName))
                {
                    ++uniqueNames[originalName];
                }
                else
                {
                    uniqueNames[originalName] = 1;
                }
            }

            var newNames = new string[names.Length];
            int i = 0;
            foreach (var pair in uniqueNames)
            {
                for (int j = 0; j < pair.Value; ++j)
                {
                    var stringToAdd = j == 0 ? pair.Key : String.Format("{0}({1})", pair.Key, j);
                    newNames[i] = stringToAdd;
                    ++i;
                }
            }

            return newNames;
        }

        public static void prettyPrintIntArray(int[] arr, int startingIndex, int stoppingIndex)
        {
            Console.Write('[');
            for (int i = startingIndex; i <= stoppingIndex; ++i)
            {
                Console.Write(arr[i]);
                if (i < stoppingIndex)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(']');
        }

        public static void prettyPrintStringArray(string[] arr, int startingIndex, int stoppingIndex)
        {
            Console.Write('[');
            for (int i = startingIndex; i <= stoppingIndex; ++i)
            {
                Console.Write(String.Format("\"{0}\"", arr[i]));
                if (i < stoppingIndex)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(']');
        }
    }
}