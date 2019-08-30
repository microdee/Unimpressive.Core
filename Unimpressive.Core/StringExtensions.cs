using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Unimpressive.Core
{
    /// <summary>
    /// Data class for string editing inserts
    /// </summary>
    public struct EditInsert
    {
        /// <summary></summary>
        public int Position;
        /// <summary></summary>
        public int Length;
        /// <summary></summary>
        public string InsertText;
    }

    /// <summary>
    /// Data class for string lines
    /// </summary>
    public struct LineRange
    {
        /// <summary></summary>
        public int Start;
        /// <summary></summary>
        public int End;
        /// <summary></summary>
        public int Length;
    }

    /// <summary>
    /// Useful extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Version of split where you can specify unsplittable blocks with arbitrary delimiter
        /// </summary>
        /// <param name="input"></param>
        /// <param name="separator"></param>
        /// <param name="ignorebetween"></param>
        /// <returns></returns>
        public static string[] SplitIgnoringBetween(this string input, string separator, string ignorebetween)
        {
            return input.Split(ignorebetween.ToCharArray())
                .Select((element, index) => index % 2 == 0  // If even index
                    ? element.Split(separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)  // Split the item
                    : new[] { ignorebetween + element + ignorebetween })  // Keep the entire item
                .SelectMany(element => element).ToArray();
        }

        /// <summary>
        /// Where applicable remove modifying diacritics from characters
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string text)
        {
            return string.Concat(
                text.Normalize(NormalizationForm.FormD)
                    .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                 UnicodeCategory.NonSpacingMark)
            ).Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Determine which line a character is included inside
        /// </summary>
        /// <param name="input"></param>
        /// <param name="charid">The character position inside the string in question</param>
        /// <returns>Line data where the character is sitting</returns>
        public static LineRange LineRangeFromCharIndex(this string input, int charid)
        {
            int linestart = 0;
            int lineend = 0;
            while (true)
            {
                lineend = input.IndexOfAny(new[] { '\r', '\n' }, lineend) + 1;
                if (charid >= linestart && charid < lineend) break;
                linestart = lineend;
            }
            var linelength = lineend - linestart;
            //if (lineend < input.Length) linelength++;
            return new LineRange
            {
                End = lineend,
                Start = linestart,
                Length = linelength
            };
        }

        /// <summary>
        /// Edit the string from a list of ranges and replacements
        /// </summary>
        /// <param name="input"></param>
        /// <param name="edits">List of ranges where the replacements should happen</param>
        /// <returns></returns>
        public static string MultiEdit(this string input, params EditInsert[] edits)
        {
            int offs = 0;
            string res = input;
            foreach (var edit in edits)
            {
                var diff = edit.InsertText.Length - edit.Length;
                var pos = edit.Position + offs;
                res = res.Remove(pos, edit.Length);
                res = res.Insert(pos, edit.InsertText);
                offs += diff;
            }
            return res;
        }

        /// <summary>
        /// Simple hash of a string represented in HEX
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string HashSha256_16(this string text)
        {
            var bytes = Encoding.Unicode.GetBytes(text);
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);
            return hash.Aggregate(string.Empty, (current, x) => current + $"{x:x2}");
        }

        /// <summary>
        /// Simple hash of a string represented in Base64
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string HashSha256_64(this string text)
        {
            var bytes = Encoding.Unicode.GetBytes(text);
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Shortcut to Contains(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool ContainsCaseless(this string text, string subtext)
        {
            return text.Contains(subtext, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Shortcut to Contains(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool ContainsCaselessAny(this string text, params string[] subtext)
        {
            return subtext.Any(text.ContainsCaseless);
        }

        /// <summary>
        /// Shortcut to Contains(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool ContainsCaselessAll(this string text, params string[] subtext)
        {
            return subtext.All(text.ContainsCaseless);
        }

        /// <summary>
        /// Shortcut to Equals(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool EqualsCaseless(this string text, string subtext)
        {
            return text.Equals(subtext, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Shortcut to Equals(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool EqualsCaselessAny(this string text, params string[] subtext)
        {
            return subtext.Any(text.EqualsCaseless);
        }

        /// <summary>
        /// Shortcut to Equals(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool EqualsCaselessAll(this string text, params string[] subtext)
        {
            return subtext.All(text.EqualsCaseless);
        }

        /// <summary>
        /// Shortcut to StartsWith(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool StartsWithCaseless(this string text, string subtext)
        {
            return text.StartsWith(subtext, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Shortcut to StartsWith(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool StartsWithCaselessAny(this string text, params string[] subtext)
        {
            return subtext.Any(text.StartsWithCaseless);
        }

        /// <summary>
        /// Shortcut to StartsWith(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool StartsWithCaselessAll(this string text, params string[] subtext)
        {
            return subtext.All(text.StartsWithCaseless);
        }

        /// <summary>
        /// Shortcut to EndsWith(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool EndsWithCaseless(this string text, string subtext)
        {
            return text.EndsWith(subtext, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Shortcut to EndsWith(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool EndsWithCaselessAny(this string text, params string[] subtext)
        {
            return subtext.Any(text.EndsWithCaseless);
        }

        /// <summary>
        /// Shortcut to EndsWith(subtext, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subtext"></param>
        /// <returns></returns>
        public static bool EndsWithCaselessAll(this string text, params string[] subtext)
        {
            return subtext.All(text.EndsWithCaseless);
        }

        /// <summary>
        /// Shortcut to regex matching groups
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns>Group collection which you can use to retrieve named captures</returns>
        /// <remarks>
        /// <code>
        /// string capture = myinput.MatchGroup(".*?")["mycapture"]
        /// </code>
        /// </remarks>
        public static GroupCollection MatchGroup(
            this string text,
            string pattern,
            RegexOptions options = RegexOptions.CultureInvariant | RegexOptions.Multiline)
        {
            var match = Regex.Match(text, pattern, options);
            return match.Groups;
        }

        /// <summary>
        /// Fluid pattern for retrieving named captures from a regex GroupCollection
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="capturename"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <remarks>
        /// <code>
        /// myinput.MatchGroup(".*?")
        ///     .Fetch("firstcap", out var first)
        ///     .Fetch("secondcap", out var second)
        ///     ...
        /// </code>
        /// </remarks>
        public static GroupCollection Fetch(this GroupCollection groups, string capturename, out string result)
        {
            result = groups[capturename].Value;
            return groups;
        }
    }
}
