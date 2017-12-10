using System.Text.RegularExpressions;

namespace Closet.Web.Infrastructure.Extesions
{
    public static class StringExtensions
    {
        public static string ToFriendlyUrl(this string text)
         => Regex.Replace(text, @"[^A-Za-z0-9_\.~]+", "-").ToLower();
    }
}
