using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PdfSolution.Core
{
    public class TestCaseXmlReference(int pageNumber, int lineIndex, int beginCharacterIndex, int endCharacterIndex, string referenceKey,string xPath) : TestCaseBase
    {
        public int PageNumber { get; private set; } = pageNumber;
        public int LineIndex { get; private set; } = lineIndex;
        public int BeginCharacterIndex { get; set; } = beginCharacterIndex;
        public int EndCharacterIndex { get; internal set; } = endCharacterIndex;
        public string ReferenceKey { get; private set; } = referenceKey;
        public string XPath { get; private set; } = xPath;

        public string? GetReferenceText(string testingFilePath, out string? referenceFilePath, out string? errorMessage)
        {
            string? result = null;
            referenceFilePath = null;
            errorMessage = null;

            if (TestDocumentsScript == null)
            {
                errorMessage = "TestDocumentsScript is null. Please verify TestDocumentsScript property.";
            }
            else
            {
                ReferenceDocument? reference = TestDocumentsScript.GetReferenceDocument(ReferenceKey);

                if (reference == null)
                {
                    errorMessage = $"Reference key '{ReferenceKey}' not found in References of TestDocumentsScript.json file.";
                }
                else if (reference.DocumentType == ReferenceDocument.DocumentTypes.Xml)
                {
                    referenceFilePath = reference.ResolveFilePath(testingFilePath);
                    XmlDocument? referenceDocument = TestDocumentsScript.GetReferenceXmlDocument(referenceFilePath);

                    if (referenceDocument == null)
                    {
                        errorMessage = $"Reference XML file not found ({referenceFilePath}).";
                    }
                    else
                    {
                        XmlNode? node = referenceDocument.SelectSingleNode(XPath);

                        if (node != null)
                        {
                            result = node.InnerText;
                        }
                    }
                }
                else
                {
                    errorMessage = $"DocumentType value of ReferenceDocument '{ReferenceKey}' must be 'Xml'.";
                }
            }

            return result;
        }

        public override TestCaseResult Test(PdfTextReader testingReader)
        {
            string? referenceText = null;
            string? referenceFilePath = null;
            string? actualText = null;
            bool testResult = false;
            string? errorMessage;

            try
            {
                referenceText = GetReferenceText(testingReader.FilePath, out referenceFilePath, out errorMessage);

                if (string.IsNullOrEmpty(errorMessage) && referenceText != null)
                {
                    actualText = testingReader.GetText(PageNumber, LineIndex, BeginCharacterIndex, EndCharacterIndex);
                    testResult = actualText.Equals(referenceText);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return new TestCaseResult(this, actualText, testResult, errorMessage)
            {
                ReferenceText = referenceText,
                ReferenceFilePath = referenceFilePath
            };
        }
    }
}
