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

        /// <summary>
        /// Reference: https://jrgraphix.net/r/Unicode/0E00-0E7F
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CorrectThaiChars(this string text)
        {
            string result = text;
            result = result.Replace("ำา", "ำ");
            result = result.Replace("ำำ", "ำ");
            result = result.Replace("รำย", "ราย");
            result = result.Replace("เอำ", "เอา");
            result = result.Replace("มำย", "มาย");
            result = result.Replace("อ้ำ", "อ้า");
            result = result.Replace("ภำ", "ภา");
            result = result.Replace("ตำม", "ตาม");
            result = result.Replace("เวลำ", "เวลา");
            result = result.Replace("กำรชำ", "การชำ");
            result = result.Replace("กำรใช้", "การใช้");
            result = result.Replace("ธรรมดำ", "ธรรมดา");
            return result;
        }
    }
}
