using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    /// <summary>
    /// A test case class to test whether text in a PDF file has expected values ​​in a given text range.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="lineIndex"></param>
    /// <param name="beginCharacterIndex"></param>
    /// <param name="endCharacterIndex"></param>
    /// <param name="expectedText"></param>
    public class TestCaseContain(int pageNumber, int lineIndex, int beginCharacterIndex, int endCharacterIndex, string expectedText):TestCaseBase
    {
        public int PageNumber { get; private set; } = pageNumber;
        public int LineIndex { get; private set; } = lineIndex;
        public int BeginCharacterIndex { get; set; } = beginCharacterIndex;
        public int EndCharacterIndex { get; internal set; } = endCharacterIndex;
        public string ExpectedText { get; private set; } = expectedText;

        public override TestCaseResult Test(PdfTextReader reader)
        {
            string? actualText = null;
            bool testResult = false;
            string? errorMessage = null;

            try
            {
                actualText = reader.GetText(PageNumber, LineIndex, BeginCharacterIndex, EndCharacterIndex);
                testResult = actualText.Contains(ExpectedText);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return new TestCaseResult(this, actualText, testResult, errorMessage);
        }
    }
}
