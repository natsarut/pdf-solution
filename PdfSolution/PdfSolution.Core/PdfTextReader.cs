using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Formats.Tar;
using System.Text;

namespace PdfSolution.Core
{
    public class PdfTextReader
    {
        private readonly PdfReader _pdfReader;
        private readonly PdfDocument _pdfDocument;
        private readonly List<TextPage> _pages;
        private readonly string _filePath;

        public string FilePath
        {
            get
            {
                return _filePath; 
            }
        }

        public PdfTextReader(string filePath)
        {
            _pdfReader = new PdfReader(filePath);
            _pdfDocument = new PdfDocument(_pdfReader);
            _pages = [];
            _filePath = filePath;
        }

        public int GetAllNumberOfPages()
        {
            return _pdfDocument.GetNumberOfPages();
        }

        public TextPage GetTextPage(int pageNumber)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1, nameof(pageNumber));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(pageNumber, _pdfDocument.GetNumberOfPages(), nameof(pageNumber));
            TextPage result;

            if (_pages.Any(x => x.PageNumber == pageNumber))
            {
                result = _pages.Where(x => x.PageNumber == pageNumber).First();
            }
            else
            {
                result = new TextPage(PdfTextExtractor.GetTextFromPage(_pdfDocument.GetPage(pageNumber)), pageNumber);
                _pages.Add(result);
            }

            return result;
        }

        public TextDocument GetDocumentText()
        {
            if (_pages.Count != _pdfDocument.GetNumberOfPages())
            {
                for (int pageNumber = 1; pageNumber <= _pdfDocument.GetNumberOfPages(); pageNumber++)
                {
                    if (!_pages.Any(x => x.PageNumber == pageNumber))
                    {
                        var page = new TextPage(PdfTextExtractor.GetTextFromPage(_pdfDocument.GetPage(pageNumber)), pageNumber);
                        _pages.Add(page);
                    }
                }
            }

            return new TextDocument(_pages);
        }

        public string GetText(int pageNumber, int lineIndex, int beginCharacterIndex, int endCharacterIndex)
        {
            TextPage textPage = GetTextPage(pageNumber);
            return textPage.GetText(lineIndex, beginCharacterIndex, endCharacterIndex);
        }

        public string[] ExtractLinesFromPdf()
        {
            var result = new List<string>();

            for (int i = 1; i <= _pdfDocument.GetNumberOfPages(); i++)
            {
                var lines = PdfTextExtractor.GetTextFromPage(_pdfDocument.GetPage(i)).SplitLines();
                result.AddRange(lines);
            }

            return [.. result];
        }
    }
}
