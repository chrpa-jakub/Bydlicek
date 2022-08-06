using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LiveBot
{
    public class SimilarityChecker
    {
        public bool IsDuplicate(string newString, List<string> oldStrings)
        {
            var newCleanedSplit = CleanString(newString).Split(" ");
            var sameWordCounts = new List<int>();


            foreach (var oldString in oldStrings)
            {
                var sameWords = 0;
                var oldCleanedSplit = CleanString(oldString).Split(" ");
                var longerString = oldCleanedSplit.Length >= newCleanedSplit.Length ? oldCleanedSplit : newCleanedSplit;
                var shorterString = longerString == oldCleanedSplit ? newCleanedSplit : oldCleanedSplit;

                foreach (var longerStringWord in longerString)
                {
                    foreach (var shorterStringWord in shorterString)
                    {
                        if (longerStringWord == shorterStringWord)
                            sameWords++;
                    }
                }

                sameWordCounts.Add(sameWords);
            }
            return sameWordCounts.Max() >= newCleanedSplit.Length * 0.925;
        }

        private string CleanString(string input)
        {
            return Regex.Replace(input, @",|\.|-|\s{2,}|•", "").ToLower();
        }
    }
}