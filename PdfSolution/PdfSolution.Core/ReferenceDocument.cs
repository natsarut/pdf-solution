using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class ReferenceDocument(ReferenceDocument.DocumentTypes documentTypes,string filePath)
    {
        public enum DocumentTypes
        {
            Pdf
        }

        public DocumentTypes DocumentType { get; private set; } = documentTypes;
        public string FilePath { get; private set; } = filePath;
    }
}
