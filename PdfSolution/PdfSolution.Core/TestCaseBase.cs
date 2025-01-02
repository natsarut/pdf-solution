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
    public class TestCaseBase(int pageNumber)
    {
        [JsonPropertyOrder(-1)]
        public int PageNumber { get; private set; } = pageNumber;
    }
}
