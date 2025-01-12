using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    [JsonDerivedType(typeof(TestCaseBase), nameof(TestCaseBase))]
    [JsonDerivedType(typeof(TestCaseEqual), nameof(TestCaseEqual))]
    [JsonDerivedType(typeof(TestCaseContain), nameof(TestCaseContain))]
    [JsonDerivedType(typeof(TestCaseContainInLine), nameof(TestCaseContainInLine))]
    [JsonDerivedType(typeof(TestCaseContainInPage), nameof(TestCaseContainInPage))]
    [JsonDerivedType(typeof(TestCasePdfReference), nameof(TestCasePdfReference))]
    public class TestCaseBase
    {
        public TestDocumentsScript? TestDocumentsScript { get; internal set; }

        public virtual TestCaseResult Test(PdfTextReader reader)
        {
            return new TestCaseResult(this, null, true);
        }
    }
}
