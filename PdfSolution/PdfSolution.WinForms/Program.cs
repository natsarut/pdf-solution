using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PdfSolution.WinForms
{
    internal static class Program
    {
        public const string OutputPath = "output";
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }

            Application.Run(new MainForm());
        }

        public static void ShowValidationMessageBox(string message)
        {
            MessageBox.Show(message, "Inputs Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowExceptionMessageBox(string message)
        {
            MessageBox.Show(message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowSuccessMessageBox(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void OpenOutputFile(string fileName)
        {
            string outputFilePath = Path.Combine(OutputPath, fileName);

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
    }
}