using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    /// <summary>
    /// A test case class to compare page regions of two documents as text.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="lineIndex"></param>
    /// <param name="beginCharacterIndex"></param>
    /// <param name="endCharacterIndex"></param>
    /// <param name="referenceKey"></param>
    public class TestCasePdfReference(int pageNumber, int lineIndex, int beginCharacterIndex, int endCharacterIndex, string referenceKey) : TestCaseBase
    {
        public int PageNumber { get; private set; } = pageNumber;
        public int LineIndex { get; private set; } = lineIndex;
        public int BeginCharacterIndex { get; set; } = beginCharacterIndex;
        public int EndCharacterIndex { get; internal set; } = endCharacterIndex;
        public string ReferenceKey { get; private set; } = referenceKey;

        public string? GetReferenceText(string testingFilePath,out string? referenceFilePath, out string? errorMessage)
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
                else if (reference.DocumentType == ReferenceDocument.DocumentTypes.Pdf)
                {
                    referenceFilePath = reference.ResolveFilePath(testingFilePath);
                    PdfTextReader? referenceReader = TestDocumentsScript.GetReferencePdfTextReader(referenceFilePath);

                    if (referenceReader == null)
                    {
                        errorMessage = $"Reference PDF file not found ({referenceFilePath}).";
                    }
                    else
                    {
                        result = referenceReader.GetText(PageNumber, LineIndex, BeginCharacterIndex, EndCharacterIndex);
                    }
                }
                else
                {
                    errorMessage = $"DocumentType value of ReferenceDocument '{ReferenceKey}' must be 'Pdf'.";
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
                referenceText = GetReferenceText(testingReader.FilePath,out referenceFilePath, out errorMessage);

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
                ReferenceFilePath= referenceFilePath
            };
        }
    }
}
