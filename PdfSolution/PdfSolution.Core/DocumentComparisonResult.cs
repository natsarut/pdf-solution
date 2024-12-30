using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class DocumentComparisonResult(string htmlContent,int mismatchLines,double equalPercentage)
    {
        public string HtmlContent { get; private set; } = htmlContent;
        public int MismatchLines { get; private set; } = mismatchLines;
        public double EqualPercentage { get; private set; } = equalPercentage;
        public string ComparisonFileName { get; set; } = string.Empty;
        public string ResultFileName { get; set; } = string.Empty;

        public bool SummaryResult
        {
            get
            {
                return MismatchLines == 0;
            }
        }
    }
}
