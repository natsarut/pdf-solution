using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TestDocumentsScript
    {         
        public IEnumerable<TestCaseBase> TestCases { get; private set; }
        public Dictionary<string, ReferenceDocument>? References { get; set; }

        public TestDocumentsScript(IEnumerable<TestCaseBase> testCases)
        {
            foreach (TestCaseBase testCase in testCases)
            {
                testCase.TestDocumentsScript = this;
            }

            TestCases = testCases;
        }

        public ReferenceDocument? GetReferenceDocument(string key)
        {
            ReferenceDocument? result = null;
            References?.TryGetValue(key, out result);
            return result;
        }
    }
}
