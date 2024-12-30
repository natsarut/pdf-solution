using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TestDocumentResult(IEnumerable<TestCaseResult> testCaseResults,string fileNameUnderTest)
    {
        public IEnumerable<TestCaseResult> TestCaseResults { get; private set; } = testCaseResults;
        public string FileNameUnderTest { get; private set; } = fileNameUnderTest;

        public bool SummaryTestResult
        {
            get
            {
                return !TestCaseResults.Any(x => x.TestResult == false);
            }
        }
    }
}
