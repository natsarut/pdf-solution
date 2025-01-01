namespace PdfSolution.WinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            documentsComparerMenuItem = new ToolStripMenuItem();
            treeView = new TreeView();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { toolsToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1184, 24);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { documentsComparerMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // documentsComparerMenuItem
            // 
            documentsComparerMenuItem.Name = "documentsComparerMenuItem";
            documentsComparerMenuItem.Size = new Size(187, 22);
            documentsComparerMenuItem.Text = "Compare Documents";
            documentsComparerMenuItem.Click += DocumentsComparerMenuItem_Click;
            // 
            // treeView
            // 
            treeView.Dock = DockStyle.Left;
            treeView.Location = new Point(0, 24);
            treeView.Name = "treeView";
            treeView.Size = new Size(300, 537);
            treeView.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 561);
            Controls.Add(treeView);
            Controls.Add(menuStrip);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            Text = "PDF Solution";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem documentsComparerMenuItem;
        private TreeView treeView;
    }
}
