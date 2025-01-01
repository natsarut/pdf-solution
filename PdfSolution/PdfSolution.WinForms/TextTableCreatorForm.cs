using PdfSolution.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfSolution.WinForms
{
    public partial class TextTableCreatorForm : Form
    {
        public TextTableCreatorForm()
        {
            InitializeComponent();
        }

        private bool ValidateInputs()
        {
            var result = false;

            if (string.IsNullOrEmpty(fileTextBox.Text))
            {
                Program.ShowValidationMessageBox("Please select a file");
            }
            else
            {
                result = true;
            }

            return result;
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                fileTextBox.Text = openFileDialog.FileName;
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    var outputFileName = "TextTable.html";
                    PdfTool.GenerateTextTableHtml(fileTextBox.Text, outputFileName);
                    Program.OpenOutputFile(outputFileName);
                    Program.ShowSuccessMessageBox("Creates text table successfully.");
                }
            }
            catch (Exception ex)
            {
                Program.ShowExceptionMessageBox(ex.Message);
            }
        }
    }
}
