using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TextPage(string text,int pageNumber)
    {
        private IEnumerable<string>? _textLines;

        public string Text 
        { 
            get
            {
                return text;
            }
        }

        public int PageNumber
        { 
            get 
            { 
                return pageNumber; 
            } 
        }

        public IEnumerable<string> TextLines 
        { 
            get
            {
                _textLines ??= text.SplitLines();
                return _textLines;
            }
        }

        public int GetMaxCharacterOfLines()
        {
            return TextLines.Select(x => x.Length).Max();
        }

        public string GetLine(int lineIndex)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(lineIndex, 0, nameof(lineIndex));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(lineIndex, TextLines.Count(), nameof(lineIndex));
            return TextLines.ElementAt(lineIndex);
        }

        public string GetText(int lineIndex, int beginCharacterIndex, int endCharacterIndex)
        {
            string textLine = GetLine(lineIndex);
            int maxCharacterOfLines = GetMaxCharacterOfLines();
            return textLine.PadRight(maxCharacterOfLines, ' ').Substring(beginCharacterIndex, endCharacterIndex - beginCharacterIndex + 1).Trim();
        }
    }
}
