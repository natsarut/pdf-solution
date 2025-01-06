using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class ReferenceDocument(ReferenceDocument.DocumentTypes documentType,string filePath)
    {
        public enum DocumentTypes
        {
            Pdf
        }

        public DocumentTypes DocumentType { get; private set; } = documentType;
        public string FilePath { get; private set; } = filePath;
    }
}
