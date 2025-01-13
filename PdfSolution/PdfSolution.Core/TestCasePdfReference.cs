using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TestCasePdfReference(int pageNumber, int lineIndex, int beginCharacterIndex, int endCharacterIndex, string referenceKey) : TestCaseBase
    {
        public int PageNumber { get; private set; } = pageNumber;
        public int LineIndex { get; private set; } = lineIndex;
        public int BeginCharacterIndex { get; set; } = beginCharacterIndex;
        public int EndCharacterIndex { get; internal set; } = endCharacterIndex;
        public string ReferenceKey { get; private set; } = referenceKey;

        public override TestCaseResult Test(PdfTextReader testingReader)
        {
            string? actualText = null;
            bool testResult = false;
            string? errorMessage = null;

            try
            {
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
                        string referenceFilePath = reference.ResolveFilePath(testingReader.FilePath);
                        PdfTextReader? referenceReader = TestDocumentsScript.GetReferencePdfTextReader(referenceFilePath);

                        if (referenceReader == null)
                        {
                            errorMessage = $"Reference PDF file not found ({referenceFilePath}).";
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        errorMessage = $"DocumentType value of ReferenceDocument '{ReferenceKey}' must be 'Pdf'.";
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return new TestCaseResult(this, actualText, testResult, errorMessage);
        }
    }
}
