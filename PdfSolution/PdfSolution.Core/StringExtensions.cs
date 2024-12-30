using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PdfSolution.Core
{
    internal static class StringExtensions
    {
        public static string[] SplitLines(this string text)
        {
            return text.Split(['\r', '\n'], StringSplitOptions.None);
        }

        public static string HtmlEncode(this string text)
        {
            return HttpUtility.HtmlEncode(text);
        }
    }
}
