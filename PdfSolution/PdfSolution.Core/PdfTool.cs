using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public static class PdfTool
    {
        public enum ComparisonTypes
        {
            CompareBySide,
            CompareByPage
        }

        private const string pdfSearchPattern = "*.pdf";
        private const string textSuccess = "text-success";
        private const string textError = "text-danger";
        private const string iconSuccess = "<i class=\"bi bi-check-circle-fill\"></i>";
        private const string iconError = "<i class=\"bi bi-x-circle-fill\"></i>";
        private const string badgeSuccess = "Pass";
        private const string badgeError = "Fail";
        private const string testDocumentsScriptFileName = "TestDocumentsScript.json";
        private const string outputDir = "output";
        private const double hundred = 100d;

        private static string WriteOutput(string outputFileName, string html)
        {
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            string result = Path.Combine(outputDir, outputFileName);
            File.WriteAllText(result, html, Encoding.UTF8);
            return result;
        }

        public static string GenerateTextTableHtml(string filePath,IEnumerable<int> pageNumbers, string? outputFileName = null)
        {
            string a=string.Join(string.Empty, ['\u0E08', '\u0E33', '\u0E32']);
            var pdfTextReader = new PdfTextReader(filePath);
            pageNumbers = [.. pageNumbers.Order()];
            var bodyContent = new StringBuilder("<nav id=\"PageIndex\" aria-label=\"...\"><ul class=\"pagination pagination-sm\">");
            bodyContent.Append(string.Join(string.Empty, pageNumbers.Select(x => $"<li class=\"page-item\"><a class=\"page-link\" href=\"#Page{x}\">{x}</a></li>")));
            bodyContent.Append($"</ul></nav>{a}");

            foreach (int pageNumber in pageNumbers)
            {
                TextPage textPage = pdfTextReader.GetTextPage(pageNumber);
                int maxCharacterOfLines = textPage.GetMaxCharacterOfLines();
                bodyContent.Append($"<table id=\"Page{pageNumber}\" class=\"table table-bordered caption-top\"><caption>Page {pageNumber}</caption><tr><th>Line/Character</th>");

                for (int i = 0; i < maxCharacterOfLines; i++)
                {
                    bodyContent.Append($"<th>{i}</th>");
                }

                bodyContent.Append("</tr>");

                for (int lineNumber = 0; lineNumber < textPage.TextLines.Count(); lineNumber++)
                {
                    string textLine = textPage.TextLines.ElementAt(lineNumber);
                    bodyContent.Append($"<tr><th>{lineNumber}</th>");

                    for (int characterNumber = 0; characterNumber < maxCharacterOfLines; characterNumber++)
                    {
                        char character = characterNumber < textLine.Length ? textLine[characterNumber] : '\0';
                        bodyContent.Append($"<td title=\"{lineNumber}, {characterNumber}\">{character.ToString().HtmlEncode()}</td>");
                    }

                    bodyContent.Append("</tr>");
                }

                bodyContent.Append("</table><p><a href=\"#PageIndex\">Go to Page Index</a></p><hr />");
            }

            var result = ResourceHelper.GetLayoutHtml();
            result = result.Replace("{PageTitle}", "Text Table");
            result = result.Replace("{BodyContent}", bodyContent.ToString());

            if (!string.IsNullOrWhiteSpace(outputFileName))
            {
                WriteOutput(outputFileName, result);
            }

            return result;
        }

        public static string GenerateTextTableHtml(string filePath, string? outputFileName = null)
        {
            var pdfTextReader = new PdfTextReader(filePath);
            var pageNumbers = Enumerable.Range(1, pdfTextReader.GetAllNumberOfPages());
            return GenerateTextTableHtml(filePath, pageNumbers,outputFileName);
        }

        public static TestDocumentResult TestDocument(string filePath,IEnumerable<TestCaseBase> testCases)
        {
            var testCaseResults = new List<TestCaseResult>();
            var pdfTextReader = new PdfTextReader(filePath);

            foreach (TestCaseBase testCase in testCases)
            {
                TestCaseResult testCaseResult = testCase.Test(pdfTextReader);
                testCaseResults.Add(testCaseResult);
            }

            return new TestDocumentResult(testCaseResults, filePath);
        }

        public static TestDocumentsReport TestDocuments(string dir, TestDocumentsScript testDocumentsScript, string? outputFileName = null)
        {
            var testDocumentResults = new List<TestDocumentResult>();
            string[] files = Directory.GetFiles(dir, pdfSearchPattern);

            foreach (string file in files)
            {
                TestDocumentResult testDocumentResult = TestDocument(file, testDocumentsScript.TestCases);
                testDocumentResults.Add(testDocumentResult);
            }

            var result = new TestDocumentsReport(testDocumentResults, dir);

            if (!string.IsNullOrWhiteSpace(outputFileName))
            {
                string html = GenerateTestDocumentsReportHtml(result);
                WriteOutput(outputFileName, html);
            }

            return result;
        }

        public static TestDocumentsReport TestDocuments(string dir, string? outputFileName = null)
        {
            string filePath = Path.Combine(dir, testDocumentsScriptFileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"ไม่พบไฟล์ '{testDocumentsScriptFileName}' ในโฟลเดอร์ '{dir}'", filePath);
            }

            string json = File.ReadAllText(filePath, Encoding.UTF8);
            TestDocumentsScript? configuration = JsonHelper.Deserialize<TestDocumentsScript>(json);

            return configuration == null ? throw new NullReferenceException($"{nameof(TestDocumentsScript)} เป็น Null จากไฟล์ '{filePath}'") : TestDocuments(dir, configuration, outputFileName);
        }

        private static void AppendTestCaseResult(TestCaseResult testCaseResult,ref StringBuilder content)
        {
            if (testCaseResult.HasError)
            {
                content.Append($"<li><strong class=\"{textError}\" title=\"{badgeError}\">{iconError}</strong> Error message: {testCaseResult.ErrorMessage?.HtmlEncode()}</li>");
            }
            else
            {
                if (testCaseResult.TestCase is TestCaseEqual testCaseEqual)
                {
                    if (testCaseResult.TestResult)
                    {
                        content.Append($"<li><strong class=\"{textSuccess}\" title=\"{badgeSuccess}\">{iconSuccess}</strong> The actual text <strong>\"{testCaseResult.ActualText?.HtmlEncode()}\"</strong> equals the expected text <strong>\"{testCaseEqual.ExpectedText.HtmlEncode()}\"</strong> in position <strong>({testCaseEqual.PageNumber}, {testCaseEqual.LineIndex}, {testCaseEqual.BeginCharacterIndex}, {testCaseEqual.EndCharacterIndex})</strong>.</li>");
                    }
                    else
                    {
                        content.Append($"<li><strong class=\"{textError}\" title=\"{badgeError}\">{iconError}</strong> The actual text <strong>\"{testCaseResult.ActualText?.HtmlEncode()}\"</strong> is not equal to the expected text <strong>\"{testCaseEqual.ExpectedText.HtmlEncode()}\"</strong> in position <strong>({testCaseEqual.PageNumber}, {testCaseEqual.LineIndex}, {testCaseEqual.BeginCharacterIndex}, {testCaseEqual.EndCharacterIndex})</strong>.</li>");
                    }
                }
                else if (testCaseResult.TestCase is TestCaseContain testCaseContain)
                {
                    if (testCaseResult.TestResult)
                    {
                        content.Append($"<li><strong class=\"{textSuccess}\" title=\"{badgeSuccess}\">{iconSuccess}</strong> The actual text <strong>\"{testCaseResult.ActualText?.HtmlEncode()}\"</strong> contains the expected text <strong>\"{testCaseContain.ExpectedText.HtmlEncode()}\"</strong> in position <strong>({testCaseContain.PageNumber}, {testCaseContain.LineIndex}, {testCaseContain.BeginCharacterIndex}, {testCaseContain.EndCharacterIndex})</strong>.</li>");
                    }
                    else
                    {
                        content.Append($"<li><strong class=\"{textError}\" title=\"{badgeError}\">{iconError}</strong> The actual text <strong>\"{testCaseResult.ActualText?.HtmlEncode()}\"</strong> is not contain to the expected text <strong>\"{testCaseContain.ExpectedText.HtmlEncode()}\"</strong> in position <strong>({testCaseContain.PageNumber}, {testCaseContain.LineIndex}, {testCaseContain.BeginCharacterIndex}, {testCaseContain.EndCharacterIndex})</strong>.</li>");
                    }
                }
                else if (testCaseResult.TestCase is TestCaseContainInLine testCaseContainInLine)
                {
                    if (testCaseResult.TestResult)
                    {
                        content.Append($"<li><strong class=\"{textSuccess}\" title=\"{badgeSuccess}\">{iconSuccess}</strong> The text line <strong>{testCaseContainInLine.LineIndex}</strong> of page <strong>{testCaseContainInLine.PageNumber}</strong> contains the expected text <strong>\"{testCaseContainInLine.ExpectedText.HtmlEncode()}\"</strong>.</li>");
                    }
                    else
                    {
                        content.Append($"<li><strong class=\"{textError}\" title=\"{badgeError}\">{iconError}</strong> The text line <strong>{testCaseContainInLine.LineIndex}</strong> of page <strong>{testCaseContainInLine.PageNumber}</strong> is not contain the expected text <strong>\"{testCaseContainInLine.ExpectedText.HtmlEncode()}\"</strong>.</li>");
                    }
                }
                else if (testCaseResult.TestCase is TestCaseContainInPage testCaseContainInPage)
                {
                    if (testCaseResult.TestResult)
                    {
                        content.Append($"<li><strong class=\"{textSuccess}\" title=\"{badgeSuccess}\">{iconSuccess}</strong> The page <strong>{testCaseContainInPage.PageNumber}</strong> contains the expected text <strong>\"{testCaseContainInPage.ExpectedText.HtmlEncode()}\"</strong>.</li>");
                    }
                    else
                    {
                        content.Append($"<li><strong class=\"{textError}\" title=\"{badgeError}\">{iconError}</strong> The page <strong>{testCaseContainInPage.PageNumber}</strong> is not contain the expected text <strong>\"{testCaseContainInPage.ExpectedText.HtmlEncode()}\"</strong>.</li>");
                    }
                }
                else if (testCaseResult.TestCase is TestCasePdfReference testCasePdfReference)
                {
                    if (testCaseResult.TestResult)
                    {
                        content.Append($"<li><strong class=\"{textSuccess}\" title=\"{badgeSuccess}\">{iconSuccess}</strong> The actual text <strong>\"{testCaseResult.ActualText?.HtmlEncode()}\"</strong> equals the reference text <strong>\"{testCaseResult.ReferenceText?.HtmlEncode()}\"</strong> in file <strong>\"{testCaseResult.ReferenceFilePath?.HtmlEncode()}\"</strong> for position <strong>({testCasePdfReference.PageNumber}, {testCasePdfReference.LineIndex}, {testCasePdfReference.BeginCharacterIndex}, {testCasePdfReference.EndCharacterIndex})</strong>.</li>");
                    }
                    else
                    {
                        content.Append($"<li><strong class=\"{textError}\" title=\"{badgeError}\">{iconError}</strong> The actual text <strong>\"{testCaseResult.ActualText?.HtmlEncode()}\"</strong> is not equal to the reference text <strong>\"{testCaseResult.ReferenceText?.HtmlEncode()}\"</strong> in file <strong>\"{testCaseResult.ReferenceFilePath?.HtmlEncode()}\"</strong> for position <strong>({testCasePdfReference.PageNumber}, {testCasePdfReference.LineIndex}, {testCasePdfReference.BeginCharacterIndex}, {testCasePdfReference.EndCharacterIndex})</strong>.</li>");
                    }
                }
                else if (testCaseResult.TestCase is TestCaseXmlReference testCaseXmlReference)
                {
                    if (testCaseResult.TestResult)
                    {
                        content.Append($"<li><strong class=\"{textSuccess}\" title=\"{badgeSuccess}\">{iconSuccess}</strong> The actual text <strong>\"{testCaseResult.ActualText?.HtmlEncode()}\"</strong> {(testCaseXmlReference.Operator==TestCaseXmlReference.Operators.Equal? "equals" : "contains")} the reference text <strong>\"{testCaseResult.ReferenceText?.HtmlEncode()}\"</strong> in XML file <strong>\"{testCaseResult.ReferenceFilePath?.HtmlEncode()}\"</strong> for position <strong>({testCaseXmlReference.PageNumber}, {testCaseXmlReference.LineIndex}, {testCaseXmlReference.BeginCharacterIndex}, {testCaseXmlReference.EndCharacterIndex})</strong>.</li>");
                    }
                    else
                    {
                        content.Append($"<li><strong class=\"{textError}\" title=\"{badgeError}\">{iconError}</strong> The actual text <strong>\"{testCaseResult.ActualText?.HtmlEncode()}\"</strong> is not {(testCaseXmlReference.Operator == TestCaseXmlReference.Operators.Equal ? "equal" : "contain")} to the reference text <strong>\"{testCaseResult.ReferenceText?.HtmlEncode()}\"</strong> in XML file <strong>\"{testCaseResult.ReferenceFilePath?.HtmlEncode()}\"</strong> for position <strong>({testCaseXmlReference.PageNumber}, {testCaseXmlReference.LineIndex}, {testCaseXmlReference.BeginCharacterIndex}, {testCaseXmlReference.EndCharacterIndex})</strong>.</li>");
                    }
                }
            }
        }

        public static string GenerateTestDocumentsReportHtml(TestDocumentsReport testDocumentsReport)
        {
            var bodyContent = new StringBuilder("<h2>Summary</h2>");
            bodyContent.Append("<div class=\"container text-center\">");
            bodyContent.Append("<div class=\"row\"><div class=\"col-7\">Directory under test</div><div class=\"col\">Total tests</div><div class=\"col\">Failed tests</div><div class=\"col\">Pass percentage</div></div>");
            bodyContent.Append("<div class=\"row\">");
            bodyContent.Append($"<div class=\"col-7\"><h3>{testDocumentsReport.DirectoryUnderTest.HtmlEncode()}</h3></div>");
            bodyContent.Append($"<div class=\"col\"><h3>{testDocumentsReport.TotalTests}</h3></div>");
            bodyContent.Append($"<div class=\"col\"><h3>{testDocumentsReport.FailedTests}</h3></div>");
            bodyContent.Append("<div class=\"col\">");
            bodyContent.Append($"<h3>{testDocumentsReport.PassPercentage:n}%</h3>");
            bodyContent.Append("<div class=\"progress-stacked\">");
            bodyContent.Append($"<div class=\"progress\" role=\"progressbar\" aria-label=\"Segment pass\" aria-valuenow=\"{testDocumentsReport.PassPercentage}\" aria-valuemin=\"0\" aria-valuemax=\"{hundred}\" style=\"width: {testDocumentsReport.PassPercentage}%\"><div class=\"progress-bar bg-success\"></div></div>");
            bodyContent.Append($"<div class=\"progress\" role=\"progressbar\" aria-label=\"Segment failed\" aria-valuenow=\"{testDocumentsReport.FailedPercentage}\" aria-valuemin=\"0\" aria-valuemax=\"{hundred}\" style=\"width: {testDocumentsReport.FailedPercentage}%\"><div class=\"progress-bar bg-danger\"></div></div>");
            bodyContent.Append("</div></div></div></div>");
            bodyContent.Append("<h2>Test Results</h2>");
            bodyContent.Append("<div class=\"accordion\" id=\"accordionTestResults\">");

            for (int i=0; i< testDocumentsReport.TestDocumentResults.Count(); i++)
            {
                TestDocumentResult testDocumentResult = testDocumentsReport.TestDocumentResults.ElementAt(i);
                string textColor;
                string icon;
                string badge;

                if (testDocumentResult.SummaryTestResult)
                {
                    textColor = textSuccess;
                    icon = iconSuccess;
                    badge = badgeSuccess;
                }
                else
                {
                    textColor = textError;
                    icon = iconError;
                    badge = badgeError;
                }

                bodyContent.Append("<div class=\"accordion-item\">");
                bodyContent.Append("<h2 class=\"accordion-header\">");
                bodyContent.Append($"<button class=\"accordion-button\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#collapse{i}\" aria-expanded=\"true\" aria-controls=\"collapse{i}\">");
                bodyContent.Append($"<h5 class=\"{textColor}\" title=\"{badge}\">{icon} {Path.GetFileName(testDocumentResult.FileNameUnderTest.HtmlEncode())}</h5>");
                bodyContent.Append("</button></h2>");
                bodyContent.Append($"<div id=\"collapse{i}\" class=\"accordion-collapse collapse\">");
                bodyContent.Append("<div class=\"accordion-body\">");
                bodyContent.Append("<ul class=\"list-unstyled\">");
                
                foreach (TestCaseResult testCaseResult in testDocumentResult.TestCaseResults)
                {
                    AppendTestCaseResult(testCaseResult, ref bodyContent);
                }

                bodyContent.Append("</ul></div></div></div>");
            }

            bodyContent.Append("</div>");
            string result = ResourceHelper.GetLayoutHtml();
            result = result.Replace("{PageTitle}", "Test Documents Report");
            result = result.Replace("{BodyContent}", bodyContent.ToString());
            return result;
        }

        private static string HighlightDifferences(string[] words1, string[] words2)
        {
            return string.Join(" ", words1.Select(word => words2.Contains(word) ? word : $"<span class=\"text-bg-warning\">{word.HtmlEncode()}</span>"));
        }

        public static DocumentComparisonResult ComparePdfBySide(string filePath1, string filePath2, string? outputFileName = null)
        {
            var reader1 = new PdfTextReader(filePath1);
            var reader2 = new PdfTextReader(filePath2);
            string[] lines1 = reader1.ExtractLinesFromPdf();
            string[] lines2 = reader2.ExtractLinesFromPdf();
            int linesCount1 = lines1.Length;
            int linesCount2 = lines2.Length;
            int maxLines = Math.Max(linesCount1, linesCount2);
            int mismatchLines = 0;
            var tableContent = new StringBuilder("<table class=\"table table-bordered caption-top\"><caption>Comparison Result</caption>");
            tableContent.Append($"<tr class=\"table-primary\"><th>Line</th><th>{filePath1.HtmlEncode()}</th><th>{filePath2.HtmlEncode()}</th></tr>");

            for (int i = 0; i < maxLines; i++)
            {
                string line1 = i < linesCount1 ? lines1[i] : string.Empty;
                string line2 = i < linesCount2 ? lines2[i] : string.Empty;
                string textColor;
                string icon;
                string badge;

                if (line1 == line2)
                {
                    textColor = textSuccess;
                    icon = iconSuccess;
                    badge = "Equal";
                }
                else
                {
                    textColor = textError;
                    icon = iconError;
                    badge = "Mismatch";
                    mismatchLines++;
                }

                string[] words1 = line1.Split(' ');
                string[] words2 = line2.Split(' ');

                tableContent.Append("<tr>");
                tableContent.Append($"<td><strong class=\"{textColor}\" title=\"{badge}\">{icon}</strong> {i}</td>");
                tableContent.Append($"<td>{HighlightDifferences(words1, words2)}</td>");
                tableContent.Append($"<td>{HighlightDifferences(words2, words1)}</td>");
                tableContent.Append("</tr>");
            }

            tableContent.Append("</table>");
            int equalLines = maxLines - mismatchLines;
            double equalPercentage = equalLines * hundred / maxLines;
            double mismatchPercentage = hundred - equalPercentage;
            var bodyContent = new StringBuilder();
            bodyContent.Append("<h2>Summary</h2>");
            bodyContent.Append("<div class=\"container text-center\">");
            bodyContent.Append("<div class=\"row\"><div class=\"col-7\">Number of lines</div><div class=\"col\">Max lines</div><div class=\"col\">Mismatch lines</div><div class=\"col\">Equal percentage</div></div>");
            bodyContent.Append("<div class=\"row\">");
            bodyContent.Append($"<div class=\"col-7 text-start\">{filePath1.HtmlEncode()}: <strong>{linesCount1}</strong><br />{filePath2.HtmlEncode()}: <strong>{linesCount2}</strong></div>");
            bodyContent.Append($"<div class=\"col\"><h3>{maxLines}</h3></div>");
            bodyContent.Append($"<div class=\"col\"><h3>{mismatchLines}</h3></div>");
            bodyContent.Append("<div class=\"col\">");
            bodyContent.Append($"<h3>{equalPercentage:n}%</h3>");
            bodyContent.Append($"<div class=\"progress-stacked\">");
            bodyContent.Append($"<div class=\"progress\" role=\"progressbar\" aria-label=\"Segment equal\" aria-valuenow=\"{equalPercentage}\" aria-valuemin=\"0\" aria-valuemax=\"{hundred}\" style=\"width: {equalPercentage}%\"><div class=\"progress-bar bg-success\"></div></div>");
            bodyContent.Append($"<div class=\"progress\" role=\"progressbar\" aria-label=\"Segment mismatch\" aria-valuenow=\"{mismatchPercentage}\" aria-valuemin=\"0\" aria-valuemax=\"{hundred}\" style=\"width: {mismatchPercentage}%\"><div class=\"progress-bar bg-danger\"></div></div>");
            bodyContent.Append($"</div></div></div></div>");
            bodyContent.Append(tableContent);
            
            string html = ResourceHelper.GetLayoutHtml();
            html = html.Replace("{PageTitle}", "PDF Comparison Result (by side)");
            html = html.Replace("{BodyContent}", bodyContent.ToString());

            if (!string.IsNullOrWhiteSpace(outputFileName))
            {
                WriteOutput(outputFileName, html);
            }

            return new DocumentComparisonResult(html, mismatchLines, equalPercentage);
        }

        public static DocumentComparisonResult ComparePdfByPage(string filePath1, string filePath2, string? outputFileName = null)
        {
            var reader1 = new PdfTextReader(filePath1);
            var reader2 = new PdfTextReader(filePath2);
            int numberOfPages1 = reader1.GetAllNumberOfPages();
            int numberOfPages2 = reader2.GetAllNumberOfPages();
            int maxPages = Math.Max(numberOfPages1, numberOfPages2);
            int linesCount1 = 0;
            int linesCount2 = 0;
            int mismatchLines = 0;
            var bodyContent = new StringBuilder();
            var tableContent = new StringBuilder();
            var paginationContent = new StringBuilder("<nav id=\"PageIndex\" aria-label=\"...\"><ul class=\"pagination pagination-sm\">");

            for (int page = 1; page <= maxPages; page++)
            {
                string text1 = page <= numberOfPages1 ? reader1.GetTextPage(page).Text : string.Empty;
                string text2 = page <= numberOfPages2 ? reader2.GetTextPage(page).Text : string.Empty;

                tableContent.Append($"<table id=\"Page{page}\" class=\"table table-bordered caption-top\"><caption>Page {page}</caption>");
                tableContent.Append($"<thead class=\"table-primary\"><tr><th>Line</th><th>{filePath1.HtmlEncode()}</th><th>{filePath2.HtmlEncode()}</th></tr></thead><tbody>");
                string[] lines1 = text1.SplitLines();
                string[] lines2 = text2.SplitLines();
                linesCount1 += lines1.Length;
                linesCount2 += lines2.Length;
                int pageMaxLines = Math.Max(lines1.Length, lines2.Length);
                int pageMismatchLines = 0;

                for (int i = 0; i < pageMaxLines; i++)
                {
                    string line1 = i < lines1.Length ? lines1[i] : string.Empty;
                    string line2 = i < lines2.Length ? lines2[i] : string.Empty;
                    string textColor;
                    string icon;
                    string badge;

                    if (line1 == line2)
                    {
                        textColor = textSuccess;
                        icon = iconSuccess;
                        badge = "Equal";
                    }
                    else
                    {
                        textColor = textError;
                        icon = iconError;
                        badge = "Mismatch";
                        pageMismatchLines++;
                    }

                    string[] words1 = line1.Split(' ');
                    string[] words2 = line2.Split(' ');
                    tableContent.Append("<tr>");
                    tableContent.Append($"<td><strong class=\"{textColor}\" title=\"{badge}\">{icon}</strong> {i}</td>");
                    tableContent.Append("<td>" + HighlightDifferences(words1, words2) + "</td>");
                    tableContent.Append("<td>" + HighlightDifferences(words2, words1) + "</td>");
                    tableContent.Append("</tr>");
                }

                paginationContent.Append($"<li class=\"page-item\"><a class=\"page-link{(pageMismatchLines > 0 ? " bg-danger-subtle" : string.Empty)}\" href=\"#Page{page}\">{page}</a></li>");
                tableContent.Append("</tbody></table>");
                tableContent.Append("<p><a href=\"#PageIndex\">Go to Page Index</a></p>");
                mismatchLines += pageMismatchLines;
            }

            paginationContent.Append("</ul></nav>");
            int maxLines = Math.Max(linesCount1, linesCount2);
            int equalLines = maxLines - mismatchLines;
            double equalPercentage = equalLines * hundred / maxLines;
            double mismatchPercentage = hundred - equalPercentage;
            bodyContent.Append("<h2>Summary</h2>");
            bodyContent.Append("<div class=\"container text-center\">");
            bodyContent.Append("<div class=\"row\"><div class=\"col-7\">Number of lines</div><div class=\"col\">Max lines</div><div class=\"col\">Mismatch lines</div><div class=\"col\">Equal percentage</div></div>");
            bodyContent.Append("<div class=\"row\">");
            bodyContent.Append($"<div class=\"col-7 text-start\">{filePath1.HtmlEncode()}: <strong>{linesCount1}</strong><br />{filePath2.HtmlEncode()}: <strong>{linesCount2}</strong></div>");
            bodyContent.Append($"<div class=\"col\"><h3>{maxLines}</h3></div>");
            bodyContent.Append($"<div class=\"col\"><h3>{mismatchLines}</h3></div>");
            bodyContent.Append("<div class=\"col\">");
            bodyContent.Append($"<h3>{equalPercentage:n}%</h3>");
            bodyContent.Append($"<div class=\"progress-stacked\">");
            bodyContent.Append($"<div class=\"progress\" role=\"progressbar\" aria-label=\"Segment equal\" aria-valuenow=\"{equalPercentage}\" aria-valuemin=\"0\" aria-valuemax=\"{hundred}\" style=\"width: {equalPercentage}%\"><div class=\"progress-bar bg-success\"></div></div>");
            bodyContent.Append($"<div class=\"progress\" role=\"progressbar\" aria-label=\"Segment mismatch\" aria-valuenow=\"{mismatchPercentage}\" aria-valuemin=\"0\" aria-valuemax=\"{hundred}\" style=\"width: {mismatchPercentage}%\"><div class=\"progress-bar bg-danger\"></div></div>");
            bodyContent.Append($"</div></div></div></div>");
            bodyContent.Append(paginationContent);
            bodyContent.Append(tableContent);
            string html = ResourceHelper.GetLayoutHtml();
            html = html.Replace("{PageTitle}", "PDF Comparison Result (by page)");
            html = html.Replace("{BodyContent}", bodyContent.ToString());

            if (!string.IsNullOrWhiteSpace(outputFileName))
            {
                WriteOutput(outputFileName, html);
            }

            return new DocumentComparisonResult(html, mismatchLines, equalPercentage);
        }

        public static DocumentComparisonsReport CompareByDirectory(string dir1, string dir2,ComparisonTypes comparisonType, string? outputFileName = null)
        {
            var files1 = Directory.GetFiles(dir1, pdfSearchPattern).Select(x => Path.GetFileName(x));
            var files2 = Directory.GetFiles(dir2, pdfSearchPattern).Select(x => Path.GetFileName(x));
            var matchingFiles = files1.Intersect(files2);
            var comparisonResults = new List<DocumentComparisonResult>(matchingFiles.Count());

            foreach (string fileName in matchingFiles)
            {
                string filePath1 = Path.Combine(dir1, fileName);
                string filePath2 = Path.Combine(dir2, fileName);
                string resultFileName = $"{Path.GetFileNameWithoutExtension(fileName)}-ComparisonResult.html";
                DocumentComparisonResult comparisonResult = comparisonType switch
                {
                    ComparisonTypes.CompareByPage => ComparePdfByPage(filePath1, filePath2, resultFileName),
                    ComparisonTypes.CompareBySide => ComparePdfBySide(filePath1, filePath2, resultFileName),
                    _ => ComparePdfByPage(filePath1, filePath2, resultFileName)
                };

                comparisonResult.ComparisonFileName = fileName;
                comparisonResult.ResultFileName = resultFileName;
                comparisonResults.Add(comparisonResult);
            }

            var result = new DocumentComparisonsReport(comparisonResults, dir1, dir2);

            if (!string.IsNullOrWhiteSpace(outputFileName))
            {
                string html = GenerateDocumentComparisonsReportHtml(result);
                WriteOutput(outputFileName, html);
            }

            return result;
        }

        public static string GenerateDocumentComparisonsReportHtml(DocumentComparisonsReport comparisonReport)
        {
            var bodyContent = new StringBuilder($"<h2>Summary</h2>");
            bodyContent.Append("<div class=\"container text-center\">");
            bodyContent.Append("<div class=\"row\"><div class=\"col-7\">Directories under comparison</div><div class=\"col\">Total matching files</div><div class=\"col\">Different files</div><div class=\"col\">Equal percentage</div></div>");
            bodyContent.Append("<div class=\"row\">");
            bodyContent.Append($"<div class=\"col-7 text-start\"><strong>{comparisonReport.Directory1.HtmlEncode()}</strong><br /><strong>{comparisonReport.Directory2.HtmlEncode()}</strong></div>");
            bodyContent.Append($"<div class=\"col\"><h3>{comparisonReport.TotalMatchingFiles}</h3></div>");
            bodyContent.Append($"<div class=\"col\"><h3>{comparisonReport.DifferentFiles}</h3></div>");
            bodyContent.Append("<div class=\"col\">");
            bodyContent.Append($"<h3>{comparisonReport.EqualPercentage:n}%</h3>");
            bodyContent.Append("<div class=\"progress-stacked\">");
            bodyContent.Append($"<div class=\"progress\" role=\"progressbar\" aria-label=\"Segment equal\" aria-valuenow=\"{comparisonReport.EqualPercentage}\" aria-valuemin=\"0\" aria-valuemax=\"{hundred}\" style=\"width: {comparisonReport.EqualPercentage}%\"><div class=\"progress-bar bg-success\"></div></div>");
            bodyContent.Append($"<div class=\"progress\" role=\"progressbar\" aria-label=\"Segment different\" aria-valuenow=\"{comparisonReport.DifferentPercentage}\" aria-valuemin=\"0\" aria-valuemax=\"{hundred}\" style=\"width: {comparisonReport.DifferentPercentage}%\"><div class=\"progress-bar bg-danger\"></div></div>");
            bodyContent.Append("</div></div></div></div>");
            bodyContent.Append("<table class=\"table table-bordered caption-top\"><caption>Results</caption>");
            bodyContent.Append("<thead class=\"table-primary\"><tr><th>Comparison File Name</th><th>Mismatch Lines</th><th>Equal Percentage</th><th>Result Detail</th></tr></thead><tbody>");

            foreach (DocumentComparisonResult comparisonResult in comparisonReport.ComparisonResults)
            {
                string textColor;
                string icon;
                string badge;

                if (comparisonResult.SummaryResult)
                {
                    textColor = textSuccess;
                    icon = iconSuccess;
                    badge = "Equal";
                }
                else
                {
                    textColor = textError;
                    icon = iconError;
                    badge = "Mismatch";
                }

                bodyContent.Append("<tr>");
                bodyContent.Append($"<td><strong class=\"{textColor}\" title=\"{badge}\">{icon}</strong> {comparisonResult.ComparisonFileName.HtmlEncode()}</td>");
                bodyContent.Append($"<td>{comparisonResult.MismatchLines}</td>");
                bodyContent.Append($"<td>{comparisonResult.EqualPercentage:n}</td>");
                bodyContent.Append($"<td><a href=\"{comparisonResult.ResultFileName}\" target=\"_blank\">Detail</a></td>");
                bodyContent.Append("</tr>");
            }

            bodyContent.Append("</tbody></table>");
            string result = ResourceHelper.GetLayoutHtml();
            result = result.Replace("{PageTitle}", "Document Comparisons Report");
            result = result.Replace("{BodyContent}", bodyContent.ToString());
            return result;
        }
    }
}
