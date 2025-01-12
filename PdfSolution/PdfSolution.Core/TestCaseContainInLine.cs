using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TestCaseContainInLine(int pageNumber, int lineIndex, string expectedText):TestCaseBase
    {
        public int PageNumber { get; private set; } = pageNumber;
        public int LineIndex { get; private set; } = lineIndex;
        public string ExpectedText { get; private set; } = expectedText;

        public override TestCaseResult Test(PdfTextReader reader)
        {
            bool testResult = false;
            string? errorMessage = null;

            try
            {
                TextPage textPage = reader.GetTextPage(PageNumber);
                string line = textPage.GetLine(LineIndex);
                testResult = line.Contains(ExpectedText);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return new TestCaseResult(this, null, testResult, errorMessage);
        }
    }
}
