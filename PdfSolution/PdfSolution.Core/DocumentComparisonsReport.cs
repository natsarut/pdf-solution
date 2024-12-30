using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class DocumentComparisonsReport(IEnumerable<DocumentComparisonResult> comparisonResults,string directory1,string directory2)
    {
        private const double hundred = 100d;

        public IEnumerable<DocumentComparisonResult> ComparisonResults { get; private set; } = comparisonResults;
        public string Directory1 { get; private set; } = directory1;
        public string Directory2 { get; private set; } = directory2;

        public int TotalMatchingFiles
        {
            get
            {
                return ComparisonResults.Count();
            }
        }

        public int EqualFiles
        {
            get
            {
                return ComparisonResults.Count(x => x.SummaryResult);
            }
        }

        public int DifferentFiles
        {
            get
            {
                return ComparisonResults.Count(x => !x.SummaryResult);
            }
        }

        public double EqualPercentage
        {
            get
            {
                return EqualFiles * hundred / TotalMatchingFiles;
            }
        }

        public double DifferentPercentage
        {
            get
            {
                return hundred - EqualPercentage;
            }
        }
    }
}
