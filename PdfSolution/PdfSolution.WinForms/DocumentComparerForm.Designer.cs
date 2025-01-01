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
            selectFile1button = new Button();
            file2TextBox = new TextBox();
            selectFile2button = new Button();
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
            compareButton.Location = new Point(695, 233);
            compareButton.Name = "compareButton";
            compareButton.Size = new Size(75, 23);
            compareButton.TabIndex = 2;
            compareButton.Text = "Compare";
            compareButton.UseVisualStyleBackColor = true;
            compareButton.Click += CompareButton_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
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
            // selectFile1button
            // 
            selectFile1button.Location = new Point(612, 22);
            selectFile1button.Name = "selectFile1button";
            selectFile1button.Size = new Size(79, 23);
            selectFile1button.TabIndex = 4;
            selectFile1button.Text = "Select File 1";
            selectFile1button.UseVisualStyleBackColor = true;
            selectFile1button.Click += SelectFile1button_Click;
            // 
            // file2TextBox
            // 
            file2TextBox.Location = new Point(6, 51);
            file2TextBox.Name = "file2TextBox";
            file2TextBox.ReadOnly = true;
            file2TextBox.Size = new Size(600, 23);
            file2TextBox.TabIndex = 5;
            // 
            // selectFile2button
            // 
            selectFile2button.Location = new Point(612, 51);
            selectFile2button.Name = "selectFile2button";
            selectFile2button.Size = new Size(79, 23);
            selectFile2button.TabIndex = 6;
            selectFile2button.Text = "Select File 2";
            selectFile2button.UseVisualStyleBackColor = true;
            selectFile2button.Click += SelectFile2button_Click;
            // 
            // openFileDialog2
            // 
            openFileDialog2.FileName = "openFileDialog2";
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
            fileGroupBox.Controls.Add(selectFile1button);
            fileGroupBox.Controls.Add(file2TextBox);
            fileGroupBox.Controls.Add(selectFile2button);
            fileGroupBox.Enabled = false;
            fileGroupBox.Location = new Point(32, 37);
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
            folderGroupBox.Location = new Point(32, 134);
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
            selectFolder2Button.Size = new Size(93, 23);
            selectFolder2Button.TabIndex = 14;
            selectFolder2Button.Text = "Select Folder 2";
            selectFolder2Button.UseVisualStyleBackColor = true;
            selectFolder2Button.Click += SelectFolder2Button_Click;
            // 
            // selectFolder1Button
            // 
            selectFolder1Button.Location = new Point(612, 22);
            selectFolder1Button.Name = "selectFolder1Button";
            selectFolder1Button.Size = new Size(93, 23);
            selectFolder1Button.TabIndex = 13;
            selectFolder1Button.Text = "Select Folder 1";
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
            fileRadio.Location = new Point(12, 37);
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
            folderRadio.Location = new Point(12, 134);
            folderRadio.Name = "folderRadio";
            folderRadio.Size = new Size(14, 13);
            folderRadio.TabIndex = 12;
            folderRadio.TabStop = true;
            folderRadio.UseVisualStyleBackColor = true;
            folderRadio.Click += FolderRadio_Click;
            // 
            // DocumentComparerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 450);
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
        private Button selectFile1button;
        private TextBox file2TextBox;
        private Button selectFile2button;
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
    }
}