using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TextDocument(IEnumerable<TextPage> textPages)
    {
        public IEnumerable<TextPage> TextPages 
        { 
            get
            {
                return textPages.OrderBy(x => x.PageNumber);
            }
        }
    }
}
