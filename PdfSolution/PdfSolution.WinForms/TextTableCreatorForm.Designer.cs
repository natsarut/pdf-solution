namespace PdfSolution.WinForms
{
    partial class TextTableCreatorForm
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
            fileTextBox = new TextBox();
            selectFileButton = new Button();
            createButton = new Button();
            openFileDialog = new OpenFileDialog();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(195, 15);
            label1.TabIndex = 0;
            label1.Text = "Select a file to creates the text table:";
            // 
            // fileTextBox
            // 
            fileTextBox.Location = new Point(12, 27);
            fileTextBox.Name = "fileTextBox";
            fileTextBox.ReadOnly = true;
            fileTextBox.Size = new Size(600, 23);
            fileTextBox.TabIndex = 4;
            // 
            // selectFileButton
            // 
            selectFileButton.Location = new Point(618, 27);
            selectFileButton.Name = "selectFileButton";
            selectFileButton.Size = new Size(79, 23);
            selectFileButton.TabIndex = 5;
            selectFileButton.Text = "Select File...";
            selectFileButton.UseVisualStyleBackColor = true;
            selectFileButton.Click += SelectFileButton_Click;
            // 
            // createButton
            // 
            createButton.Location = new Point(622, 56);
            createButton.Name = "createButton";
            createButton.Size = new Size(75, 23);
            createButton.TabIndex = 6;
            createButton.Text = "Create";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += CreateButton_Click;
            // 
            // TextTableCreatorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(createButton);
            Controls.Add(selectFileButton);
            Controls.Add(fileTextBox);
            Controls.Add(label1);
            Name = "TextTableCreatorForm";
            Text = "Text Table Creator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox fileTextBox;
        private Button selectFileButton;
        private Button createButton;
        private OpenFileDialog openFileDialog;
    }
}