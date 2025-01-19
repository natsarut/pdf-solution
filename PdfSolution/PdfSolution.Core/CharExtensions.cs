using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    internal static class CharExtensions
    {
        public static string GetEscapeSequence(this char c)
        {
            return $"\\u{(int)c:X4}";
        }
    }
}
