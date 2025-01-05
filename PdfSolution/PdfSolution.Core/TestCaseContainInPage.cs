using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TestCaseContainInPage(int pageNumber, string expectedText) : TestCaseBase(pageNumber)
    {
        public string ExpectedText { get; private set; } = expectedText;
    }
}
