using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TestCaseResult(TestCaseBase testCase,string? actualText, bool testResult,string? errorMessage=null)
    {
        public TestCaseBase TestCase { get; private set; } = testCase;
        public string? ActualText { get; private set; } = actualText;
        public bool TestResult { get; private set; } = testResult;
        public string? ErrorMessage { get; private set; } = errorMessage;

        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorMessage);
            }
        }
    }
}
