using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PdfSolution.Core
{
    public class TestDocumentsScript
    {
        private readonly Dictionary<string, PdfTextReader> _referencePdfTextReaders = [];
        private readonly Dictionary<string, XmlDocument> _referenceXmlDocuments = [];

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

        public PdfTextReader? GetReferencePdfTextReader(string filePath)
        {
            if (!_referencePdfTextReaders.TryGetValue(filePath, out PdfTextReader? result))
            {
                if (File.Exists(filePath))
                {
                    result = new PdfTextReader(filePath);
                    _referencePdfTextReaders.Add(filePath, result);
                }
            }

            return result;
        }

        public XmlDocument? GetReferenceXmlDocument(string filePath)
        {
            if (!_referenceXmlDocuments.TryGetValue(filePath, out XmlDocument? result))
            {
                if (File.Exists(filePath))
                {
                    result = new XmlDocument();
                    result.Load(filePath);
                    _referenceXmlDocuments.Add(filePath, result);
                }
            }

            return result;
        }
    }
}
