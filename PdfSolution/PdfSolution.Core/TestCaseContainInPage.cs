using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TestCaseContainInPage(int pageNumber, string expectedText):TestCaseBase
    {
        public int PageNumber { get; private set; } = pageNumber;
        public string ExpectedText { get; private set; } = expectedText;
    }
}
