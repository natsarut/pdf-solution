using PdfSolution.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

const string processingMessage = "Processing...";
const string resultFileName = "ComparisonResult.html";
const string outputDirectory = "output";
var helpMessage = new StringBuilder("\nUsage: pdfsln [function-name]\n\n");
helpMessage.AppendLine("function-name:");
helpMessage.AppendLine("  -comparebyside [file-path-1] [file-path-2]    Compare PDF 2 files by side.");
helpMessage.AppendLine("    file-path-1                                 PDF file 1 to compare.");
helpMessage.AppendLine("    file-path-2                                 PDF file 2 to compare.\n");
helpMessage.AppendLine("  -comparebypage [file-path-1] [file-path-2]    Compare PDF 2 files by page.");
helpMessage.AppendLine("    file-path-1                                 PDF file 1 to compare.");
helpMessage.AppendLine("    file-path-2                                 PDF file 2 to compare.\n");
helpMessage.AppendLine("  -comparebydir [dir-1] [dir-2]                 Compare PDF files in 2 directories.");
helpMessage.AppendLine("    dir-1                                       Directory 1 to compare.");
helpMessage.AppendLine("    dir-2                                       Directory 2 to compare.\n");
helpMessage.AppendLine("  -texttable [file-path]                        Creates text table to specify text position for test case.");
helpMessage.AppendLine("    file-path                                   PDF file to creates text table.\n");
helpMessage.AppendLine("  -testdoc [dir]                                Run test documents in directory by test documents configuration file.");
helpMessage.AppendLine("    dir                                         Directory to run test documents.");

if (args.Length > 0)
{
    string filePath1;
    string filePath2;
    string outputFileName;

    try
    {
        string functionName = args[0];

        switch (functionName.ToLower())
        {
            case "-comparebyside":
                filePath1 = args[1];
                filePath2 = args[2];
                Console.WriteLine(processingMessage);
                ClearOutputDirectory();
                _ = PdfTool.ComparePdfBySide(filePath1, filePath2, resultFileName);
                Console.WriteLine($"Comparison complete. See {resultFileName} for details.");
                OpenFile(resultFileName);
                break;
            case "-comparebypage":
                filePath1 = args[1];
                filePath2 = args[2];
                Console.WriteLine(processingMessage);
                ClearOutputDirectory();
                _ = PdfTool.ComparePdfByPage(filePath1, filePath2, resultFileName);
                Console.WriteLine($"Comparison complete. See {resultFileName} for details.");
                OpenFile(resultFileName);
                break;
            case "-comparebydir":
                string dir1 = args[1];
                string dir2 = args[2];

                if (!Directory.Exists(dir1))
                {
                    Console.WriteLine($"Directory '{dir1}' not found.");
                }
                else if (!Directory.Exists(dir2))
                {
                    Console.WriteLine($"Directory '{dir2}' not found.");
                }
                else
                {
                    outputFileName = "DocumentComparisons.html";
                    Console.WriteLine(processingMessage);
                    ClearOutputDirectory();
                    DocumentComparisonsReport report = PdfTool.CompareByDirectory(dir1, dir2, outputFileName);
                    Console.WriteLine($"Found {report.TotalMatchingFiles} matching files.");
                    Console.WriteLine($"Comparison complete. See directory '{outputDirectory}' for details.");
                    OpenFile(outputFileName);
                }

                break;
            case "-texttable":
                filePath1 = args[1];
                Console.WriteLine(processingMessage);
                ClearOutputDirectory();
                outputFileName = "TextTable.html";
                _ = PdfTool.GenerateTextTableHtml(filePath1, outputFileName);
                Console.WriteLine("Generate HTML complete.");
                OpenFile(outputFileName);
                break;
            case "-testdoc":
                outputFileName = "TestDocuments.html";
                Console.WriteLine(processingMessage);
                ClearOutputDirectory();
                _ = PdfTool.TestDocuments(args[1], outputFileName);
                Console.WriteLine("Generates test documents report complete.");
                OpenFile(outputFileName);
                break;
            default:
                Console.WriteLine($"Function name '{functionName}' is invalid.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
    }
}
else
{
    Console.WriteLine(helpMessage.ToString());
}

static void ClearOutputDirectory()
{
    if (Directory.Exists(outputDirectory))
    {
        foreach (string file in Directory.GetFiles(outputDirectory))
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
    }
}

static void OpenFile(string fileName)
{
    string outputFilePath = Path.Combine(outputDirectory, fileName);

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        Process.Start(new ProcessStartInfo(outputFilePath) { UseShellExecute = true });
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
        Process.Start("xdg-open", outputFilePath);
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
        Process.Start("open", outputFilePath);
    }
}