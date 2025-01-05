using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TestCaseContainInLine(int pageNumber, int lineIndex, string expectedText) : TestCaseBase(pageNumber)
    {
        public int LineIndex { get; private set; } = lineIndex;
        public string ExpectedText { get; private set; } = expectedText;
    }
}
