using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TestDocumentsReport(IEnumerable<TestDocumentResult> testDocumentResults,string directoryUnderTest)
    {
        private const double hundred = 100d;

        public IEnumerable<TestDocumentResult> TestDocumentResults { get; private set; } = testDocumentResults;
        public string DirectoryUnderTest { get; private set; } = directoryUnderTest;

        public int TotalTests
        {
            get
            {
                return TestDocumentResults.Count();
            }
        }

        public int PassTests
        {
            get
            {
                return TestDocumentResults.Count(x => x.SummaryTestResult);
            }
        }

        public int FailedTests
        {
            get
            {
                return TestDocumentResults.Count(x => !x.SummaryTestResult);
            }
        }

        public double PassPercentage
        {
            get
            {
                return PassTests * hundred / TotalTests;
            }
        }

        public double FailedPercentage
        {
            get
            {
                return hundred - PassPercentage;
            }
        }
    }
}
