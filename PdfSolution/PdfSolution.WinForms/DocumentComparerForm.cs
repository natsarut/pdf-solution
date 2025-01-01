using PdfSolution.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfSolution.WinForms
{
    public partial class DocumentComparerForm : Form
    {
        private const string outputFileName = "ComparisonResult.html";

        public DocumentComparerForm()
        {
            InitializeComponent();
        }

        private void SelectFilesOrFolders()
        {
            fileGroupBox.Enabled = fileRadio.Checked;
            folderGroupBox.Enabled = folderRadio.Checked;
        }

        private bool ValidateInputs()
        {
            var result = false;

            if (string.IsNullOrEmpty(comparisonTypeComboBox.Text))
            {
                Program.ShowValidationMessageBox("Please select comparison type.");
            }
            else if (!(fileRadio.Checked||folderRadio.Checked))
            {
                Program.ShowValidationMessageBox("Please select files or folders.");
            }
            else if (fileRadio.Checked)
            {
                if (string.IsNullOrEmpty(file1TextBox.Text))
                {
                    Program.ShowValidationMessageBox("Please select file 1.");
                }
                else if (string.IsNullOrEmpty(file2TextBox.Text))
                {
                    Program.ShowValidationMessageBox("Please select file 2.");
                }
                else
                {
                    result = true;
                }
            }
            else if (folderRadio.Checked)
            {
                if (string.IsNullOrEmpty(folder1TextBox.Text))
                {
                    Program.ShowValidationMessageBox("Please select folder 1.");
                }
                else if (string.IsNullOrEmpty(folder2TextBox.Text))
                {
                    Program.ShowValidationMessageBox("Please select folder 2.");
                }
                else
                {
                    result = true;
                }
            }

            return result;
        }

        private void CompareButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    switch (comparisonTypeComboBox.Text)
                    {
                        case "Compare by side":
                            if (fileRadio.Checked)
                            {
                                PdfTool.ComparePdfBySide(file1TextBox.Text, file2TextBox.Text, outputFileName);
                            }
                            else
                            {
                                PdfTool.CompareByDirectory(folder1TextBox.Text, folder2TextBox.Text, PdfTool.ComparisonTypes.CompareBySide, outputFileName);
                            }

                            break;
                        case "Compare by page":
                            if (fileRadio.Checked)
                            {
                                PdfTool.ComparePdfByPage(file1TextBox.Text, file2TextBox.Text, outputFileName);
                            }
                            else
                            {
                                PdfTool.CompareByDirectory(folder1TextBox.Text, folder2TextBox.Text, PdfTool.ComparisonTypes.CompareByPage, outputFileName);
                            }

                            break;
                    }

                    Program.OpenOutputFile(outputFileName);
                    Program.ShowSuccessMessageBox("Compare documents successfully.");
                }
            }
            catch (Exception ex)
            {
                Program.ShowExceptionMessageBox(ex.Message);
            }   
        }

        private void SelectFile1Button_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                file1TextBox.Text = openFileDialog1.FileName;
            }
        }

        private void SelectFile2Button_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog(this) == DialogResult.OK)
            {
                file2TextBox.Text = openFileDialog2.FileName;
            }
        }

        private void SelectFolder1Button_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                folder1TextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void SelectFolder2Button_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog(this) == DialogResult.OK)
            {
                folder2TextBox.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        private void FileRadio_Click(object sender, EventArgs e)
        {
            SelectFilesOrFolders();
        }

        private void FolderRadio_Click(object sender, EventArgs e)
        {
            SelectFilesOrFolders();
        }
    }
}
