using System;
using System.Collections.Generic;
using DiscordNetPlus.Utils;

namespace DiscordNetPlus.Extentions
{
    public static class FundamentalExtentions
    {
        public static IEnumerable<string> DivideString(this string str, int maxChunkSize)
        {
            var chunks = new List<string>();
            for (var i = 0; i < str.Length; i += maxChunkSize)
            {
                chunks.Add(str.Substring(i, Math.Min(maxChunkSize, str.Length - i)));
            }

            return chunks;
        }

        public static IEnumerable<List<T>> SplitList<T>(this List<T> sourceList, int groupSize)
        {
            for (var i = 0; i < sourceList.Count; i += groupSize)
            {
                yield return sourceList.GetRange(i, Math.Min(groupSize, sourceList.Count - i));
            }
        }

        public static string ToFileSizeString(this long bytes)
        {
            return string.Format(new FileSizeFormatProvider(), "{0:fs}", bytes);
        }

        public static T RandomPick<T>(this List<T> sourceList)
        {
            var randomer = new Random();
            return sourceList[randomer.Next(sourceList.Count)];
        }
    }
}