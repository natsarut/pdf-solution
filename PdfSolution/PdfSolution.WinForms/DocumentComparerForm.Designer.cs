namespace PdfSolution.WinForms
{
    partial class DocumentComparerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            compareButton = new Button();
            openFileDialog1 = new OpenFileDialog();
            file1TextBox = new TextBox();
            selectFile1Button = new Button();
            file2TextBox = new TextBox();
            selectFile2Button = new Button();
            openFileDialog2 = new OpenFileDialog();
            comparisonTypeComboBox = new ComboBox();
            fileGroupBox = new GroupBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog2 = new FolderBrowserDialog();
            folderGroupBox = new GroupBox();
            selectFolder2Button = new Button();
            selectFolder1Button = new Button();
            folder2TextBox = new TextBox();
            folder1TextBox = new TextBox();
            fileRadio = new RadioButton();
            folderRadio = new RadioButton();
            label2 = new Label();
            fileGroupBox.SuspendLayout();
            folderGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(101, 15);
            label1.TabIndex = 0;
            label1.Text = "Comparison type:";
            // 
            // compareButton
            // 
            compareButton.Location = new Point(698, 248);
            compareButton.Name = "compareButton";
            compareButton.Size = new Size(75, 23);
            compareButton.TabIndex = 2;
            compareButton.Text = "Compare";
            compareButton.UseVisualStyleBackColor = true;
            compareButton.Click += CompareButton_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";
            // 
            // file1TextBox
            // 
            file1TextBox.Location = new Point(6, 22);
            file1TextBox.Name = "file1TextBox";
            file1TextBox.ReadOnly = true;
            file1TextBox.Size = new Size(600, 23);
            file1TextBox.TabIndex = 3;
            // 
            // selectFile1Button
            // 
            selectFile1Button.Location = new Point(612, 22);
            selectFile1Button.Name = "selectFile1Button";
            selectFile1Button.Size = new Size(104, 23);
            selectFile1Button.TabIndex = 4;
            selectFile1Button.Text = "Select File 1...";
            selectFile1Button.UseVisualStyleBackColor = true;
            selectFile1Button.Click += SelectFile1Button_Click;
            // 
            // file2TextBox
            // 
            file2TextBox.Location = new Point(6, 51);
            file2TextBox.Name = "file2TextBox";
            file2TextBox.ReadOnly = true;
            file2TextBox.Size = new Size(600, 23);
            file2TextBox.TabIndex = 5;
            // 
            // selectFile2Button
            // 
            selectFile2Button.Location = new Point(612, 51);
            selectFile2Button.Name = "selectFile2Button";
            selectFile2Button.Size = new Size(104, 23);
            selectFile2Button.TabIndex = 6;
            selectFile2Button.Text = "Select File 2...";
            selectFile2Button.UseVisualStyleBackColor = true;
            selectFile2Button.Click += SelectFile2Button_Click;
            // 
            // openFileDialog2
            // 
            openFileDialog2.Filter = "pdf files (*.pdf)|*.pdf";
            // 
            // comparisonTypeComboBox
            // 
            comparisonTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comparisonTypeComboBox.FormattingEnabled = true;
            comparisonTypeComboBox.Items.AddRange(new object[] { "Compare by side", "Compare by page" });
            comparisonTypeComboBox.Location = new Point(119, 8);
            comparisonTypeComboBox.Name = "comparisonTypeComboBox";
            comparisonTypeComboBox.Size = new Size(121, 23);
            comparisonTypeComboBox.TabIndex = 8;
            // 
            // fileGroupBox
            // 
            fileGroupBox.Controls.Add(file1TextBox);
            fileGroupBox.Controls.Add(selectFile1Button);
            fileGroupBox.Controls.Add(file2TextBox);
            fileGroupBox.Controls.Add(selectFile2Button);
            fileGroupBox.Enabled = false;
            fileGroupBox.Location = new Point(35, 52);
            fileGroupBox.Name = "fileGroupBox";
            fileGroupBox.Size = new Size(738, 91);
            fileGroupBox.TabIndex = 9;
            fileGroupBox.TabStop = false;
            fileGroupBox.Text = "Select files:";
            // 
            // folderGroupBox
            // 
            folderGroupBox.Controls.Add(selectFolder2Button);
            folderGroupBox.Controls.Add(selectFolder1Button);
            folderGroupBox.Controls.Add(folder2TextBox);
            folderGroupBox.Controls.Add(folder1TextBox);
            folderGroupBox.Enabled = false;
            folderGroupBox.Location = new Point(35, 149);
            folderGroupBox.Name = "folderGroupBox";
            folderGroupBox.Size = new Size(738, 93);
            folderGroupBox.TabIndex = 10;
            folderGroupBox.TabStop = false;
            folderGroupBox.Text = "Select folders:";
            // 
            // selectFolder2Button
            // 
            selectFolder2Button.Location = new Point(612, 51);
            selectFolder2Button.Name = "selectFolder2Button";
            selectFolder2Button.Size = new Size(104, 23);
            selectFolder2Button.TabIndex = 14;
            selectFolder2Button.Text = "Select Folder 2...";
            selectFolder2Button.UseVisualStyleBackColor = true;
            selectFolder2Button.Click += SelectFolder2Button_Click;
            // 
            // selectFolder1Button
            // 
            selectFolder1Button.Location = new Point(612, 22);
            selectFolder1Button.Name = "selectFolder1Button";
            selectFolder1Button.Size = new Size(104, 23);
            selectFolder1Button.TabIndex = 13;
            selectFolder1Button.Text = "Select Folder 1...";
            selectFolder1Button.UseVisualStyleBackColor = true;
            selectFolder1Button.Click += SelectFolder1Button_Click;
            // 
            // folder2TextBox
            // 
            folder2TextBox.Location = new Point(6, 51);
            folder2TextBox.Name = "folder2TextBox";
            folder2TextBox.ReadOnly = true;
            folder2TextBox.Size = new Size(600, 23);
            folder2TextBox.TabIndex = 12;
            // 
            // folder1TextBox
            // 
            folder1TextBox.Location = new Point(6, 22);
            folder1TextBox.Name = "folder1TextBox";
            folder1TextBox.ReadOnly = true;
            folder1TextBox.Size = new Size(600, 23);
            folder1TextBox.TabIndex = 11;
            // 
            // fileRadio
            // 
            fileRadio.AutoSize = true;
            fileRadio.Location = new Point(15, 52);
            fileRadio.Name = "fileRadio";
            fileRadio.Size = new Size(14, 13);
            fileRadio.TabIndex = 11;
            fileRadio.TabStop = true;
            fileRadio.UseVisualStyleBackColor = true;
            fileRadio.Click += FileRadio_Click;
            // 
            // folderRadio
            // 
            folderRadio.AutoSize = true;
            folderRadio.Location = new Point(15, 149);
            folderRadio.Name = "folderRadio";
            folderRadio.Size = new Size(14, 13);
            folderRadio.TabIndex = 12;
            folderRadio.TabStop = true;
            folderRadio.UseVisualStyleBackColor = true;
            folderRadio.Click += FolderRadio_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 34);
            label2.Name = "label2";
            label2.Size = new Size(118, 15);
            label2.TabIndex = 7;
            label2.Text = "Select files or folders:";
            // 
            // DocumentComparerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 450);
            Controls.Add(label2);
            Controls.Add(folderRadio);
            Controls.Add(fileRadio);
            Controls.Add(folderGroupBox);
            Controls.Add(fileGroupBox);
            Controls.Add(comparisonTypeComboBox);
            Controls.Add(compareButton);
            Controls.Add(label1);
            Name = "DocumentComparerForm";
            Text = "Document Comparer";
            fileGroupBox.ResumeLayout(false);
            fileGroupBox.PerformLayout();
            folderGroupBox.ResumeLayout(false);
            folderGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button compareButton;
        private OpenFileDialog openFileDialog1;
        private TextBox file1TextBox;
        private Button selectFile1Button;
        private TextBox file2TextBox;
        private Button selectFile2Button;
        private OpenFileDialog openFileDialog2;
        private ComboBox comparisonTypeComboBox;
        private GroupBox fileGroupBox;
        private FolderBrowserDialog folderBrowserDialog1;
        private FolderBrowserDialog folderBrowserDialog2;
        private GroupBox folderGroupBox;
        private TextBox folder1TextBox;
        private Button selectFolder2Button;
        private Button selectFolder1Button;
        private TextBox folder2TextBox;
        private RadioButton fileRadio;
        private RadioButton folderRadio;
        private Label label2;
    }
}