using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InterviewTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int startIndex { get; set; }
        private int length { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            length = 0;
            startIndex = 0;
            //var result = TwoSum(new int[] { 3,2,4}, 6);
            //MessageBox.Show(string.Join(", ", result.ToList()));

            //var test = new char[,]{ { '1', '1', '0', '1', '0' }, { '1', '0', '0', '1', '0' }, { '1', '1', '0', '0', '0' }, { '1', '0', '0', '0', '0' } };
            //var result = NumIslands(test);     

            //var result = FindAnagrams("cbaebabacd", "abc");
            //var result = FindAnagrams("abacbabc", "abc");

            //var result = LengthOfLongestSubstring("aab");

            //var result = MaxProfit(new List<int>() { 7, 1, 5, 3, 6, 4}.ToArray());

            //var result = ProductExceptSelf(new List<int>() { 1,2,3,4 }.ToArray());

            var result = LongestPalindrome("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaabcaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            MessageBox.Show(result.ToString());
        }


        //1. Two Sum
        public int[] TwoSum(int[] nums, int target)
        {
            var result = new int[2];
            var dir = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dir.ContainsValue(target - nums[i]))
                {
                    result[0] = dir.Where(x => x.Value == target - nums[i]).Select(x => x.Key).Single();
                    result[1] = i;
                    break;
                }

                dir.Add(i, nums[i]);
            }
            return result;
        }

        //2. Valid Parentheses
        public bool IsValid(string s)
        {
            var stack = new Stack<char>();
            foreach (var c in s)
            {
                if (c == '(')
                {
                    stack.Push(')');
                }
                else if (c == '[')
                {
                    stack.Push(']');
                }
                else if (c == '{')
                {
                    stack.Push('}');
                }
                else if (stack.Count == 0 || stack.Pop() != c)
                {
                    return false;
                }
            }
            return stack.Count == 0;
        }

        //3. Copy List with Random Pointer
        //NEED CLEAR
        public RandomListNode CopyRandomList(RandomListNode head)
        {
            if (head == null) return null;
            var node = head;
            var result = new Dictionary<RandomListNode, RandomListNode>();
            //Copy regular list node
            while (node != null)
            {
                result.Add(node, new RandomListNode(node.label));
                node = node.next;
            }

            node = head;
            while (node != null)
            {
                if (node.next != null)
                    result[node].next = result[node.next];
                if (node.random != null)
                    result[node].random = result[node.random];
                node = node.next;
            }
            return result[head];
        }

        //4. Number of Islands
        public int NumIslands(char[,] grid)
        {
            var result = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == '1')
                    {
                        ResetIsland(grid, i, j);
                        result++;
                    }
                }
            }

            return result;
        }

        private void ResetIsland(char[,] grid, int i, int j)
        {
            if (i < 0 || i >= grid.GetLength(0)) return;
            if (j < 0 || j >= grid.GetLength(1)) return;
            if (grid[i, j] != '1') return;

            grid[i, j] = '0';

            this.ResetIsland(grid, i - 1, j);
            this.ResetIsland(grid, i + 1, j);
            this.ResetIsland(grid, i, j - 1);
            this.ResetIsland(grid, i, j + 1);
        }

        //5. Rotate Image
        //NEED CLEAR
        public void Rotate(int[,] matrix)
        {
            var rowCount = matrix.GetLength(0);
            var colCount = matrix.GetLength(1);
            var result = matrix;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = i; j < colCount; j++)
                {
                    var temp = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = temp;
                }
            }

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount / 2; j++)
                {
                    var temp = matrix[i, j];
                    matrix[i, j] = matrix[i, colCount - 1 - j];
                    matrix[i, colCount - 1 - j] = temp;
                }
            }
        }

        //6. Find All Anagrams In a String
        public IList<int> FindAnagrams(string s, string p)
        {
            if (string.IsNullOrEmpty(s)) return new List<int>();
            if (p.Length > s.Length) return new List<int>();
            var result = new List<int>();
            bool isMatch = false;
            var pLen = p.Length;
            var tempAnagramsString = p.OrderBy(x => x).ToList();
            var tempString = s.Substring(0, pLen).ToList();
            if (IsAnagrams(tempAnagramsString, tempString.OrderBy(x => x).ToList()))
            {
                result.Add(0);
                isMatch = true;
            }

            for (int i = pLen; i <= s.Length; i++)
            {
                if (isMatch)
                {
                    if (i == s.Length) continue;
                    if (tempString.First() == s[i])
                        result.Add(i - pLen + 1);
                    else
                        isMatch = false;
                }
                else
                {

                    if (IsAnagrams(tempAnagramsString, tempString.OrderBy(x => x).ToList()))
                    {
                        result.Add(i - pLen);
                        isMatch = true;
                    }
                    else
                    {
                        isMatch = false;
                    }
                }
                if (i == s.Length) continue;
                else
                {
                    tempString.RemoveAt(0);
                    tempString.Add(s[i]);
                }
            }
            return result;
        }

        public bool IsAnagrams(List<char> target, List<char> source)
        {
            for (int i = 0; i < target.Count; i++)
            {
                if (target[i] != source[i]) return false;
            }
            return true;
        }

        //7, Longest Substring Without Repeating Characters
        public int LengthOfLongestSubstring(string s)
        {
            var result = 0;
            var tempResult = new List<char>();
            foreach (var c in s)
            {
                var duplicatedIndex = tempResult.IndexOf(c);
                if (duplicatedIndex >= 0)
                {
                    if (tempResult.Count > result)
                        result = tempResult.Count;
                    tempResult.RemoveRange(0, duplicatedIndex + 1);
                    tempResult.Add(c);
                }
                else
                {
                    tempResult.Add(c);
                }
            }

            if (tempResult.Count > result)
                result = tempResult.Count;
            return result;
        }

        //8. Best Time to Buy and Sell Stock
        public int MaxProfit(int[] prices)
        {
            var minValue = 0;
            var profit = 0;
            if (prices.Length == 0)
                return 0;
            else
                minValue = prices[0];

            foreach (var p in prices)
            {
                if (minValue > p)
                    minValue = p;
                else
                {
                    if (profit < p - minValue)
                        profit = p - minValue;
                }
            }

            return profit;
        }

        //9. Product of Array Except Self
        public int[] ProductExceptSelf(int[] nums)
        {
            var result = new int[nums.Count()];
            var tmp = 1;
            //Calculate left
            for (int i = 0; i < nums.Count(); i++)
            {
                result[i] = tmp;
                tmp *= nums[i];
            }
            //Calculate right
            tmp = 1;
            for (int i = nums.Count() - 1; i >= 0; i--)
            {
                result[i] *= tmp;
                tmp *= nums[i];
            }

            return result;
        }

        //10. Longest Palidromic Substring
        public string LongestPalindrome(string s)
        {
            for (int i = 0; i < s.Length - 1; i++)
            {
                CheckPalidromic(i, i, s);
                if (s[i] == s[i + 1])
                    CheckPalidromic(i, i + 1, s);
            }

            return s.Substring(startIndex, length);
        }

        private void CheckPalidromic(int j, int k, string s)
        {
            while (j >= 0 && k < s.Length && s[j] == s[k])
            {
                j--;
                k++;
            }

            if (length < k - j - 1)
            {
                length = k - j - 1;
                startIndex = j + 1;
            }
        }

        //Letter Combinations of a Phone Number
        public IList<string> LetterCombinations(string digits)
        {
            var result = new List<string>();
            foreach (var c in digits)
            {

            }

            return result;
        }

        //private Dictionary<char, List<char>> phonenumber = new Dictionary<char, List<char>>
        //{
        //    {'0', new List<char>()},
        //    {'1', new List<char>()},
        //    {'2', new List<char>() {'a', 'b', 'c'} },
        //    {'3', new List<char>() {'d', 'e', 'f'}},
        //    {'4', new List<char>() {'g', 'h', 'i'}},
        //    {'5', new List<char>() {'j', 'k', 'l'}},
        //    {'6', new List<char>() {'m', 'n', 'o'}},
        //    {'7', new List<char>() {'p', 'q', 'r', 's'}},
        //    {'8', new List<char>() {'t', 'u', 'v'}},
        //    {'9', new List<char>() {'w', 'x', 'y', 'z'}}
        //};
        //private List<string> resstring = new List<string>();
        //public IList<string> LetterCombinations(string digits)
        //{
        //    LetterCombinationsHelper(digits, 0, new StringBuilder());

        //    return resstring;
        //}

        //private void LetterCombinationsHelper(string digits, int i, StringBuilder result)
        //{
        //    if (i >= digits.Length)
        //    {
        //        if (result.Length > 0)
        //        {
        //            resstring.Add(result.ToString());
        //        }
        //        return;
        //    }

        //    var cur = phonenumber[digits[i]];
        //    foreach (var letter in cur)
        //    {
        //        result.Append(letter);
        //        LetterCombinationsHelper(digits, i + 1, result);
        //        result.Remove(result.Length - 1, 1);
        //    }
        //}

        //
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            var tempList = new List<string>();




            return tempList.Count;
        }
    }
}
