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
            Pdf,
            Xml
        }

        public DocumentTypes DocumentType { get; private set; } = documentType;
        public string FilePath { get; private set; } = filePath;

        public string ResolveFilePath(string testingFilePath)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(testingFilePath);
            return FilePath.Replace("{FileName}", fileNameWithoutExt);
        }
    }
}
