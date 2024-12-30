using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TestDocumentsConfiguration(IEnumerable<TestCaseBase> testCases)
    {
        public IEnumerable<TestCaseBase> TestCases { get; private set; } = testCases;
    }
}
