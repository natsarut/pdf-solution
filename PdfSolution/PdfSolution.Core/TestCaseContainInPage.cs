using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    /// <summary>
    /// A test case class to test whether text in a PDF file has expected values ​​in a given page.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="expectedText"></param>
    public class TestCaseContainInPage(int pageNumber, string expectedText):TestCaseBase
    {
        public int PageNumber { get; private set; } = pageNumber;
        public string ExpectedText { get; private set; } = expectedText;

        public override TestCaseResult Test(PdfTextReader reader)
        {
            bool testResult = false;
            string? errorMessage = null;

            try
            {
                TextPage textPage = reader.GetTextPage(PageNumber);
                testResult = textPage.Text.Contains(ExpectedText);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return new TestCaseResult(this, null, testResult, errorMessage);
        }
    }
}
